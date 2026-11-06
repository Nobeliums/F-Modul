using System;
using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Project.Develop.Runtime.Gameplay.Features.ApplyDamage;
using _Project.Develop.Runtime.Gameplay.Features.Attack;
using _Project.Develop.Runtime.Gameplay.Features.ContactTakeDamage;
using _Project.Develop.Runtime.Gameplay.Features.LIfeCycle;
using _Project.Develop.Runtime.Gameplay.Features.MovementFeuture;
using _Project.Develop.Runtime.Gameplay.Features.Sensors;
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
				.AddComponent(new MoveDirection() { Value = new ReactiveVabiable<Vector3>() })
				.AddComponent(new MoveSpeed()
					{ Value = new ReactiveVabiable<float>(config.MovementConfig.MoveSpeed) })
				.AddComponent(new RotateDirection() { Value = new ReactiveVabiable<Vector3>() })
				.AddComponent(new IsMoving() { Value = new ReactiveVabiable<bool>() } )
				.AddComponent(new RotateSpeed()
					{ Value = new ReactiveVabiable<float>(config.MovementConfig.RotationSpeed) })
				.AddComponent(new MaxHealth()
					{ Value = new ReactiveVabiable<float>(config.HealthConfig.MaxHealth) })
				.AddComponent(new CurrentHealth()
					{ Value = new ReactiveVabiable<float>(config.HealthConfig.StartHealth) })
				.AddComponent(new IsDead() { Value = new ReactiveVabiable<bool>(false) })
				.AddComponent(new InDeathProcess() { Value = new ReactiveVabiable<bool>(false) })
				.AddComponent(new DeathProcessInitialTime()
					{ Value = new ReactiveVabiable<float>(config.HealthConfig.DeathProcessInitialTime) })
				.AddComponent(new DeathProcessCurrentTime() { Value = new ReactiveVabiable<float>() })
				.AddComponent(new TakeDamageRequest() { Value = new ReactiveEvent<float>() })
				.AddComponent(new TakeDamageEvent() { Value = new ReactiveEvent<float>() })
				.AddComponent(new ContactsDetectingMask() { Value = 1 << LayerMask.NameToLayer("Characters") })
				.AddComponent(new ContactColliderBuffer() { Value = new Buffer<Collider>(BufferSize) })
				.AddComponent(new ContactEntitiesBuffer() { Value = new Buffer<Entity>(BufferSize) })
				.AddComponent(new BodyContactDamage() { Value = new ReactiveVabiable<float>(config.AttackConfig.Damage) });

			ICompositeCondition canMove = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.GetComponent<IsDead>().Value.Value == false));

			ICompositeCondition canRotate = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.GetComponent<IsDead>().Value.Value == false));

			ICompositeCondition mustDie = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.GetComponent<CurrentHealth>().Value.Value <= 0f));

			ICompositeCondition mustRelease = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.GetComponent<IsDead>().Value.Value))
				.AddCondition(new FuncCondition(() => entity.GetComponent<InDeathProcess>().Value.Value == false));
			
			ICompositeCondition canApplyDamage = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.GetComponent<IsDead>().Value.Value == false));

			entity
				.AddComponent(new CanMove() { Value = canMove })
				.AddComponent(new CanRotate() { Value = canRotate })
				.AddComponent(new MustDie() { Value = mustDie })
				.AddComponent(new MustSelfRelease() { Value = mustRelease })
				.AddComponent(new CanApplyDamage() {Value = canApplyDamage});

			entity
				.AddSystem(new RigidbodyMovementSystem())
				.AddSystem(new RigidbodyRotationSystem())
				.AddSystem(new BodyContactsDetectingSystem())
				.AddSystem(new BodyContactsEntitiesFilterSystem(_collidersRegistryService))
				.AddSystem(new DealDamageOnContactSystem())
				.AddSystem(new ApplyDamageSystem())
				.AddSystem(new DeathSystem())
				.AddSystem(new DeathProcessTimerSystem())
				.AddSystem(new SelfReleaseSystem(_entitiesContext));
			
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
				.AddComponent(new MoveDirection() { Value = new ReactiveVabiable<Vector3>() })
				.AddComponent(new MoveSpeed()
					{ Value = new ReactiveVabiable<float>(config.MovementConfig.MoveSpeed) })
				.AddComponent(new IsMoving() { Value = new ReactiveVabiable<bool>() } )
				.AddComponent(new RotateDirection() { Value = new ReactiveVabiable<Vector3>() })
				.AddComponent(new RotateSpeed()
					{ Value = new ReactiveVabiable<float>(config.MovementConfig.RotationSpeed) })
				.AddComponent(new MaxHealth()
					{ Value = new ReactiveVabiable<float>(config.HealthConfig.MaxHealth) })
				.AddComponent(new CurrentHealth()
					{ Value = new ReactiveVabiable<float>(config.HealthConfig.StartHealth) })
				.AddComponent(new IsDead() { Value = new ReactiveVabiable<bool>(false) })
				.AddComponent(new InDeathProcess() { Value = new ReactiveVabiable<bool>(false) })
				.AddComponent(new DeathProcessInitialTime()
					{ Value = new ReactiveVabiable<float>(config.HealthConfig.DeathProcessInitialTime) })
				.AddComponent(new DeathProcessCurrentTime() { Value = new ReactiveVabiable<float>() })
				.AddComponent(new TakeDamageRequest() { Value = new ReactiveEvent<float>() })
				.AddComponent(new TakeDamageEvent() { Value = new ReactiveEvent<float>() })
				.AddComponent(new AttackProcessInitialTime()
					{ Value = new ReactiveVabiable<float>(config.AttackConfig.InitialAttackTime) })
				.AddComponent(new AttackProcessCurrentTime() { Value = new ReactiveVabiable<float>() })
				.AddComponent(new StartAttackRequest() { Value = new ReactiveEvent() })
				.AddComponent(new StartAttackEvent() { Value = new ReactiveEvent() })
				.AddComponent(new InAttackProcess() { Value = new ReactiveVabiable<bool>() })
				.AddComponent(new AttackDelayTime() { Value = new ReactiveVabiable<float>(config.AttackConfig.AttackDelayTime) } )
				.AddComponent(new AttackDelayEndEvent() { Value = new ReactiveEvent() })
				.AddComponent(new EndAttackEvent() { Value = new ReactiveEvent() })
				.AddComponent(new InstantAttackDamage() { Value = new ReactiveVabiable<float>(config.AttackConfig.Damage) })
				.AddComponent(new AttackCanceledEvent() { Value = new ReactiveEvent() })
				.AddComponent(new AttackCooldownInitialTime() { Value = new ReactiveVabiable<float>(config.AttackConfig.AttackCooldownInitialTime) } )
				.AddComponent(new AttackCooldownCurrentTime() { Value =  new ReactiveVabiable<float>() } )
				.AddComponent(new InAttackCooldown() { Value = new ReactiveVabiable<bool>(false) } );

			ICompositeCondition canMove = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.GetComponent<IsDead>().Value.Value == false));

			ICompositeCondition canRotate = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.GetComponent<IsDead>().Value.Value == false));

			ICompositeCondition mustDie = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.GetComponent<CurrentHealth>().Value.Value <= 0f));

			ICompositeCondition mustRelease = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.GetComponent<IsDead>().Value.Value))
				.AddCondition(new FuncCondition(() => entity.GetComponent<InDeathProcess>().Value.Value == false));
			
			ICompositeCondition canApplyDamage = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.GetComponent<IsDead>().Value.Value == false));
			
			ICompositeCondition canStartAttack = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.GetComponent<IsDead>().Value.Value == false))
				.AddCondition(new FuncCondition(() => entity.GetComponent<InAttackProcess>().Value.Value == false))
				.AddCondition(new FuncCondition(() => entity.GetComponent<IsMoving>().Value.Value == false))
				.AddCondition(new FuncCondition(() => entity.GetComponent<InAttackCooldown>().Value.Value == false));

			ICompositeCondition mustCancelAttack = new CompositeCondition(LogicOperations.Or)
				.AddCondition(new FuncCondition(() => entity.GetComponent<IsMoving>().Value.Value))
				.AddCondition(new FuncCondition(() => entity.GetComponent<IsDead>().Value.Value));

			entity
				.AddComponent(new CanMove() { Value = canMove })
				.AddComponent(new CanRotate() { Value = canRotate })
				.AddComponent(new MustDie() { Value = mustDie })
				.AddComponent(new MustSelfRelease() { Value = mustRelease })
				.AddComponent(new CanApplyDamage() { Value = canApplyDamage })
				.AddComponent(new CanStartAttack() { Value = canStartAttack })
				.AddComponent(new MustCancelAttack() { Value = mustCancelAttack });

			entity
				.AddSystem(new RigidbodyMovementSystem())
				.AddSystem(new RigidbodyRotationSystem())
				.AddSystem(new StartAttackSystem())
				.AddSystem(new AttackCancelSystem())
				.AddSystem(new AttackProcessTimerSystem())
				.AddSystem(new AttackDelayEndTriggerSystem())
				.AddSystem(new InstantShootSystem(this))
				.AddSystem(new EndAttackSystem())
				.AddSystem(new AttackCooldownTimerSystem())
				.AddSystem(new ApplyDamageSystem())
				.AddSystem(new DeathSystem())
				.AddSystem(new DeathProcessTimerSystem())
				.AddSystem(new SelfReleaseSystem(_entitiesContext));
			
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
				.AddComponent(new MoveDirection() { Value = new ReactiveVabiable<Vector3>(direction) })
				.AddComponent(new MoveSpeed()
					{ Value = new ReactiveVabiable<float>(config.MovementConfig.MoveSpeed) })
				.AddComponent(new RotateDirection() { Value = new ReactiveVabiable<Vector3>(direction) })
				.AddComponent(new IsMoving() { Value = new ReactiveVabiable<bool>() } )
				.AddComponent(new RotateSpeed()
					{ Value = new ReactiveVabiable<float>(99999) })
				.AddComponent(new IsDead() { Value = new ReactiveVabiable<bool>(false) })
				.AddComponent(new ContactsDetectingMask() { Value = 1 << LayerMask.NameToLayer("Characters") })
				.AddComponent(new ContactColliderBuffer() { Value = new Buffer<Collider>(BufferSize) })
				.AddComponent(new ContactEntitiesBuffer() { Value = new Buffer<Entity>(BufferSize) })
				.AddComponent(new BodyContactDamage() { Value = new ReactiveVabiable<float>(damage) })
				.AddComponent(new DeathMask() { Value = 1 << LayerMask.NameToLayer("Characters") })
				.AddComponent(new IsTouchDeathMask() { Value = new ReactiveVabiable<bool>() });

			ICompositeCondition canMove = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.GetComponent<IsDead>().Value.Value == false));

			ICompositeCondition canRotate = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.GetComponent<IsDead>().Value.Value == false));

			ICompositeCondition mustDie = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.GetComponent<IsTouchDeathMask>().Value.Value));

			ICompositeCondition mustRelease = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.GetComponent<IsDead>().Value.Value));

			entity
				.AddComponent(new CanMove() { Value = canMove })
				.AddComponent(new CanRotate() { Value = canRotate })
				.AddComponent(new MustDie() { Value = mustDie })
				.AddComponent(new MustSelfRelease() { Value = mustRelease });

			entity
				.AddSystem(new RigidbodyMovementSystem())
				.AddSystem(new RigidbodyRotationSystem())
				.AddSystem(new BodyContactsDetectingSystem())
				.AddSystem(new BodyContactsEntitiesFilterSystem(_collidersRegistryService))
				.AddSystem(new DealDamageOnContactSystem())
				.AddSystem(new DeathMaskTouchDetectorSystem())
				.AddSystem(new DeathSystem())
				.AddSystem(new SelfReleaseSystem(_entitiesContext));
			
			_entitiesContext.Add(entity);
			
			return entity;
			
		}


		private Entity CreateEmpty() => new Entity();
	}
}