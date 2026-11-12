using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.Common;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.AI.States;
using _Project.Develop.Runtime.Gameplay.Features.Attack;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Input;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.TimerService;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.AI
{
	public class BrainsFactory
	{
		private DIContainer _container;
		private TimerServiceFactory _timerServiceFactory;
		private AIBrainsContext _aiBrainsContext;
		private IInputService _inputService;

		public BrainsFactory(DIContainer container)
		{
			_container = container;
			
			_timerServiceFactory = _container.Resolve<TimerServiceFactory>();
			_aiBrainsContext = _container.Resolve<AIBrainsContext>();
			_inputService = _container.Resolve<IInputService>();
		}

		public StateMachineBrain CreateMainHeroBrain(Entity entity)
		{
			AIStateMachine combatState = CreateAutoAttackStateMachine(entity);

			PlayerInputMovementState movementState = new PlayerInputMovementState(_inputService, entity);
			
			ReactiveVariable<Entity> currentTarget = entity.GetComponent<CurrentTarget>().Value;

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