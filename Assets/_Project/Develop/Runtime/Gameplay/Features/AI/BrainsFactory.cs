using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.Common;
using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.AI.States;
using _Project.Develop.Runtime.Gameplay.Features.Attack;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.ConfigManagement;
using _Project.Develop.Runtime.Utilities.Input;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.TimerService;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.AI
{
	public class BrainsFactory
	{
		private readonly DIContainer _container;
		private readonly TimerServiceFactory _timerServiceFactory;
		private readonly AIBrainsContext _aiBrainsContext;
		private readonly IInputService _inputService;
		private readonly EntitiesLifeContext _entitiesLifeContext;

		public BrainsFactory(DIContainer container)
		{
			_container = container;
			
			_timerServiceFactory = _container.Resolve<TimerServiceFactory>();
			_aiBrainsContext = _container.Resolve<AIBrainsContext>();
			_inputService = _container.Resolve<IInputService>();
			_entitiesLifeContext = container.Resolve<EntitiesLifeContext>();
		}

		public StateMachineBrain CreateMainHeroBrain(Entity entity, ITargetSelector targetSelector)
		{
			AIStateMachine combatState = CreateAutoAttackStateMachine(entity);

			PlayerInputMovementState movementState = new PlayerInputMovementState(_inputService, entity);
			
			ReactiveVariable<Entity> currentTarget = entity.CurrentTarget;

			ICompositeCondition fromMovementToCombatStateCondition = new CompositeCondition()
				.AddCondition(new FuncCondition(() => currentTarget.Value != null))
				.AddCondition(new FuncCondition(() => _inputService.Direction == Vector3.zero));
			
			ICompositeCondition fromCombatToMovementStateCondition = new CompositeCondition(LogicOperations.Or)
				.AddCondition(new FuncCondition(() => currentTarget.Value == null))
				.AddCondition(new FuncCondition(() => _inputService.Direction != Vector3.zero));

			AIStateMachine behaviour = new AIStateMachine();
			
			behaviour.AddState(movementState);
			behaviour.AddState(combatState);
			
			behaviour.AddTransition(movementState, combatState, fromMovementToCombatStateCondition);
			behaviour.AddTransition(combatState, movementState, fromCombatToMovementStateCondition);

			FindTargetState findTargetState = new FindTargetState(targetSelector, _entitiesLifeContext, entity);
			AIParallelState parallelState = new AIParallelState(findTargetState, behaviour);

			AIStateMachine rootStateMachin = new AIStateMachine();
			
			rootStateMachin.AddState(parallelState);

			StateMachineBrain brain = new StateMachineBrain(rootStateMachin);
			_aiBrainsContext.SetFor(entity, brain);

			return brain;
		}

		public StateMachineBrain CreateMainHeroBrainWithManualAttack(Entity entity)
		{
			AIStateMachine combatState = CreateManualAttackStateMachine(entity);

			PlayerInputMovementState movementState = new PlayerInputMovementState(_inputService, entity);

			ICompositeCondition fromMovementToCombatStateCondition = new CompositeCondition()
				.AddCondition(new FuncCondition(() => _inputService.Direction == Vector3.zero));
			
			ICompositeCondition fromCombatToMovementStateCondition = new CompositeCondition(LogicOperations.Or)
				.AddCondition(new FuncCondition(() => _inputService.Direction != Vector3.zero));

			AIStateMachine behaviour = new AIStateMachine();
			
			behaviour.AddState(movementState);
			behaviour.AddState(combatState);
			
			behaviour.AddTransition(movementState, combatState, fromMovementToCombatStateCondition);
			behaviour.AddTransition(combatState, movementState, fromCombatToMovementStateCondition);

			StateMachineBrain brain = new StateMachineBrain(behaviour);
			_aiBrainsContext.SetFor(entity, brain);

			return brain;
		}

		public StateMachineBrain CreateGhostBrain(Entity entity)
		{
			AIStateMachine stateMachine = CreateRandomMovementStateMachine(entity);
			StateMachineBrain brain = new StateMachineBrain(stateMachine);
			
			_aiBrainsContext.SetFor(entity, brain);
			
			return brain;
		}

		public StateMachineBrain CreateGhostTeleportToRandomPointBrain(Entity entity)
		{
			AIStateMachine stateMachine = CreateTeleportStateMachine(entity, new RandomPointInRadiusSelector(entity.TeleportRadius));
			StateMachineBrain brain = new StateMachineBrain(stateMachine);
			
			_aiBrainsContext.SetFor(entity, brain);

			return brain;
		}

		public StateMachineBrain CreateGhostTeleportToTargetBrain(Entity entity, ITargetSelector targetSelector)
		{
			IPointSelector pointSelector = new ToTargetDirectionPointSelector(entity.CurrentTarget, entity, entity.TeleportRadius);

			ReactiveVariable<float> currentEnergy = entity.CurrentEnergy;
			ReactiveVariable<float> maxEnergy = entity.MaxEnergy;
			ReactiveVariable<float> percentOfEnergyToTeleport = entity.MinPercentOfEnergyToTeleport;
			
			ICompositeCondition additionalToTeleportStateCondition = new  CompositeCondition()
				.AddCondition(new FuncCondition(() => currentEnergy.Value / maxEnergy.Value > percentOfEnergyToTeleport.Value / 100f))
				.AddCondition(new FuncCondition(() => entity.CurrentTarget.Value != null));

			AIStateMachine stateMachine = CreateTeleportStateMachine(entity, pointSelector, additionalToTeleportStateCondition);
			
			FindTargetState findTargetState = new FindTargetState(targetSelector, _entitiesLifeContext, entity);
			AIParallelState  parallelState = new AIParallelState(findTargetState, stateMachine);
			
			AIStateMachine rootStateMachine = new AIStateMachine();
			
			rootStateMachine.AddState(parallelState);
			
			StateMachineBrain brain = new StateMachineBrain(rootStateMachine);
			
			_aiBrainsContext.SetFor(entity, brain);

			return brain;
		}

		private AIStateMachine CreateTeleportStateMachine(Entity entity, IPointSelector pointSelector, ICompositeCondition additionalCondition = null)
		{
			List<IDisposable> disposables = new List<IDisposable>();
			
			EmptyState cooldownState = new EmptyState();

			TeleportState teleportState = new TeleportState(
				entity.InTeleportCooldown,
				entity.TeleportRequest,
				pointSelector,
				entity.TeleportEnergyCost,
				entity.SpendEnergyRequest);
			
			TimerService teleportCooldownTimer = _timerServiceFactory.Create(entity.TeleportCooldownInitialTime.Value);

			disposables.Add(teleportState.Entered.Subscribe(teleportCooldownTimer.Restart));
			disposables.Add(teleportCooldownTimer.TimerEnded.Subscribe(() => entity.InTeleportCooldown.Value = false));

			ICompositeCondition fromCooldownStateToTeleport = new CompositeCondition()
				.AddCondition(new FuncCondition(() => teleportCooldownTimer.IsOver))
				.AddCondition(entity.CanTeleport);

			ICompositeCondition fromTeleportStateToCooldown = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.CanTeleport.Evaluate() == false));

			if (additionalCondition != null)
				fromCooldownStateToTeleport.AddCondition(additionalCondition);
			
			AIStateMachine behaviour = new AIStateMachine(disposables);
			
			behaviour.AddState(cooldownState);
			behaviour.AddState(teleportState);
			
			behaviour.AddTransition(teleportState, cooldownState, fromTeleportStateToCooldown);
			behaviour.AddTransition(cooldownState, teleportState, fromCooldownStateToTeleport);

			return behaviour;
		}

		private AIStateMachine CreateRandomMovementStateMachine(Entity entity)
		{
			List<IDisposable> disposables = new();
			
			RandomMovementState movementState = new RandomMovementState(
				entity,
				0.5f,
				30f);

			EmptyState emptyState = new EmptyState();

			TimerService movementTimer = _timerServiceFactory.Create(2f);
			disposables.Add(movementTimer);
			disposables.Add(movementState.Entered.Subscribe(movementTimer.Restart));
			
			TimerService idleTimer = _timerServiceFactory.Create(2f);
			disposables.Add(idleTimer);
			disposables.Add(emptyState.Entered.Subscribe(idleTimer.Restart));

			FuncCondition movementTimerEndCondition = new FuncCondition(() => movementTimer.IsOver);
			FuncCondition idleTimerEndCondition = new FuncCondition(() => idleTimer.IsOver);

			AIStateMachine stateMachine = new AIStateMachine(disposables);
			
			stateMachine.AddState(emptyState);
			stateMachine.AddState(movementState);
			
			stateMachine.AddTransition(movementState, emptyState, movementTimerEndCondition);
			stateMachine.AddTransition(emptyState, movementState, idleTimerEndCondition);
			
			return stateMachine;
		}
		
		private AIStateMachine CreateManualAttackStateMachine(Entity entity)
		{
			RotateToInputDeltaState rotateToInputState = new RotateToInputDeltaState(entity, _inputService);
			AttackTriggerState attackTriggerState = new AttackTriggerState(entity);

			ICondition canAttack = entity.GetComponent<CanStartAttack>().Value;

			ICompositeCondition fromRotateToAttackCondition = new CompositeCondition()
				.AddCondition(canAttack)
				.AddCondition(new FuncCondition(() => _inputService.IsAttackButtonPressed));
			
			ReactiveVariable<bool> inAttackProcess = entity.GetComponent<InAttackProcess>().Value;

			ICondition fromAttackToRotateStateCondition = new FuncCondition(() => inAttackProcess.Value == false);

			AIStateMachine stateMachine = new AIStateMachine();
			
			stateMachine.AddState(rotateToInputState);
			stateMachine.AddState(attackTriggerState);
			
			stateMachine.AddTransition(rotateToInputState, attackTriggerState, fromRotateToAttackCondition);
			stateMachine.AddTransition(attackTriggerState, rotateToInputState, fromAttackToRotateStateCondition);
			
			return stateMachine;
		}

		private AIStateMachine CreateAutoAttackStateMachine(Entity entity)
		{
			RotateToTargetState rotateToTargetState = new RotateToTargetState(entity);
			AttackTriggerState attackTriggerState = new AttackTriggerState(entity);

			ICondition canAttack = entity.GetComponent<CanStartAttack>().Value;
			Transform transform = entity.GetComponent<TransformComponent>().Value;
			ReactiveVariable<Entity> currentTarget = entity.GetComponent<CurrentTarget>().Value;

			ICompositeCondition fromRotateToAttackCondition = new CompositeCondition()
				.AddCondition(canAttack)
				.AddCondition(new FuncCondition(() =>
				{
					Entity target =  currentTarget.Value;

					if (target == null)
						return false;
					
					float angleToTarget = Quaternion.Angle(
						transform.rotation,
						Quaternion.LookRotation(
							target.GetComponent<TransformComponent>().Value.position -  transform.position));

					return angleToTarget <= 1f;
				}));
			
			ReactiveVariable<bool> inAttackProcess = entity.GetComponent<InAttackProcess>().Value;

			ICondition fromAttackToRotateStateCondition = new FuncCondition(() => inAttackProcess.Value == false);

			AIStateMachine stateMachine = new AIStateMachine();
			
			stateMachine.AddState(rotateToTargetState);
			stateMachine.AddState(attackTriggerState);
			
			stateMachine.AddTransition(rotateToTargetState, attackTriggerState, fromRotateToAttackCondition);
			stateMachine.AddTransition(attackTriggerState, rotateToTargetState, fromAttackToRotateStateCondition);
			
			return stateMachine;
		}
	}
}