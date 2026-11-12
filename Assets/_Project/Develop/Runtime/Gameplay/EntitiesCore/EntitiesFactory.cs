using System;
using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Project.Develop.Runtime.Gameplay.Features.ApplyDamage;
using _Project.Develop.Runtime.Gameplay.Features.Attack;
using _Project.Develop.Runtime.Gameplay.Features.ContactTakeDamage;
using _Project.Develop.Runtime.Gameplay.Features.LIfeCycle;
using _Project.Develop.Runtime.Gameplay.Features.LIfeCycle.Energy;
using _Project.Develop.Runtime.Gameplay.Features.MovementFeuture;
using _Project.Develop.Runtime.Gameplay.Features.Sensors;
using _Project.Develop.Runtime.Gameplay.Features.TeleportTakeDamage;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.ConfigManagement;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.EntitiesCore
{
	public class EntitiesFactory
	{
		private const int BufferSize = 64;
		
		private readonly DIContainer _container;
		private readonly EntitiesLifeContext _entitiesContext;

		private readonly MonoEntitiesFactory _monoEntitiesFactory;
		private readonly ConfigService _configService;
		private readonly CollidersRegistryService _collidersRegistryService;

		public EntitiesFactory(DIContainer container)
		{
			_container = container;
			_monoEntitiesFactory = _container.Resolve<MonoEntitiesFactory>();
			_entitiesContext = _container.Resolve<EntitiesLifeContext>();
			_configService = _container.Resolve<ConfigService>();
			_collidersRegistryService = _container.Resolve<CollidersRegistryService>();
		}

		public Entity CreateGhostEntity(Vector3 position)
		{
			GhostConfig config = _configService.GetConfig<GhostConfig>();

			Entity entity = CreateEmpty();

			MonoEntity monoEntity =
				_monoEntitiesFactory.Create(entity, position, "Prefabs/Gameplay/Entity/GhostMonoEntity");
			
			Debug.Log(entity.ToString());

			entity
				.AddMoveDirection()
				.AddMoveSpeed(new ReactiveVariable<float>(config.MovementConfig.MoveSpeed))
				.AddRotateDirection()
				.AddIsMoving()
				.AddRotateSpeed(new ReactiveVariable<float>(config.MovementConfig.RotationSpeed))
				.AddMaxHealth(new ReactiveVariable<float>(config.HealthConfig.MaxHealth))
				.AddCurrentHealth(new ReactiveVariable<float>(config.HealthConfig.StartHealth))
				.AddIsDead(new ReactiveVariable<bool>(false))
				.AddInDeathProcess(new ReactiveVariable<bool>(false))
				.AddDeathProcessInitialTime(new ReactiveVariable<float>(config.HealthConfig.DeathProcessInitialTime))
				.AddDeathProcessCurrentTime()
				.AddTakeDamageRequest()
				.AddTakeDamageEvent()
				.AddContactsDetectingMask(1 << LayerMask.NameToLayer("Characters"))
				.AddContactColliderBuffer(new Buffer<Collider>(BufferSize))
				.AddContactEntitiesBuffer(new Buffer<Entity>(BufferSize))
				.AddBodyContactDamage(new ReactiveVariable<float>(config.AttackConfig.Damage));

			ICompositeCondition canMove = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.IsDead.Value == false));

			ICompositeCondition canRotate = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.IsDead.Value == false));

			ICompositeCondition mustDie = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.CurrentHealth.Value <= 0f));

			ICompositeCondition mustRelease = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.IsDead.Value))
				.AddCondition(new FuncCondition(() => entity.InDeathProcess.Value == false));

			ICompositeCondition canApplyDamage = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.IsDead.Value == false));

			entity
				.AddCanMove(canMove)
				.AddCanRotate(canRotate)
				.AddMustDie(mustDie)
				.AddMustSelfRelease(mustRelease)
				.AddCanApplyDamage(canApplyDamage);

			entity
				.AddRigidbodyMovementSystem()
				.AddRigidbodyRotationSystem()
				.AddBodyContactsDetectingSystem()
				.AddBodyContactsEntitiesFilterSystem(_collidersRegistryService)
				.AddDealDamageOnContactSystem()
				.AddApplyDamageSystem()
				.AddDeathSystem()
				.AddDeathProcessTimerSystem()
				.AddSelfReleaseSystem(_entitiesContext);
			
			_entitiesContext.Add(entity);
			
			return entity;
			
		}
	
		public Entity CreateMainCharacterEntity(Vector3 position)
		{
			PlayerConfig config = _configService.GetConfig<PlayerConfig>();

			Entity entity = CreateEmpty();

			MonoEntity monoEntity =
				_monoEntitiesFactory.Create(entity, position, "Prefabs/Gameplay/Entity/MainCharacterMonoEntity");

			entity
				.AddMoveDirection()
				.AddMoveSpeed(new ReactiveVariable<float>(config.MovementConfig.MoveSpeed))
				.AddIsMoving()
				.AddRotateDirection()
				.AddRotateSpeed(new ReactiveVariable<float>(config.MovementConfig.RotationSpeed))
				.AddMaxHealth(new ReactiveVariable<float>(config.HealthConfig.MaxHealth))
				.AddCurrentHealth(new ReactiveVariable<float>(config.HealthConfig.StartHealth))
				.AddIsDead(new ReactiveVariable<bool>(false))
				.AddInDeathProcess(new ReactiveVariable<bool>(false))
				.AddDeathProcessInitialTime(new ReactiveVariable<float>(config.HealthConfig.DeathProcessInitialTime))
				.AddDeathProcessCurrentTime()
				.AddTakeDamageRequest()
				.AddTakeDamageEvent()
				.AddAttackProcessInitialTime(new ReactiveVariable<float>(config.AttackConfig.InitialAttackTime))
				.AddAttackProcessCurrentTime()
				.AddStartAttackRequest()
				.AddStartAttackEvent()
				.AddInAttackProcess()
				.AddAttackDelayTime(new ReactiveVariable<float>(config.AttackConfig.AttackDelayTime))
				.AddAttackDelayEndEvent()
				.AddEndAttackEvent()
				.AddInstantAttackDamage(new ReactiveVariable<float>(config.AttackConfig.Damage))
				.AddAttackCanceledEvent()
				.AddAttackCooldownInitialTime(new ReactiveVariable<float>(config.AttackConfig.AttackCooldownInitialTime))
				.AddAttackCooldownCurrentTime()
				.AddInAttackCooldown(new ReactiveVariable<bool>(false));

			ICompositeCondition canMove = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.IsDead.Value == false));

			ICompositeCondition canRotate = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.IsDead.Value == false));

			ICompositeCondition mustDie = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.CurrentHealth.Value <= 0f));

			ICompositeCondition mustRelease = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.IsDead.Value))
				.AddCondition(new FuncCondition(() => entity.InDeathProcess.Value == false));
			
			ICompositeCondition canApplyDamage = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.IsDead.Value == false));
			
			ICompositeCondition canStartAttack = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.IsDead.Value == false))
				.AddCondition(new FuncCondition(() => entity.InAttackProcess.Value == false))
				.AddCondition(new FuncCondition(() => entity.IsMoving.Value == false))
				.AddCondition(new FuncCondition(() => entity.InAttackCooldown.Value == false));

			ICompositeCondition mustCancelAttack = new CompositeCondition(LogicOperations.Or)
				.AddCondition(new FuncCondition(() => entity.IsMoving.Value))
				.AddCondition(new FuncCondition(() => entity.IsDead.Value));

			entity
				.AddCanMove(canMove)
				.AddCanRotate(canRotate)
				.AddMustDie(mustDie)
				.AddMustSelfRelease(mustRelease)
				.AddCanApplyDamage(canApplyDamage)
				.AddCanStartAttack(canStartAttack)
				.AddMustCancelAttack(mustCancelAttack);

			entity
				.AddRigidbodyMovementSystem()
				.AddRigidbodyRotationSystem()
				.AddStartAttackSystem()
				.AddAttackCancelSystem()
				.AddAttackProcessTimerSystem()
				.AddAttackDelayEndTriggerSystem()
				.AddInstantShootSystem(this)
				.AddEndAttackSystem()
				.AddAttackCooldownTimerSystem()
				.AddApplyDamageSystem()
				.AddDeathSystem()
				.AddDeathProcessTimerSystem()
				.AddSelfReleaseSystem(_entitiesContext);
			
			_entitiesContext.Add(entity);
			
			return entity;
		}
		
		public Entity CreateProjectile(Vector3 position, Vector3 direction, float damage)
		{
			GhostConfig config = _configService.GetConfig<GhostConfig>();

			Entity entity = CreateEmpty();

			MonoEntity monoEntity =
				_monoEntitiesFactory.Create(entity, position, "Prefabs/Gameplay/Entity/Projectiles/Projectile");
			
			Debug.Log(entity.ToString());

			entity
				.AddMoveDirection(new ReactiveVariable<Vector3>(direction))
				.AddMoveSpeed(new ReactiveVariable<float>(config.MovementConfig.MoveSpeed))
				.AddRotateDirection(new ReactiveVariable<Vector3>(direction))
				.AddIsMoving()
				.AddRotateSpeed(new ReactiveVariable<float>(99999))
				.AddIsDead(new ReactiveVariable<bool>(false))
				.AddContactsDetectingMask(1 << LayerMask.NameToLayer("Characters"))
				.AddContactColliderBuffer(new Buffer<Collider>(BufferSize))
				.AddContactEntitiesBuffer(new Buffer<Entity>(BufferSize))
				.AddBodyContactDamage(new ReactiveVariable<float>(damage))
				.AddDeathMask(1 << LayerMask.NameToLayer("Characters"))
				.AddIsTouchDeathMask();

			ICompositeCondition canMove = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.IsDead.Value == false));

			ICompositeCondition canRotate = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.IsDead.Value == false));

			ICompositeCondition mustDie = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.IsTouchDeathMask.Value));

			ICompositeCondition mustRelease = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.IsDead.Value));

			entity
				.AddCanMove(canMove)
				.AddCanRotate(canRotate)
				.AddMustDie(mustDie)
				.AddMustSelfRelease(mustRelease);

			entity
				.AddRigidbodyMovementSystem()
				.AddRigidbodyRotationSystem()
				.AddBodyContactsDetectingSystem()
				.AddBodyContactsEntitiesFilterSystem(_collidersRegistryService)
				.AddDealDamageOnContactSystem()
				.AddDeathMaskTouchDetectorSystem()
				.AddDeathSystem()
				.AddSelfReleaseSystem(_entitiesContext);
			
			_entitiesContext.Add(entity);
			
			return entity;
			
		}
		
		public Entity CreateTeleportGhost(Vector3 position)
		{
			GhostTeleportConfig config = _configService.GetConfig<GhostTeleportConfig>();

			Entity entity = CreateEmpty();

			MonoEntity monoEntity =
				_monoEntitiesFactory.Create(entity, position, "Prefabs/Gameplay/Entity/TeleportGhostMonoEntity");

			entity
				.AddMaxHealth(new ReactiveVariable<float>(config.HealthConfig.MaxHealth))
				.AddCurrentHealth(new ReactiveVariable<float>(config.HealthConfig.StartHealth))
				.AddIsDead(new ReactiveVariable<bool>(false))
				.AddInDeathProcess(new ReactiveVariable<bool>(false))
				.AddDeathProcessInitialTime(new ReactiveVariable<float>(config.HealthConfig.DeathProcessInitialTime))
				.AddDeathProcessCurrentTime()
				.AddTakeDamageRequest()
				.AddTakeDamageEvent()
				.AddCurrentEnergy(new ReactiveVariable<float>(config.EnergyConfig.StartEnergy))
				.AddMaxEnergy(new ReactiveVariable<float>(config.EnergyConfig.MaxEnergy))
				.AddPercentByMaxRegenerationEnergy(new ReactiveVariable<float>(config.EnergyConfig.EnergyRegenerationPercentByMax))
				.AddRegenerationEnergyInitialTime(new ReactiveVariable<float>(config.EnergyConfig.EnergyRegeneretionPeriodInitialTime))
				.AddRegenerationEnergyCurrentTime()
				.AddSpendEnergyRequest()
				.AddSpendEnergyEvent()
				.AddRegenerationEnergyRequest()
				.AddRegenerationEnergyEvent()
				.AddTeleportRadius(new ReactiveVariable<float>(config.TeleportConfig.TeleportRadius))
				.AddTeleportEnergyCost(new ReactiveVariable<float>(config.TeleportConfig.TeleportEnergyCost))
				.AddTeleportEvent()
				.AddTeleportCooldownInitialTime(new ReactiveVariable<float>(config.TeleportConfig.TeleportCooldown))
				.AddTeleportCooldownCurrentTime()
				.AddInTeleportCooldown()
				.AddTeleportDamage(new ReactiveVariable<float>(config.AttackConfig.Damage))
				.AddTeleportTakeDamageRadius(new ReactiveVariable<float>(config.AttackConfig.TeleportTakeDamageRadius))
				.AddContactsDetectingMask(1 << LayerMask.NameToLayer("Characters"))
				.AddTeleportTakeDamageColliderBuffer(new Buffer<Collider>(BufferSize));

			ICompositeCondition mustDie = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.CurrentHealth.Value <= 0f));

			ICompositeCondition mustRelease = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.IsDead.Value))
				.AddCondition(new FuncCondition(() => entity.InDeathProcess.Value == false));
			
			ICompositeCondition canApplyDamage = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.IsDead.Value == false));
			
			ICompositeCondition canSpendEnergy = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.IsDead.Value == false))
				.AddCondition(new FuncCondition(() => entity.InDeathProcess.Value == false));
			
			ICompositeCondition canRegenerateEnergy = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.IsDead.Value == false))
				.AddCondition(new FuncCondition(() => entity.InDeathProcess.Value == false));
			
			ICompositeCondition canTeleport = new  CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.IsDead.Value == false))
				.AddCondition(new FuncCondition(() => entity.InDeathProcess.Value == false))
				.AddCondition(new FuncCondition(
					() => entity.CurrentEnergy.Value
					      >= entity.TeleportEnergyCost.Value))
				.AddCondition(new FuncCondition(() => entity.InTeleportCooldown.Value == false));

			entity
				.AddMustDie(mustDie)
				.AddMustSelfRelease(mustRelease)
				.AddCanApplyDamage(canApplyDamage)
				.AddCanSpendEnergy(canSpendEnergy)
				.AddCanRegenerateEnergy(canRegenerateEnergy)
				.AddCanTeleport(canTeleport);

			entity
				.AddSpendEnergySystem()
				.AddTeleportMovementSystem()
				.AddTeleportCooldownSystem()
				.AddEnergyRegenerationSystem()
				.AddEnergyRegenerationTimerSystem()
				.AddApplyDamageSystem()
				.AddDeathSystem()
				.AddDeathProcessTimerSystem()
				.AddSelfReleaseSystem(_entitiesContext)
				.AddTeleportTakeDamageSystem(_collidersRegistryService);
			
			_entitiesContext.Add(entity);
			
			return entity;
		}

		private Entity CreateEmpty() => new Entity();

	}
}