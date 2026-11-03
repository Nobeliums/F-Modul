using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Project.Develop.Runtime.Gameplay.Features.LIfeCycle;
using _Project.Develop.Runtime.Gameplay.Features.MovementFeuture;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.ConfigManagement;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.EntitiesCore
{
	public class EntitiesFactory
	{
		private readonly DIContainer _container;
		private readonly EntitiesLifeContext _entitiesContext;

		private readonly MonoEntitiesFactory _monoEntitiesFactory;
		private readonly ConfigService _configService;

		public EntitiesFactory(DIContainer container)
		{
			_container = container;
			_monoEntitiesFactory = _container.Resolve<MonoEntitiesFactory>();
			_entitiesContext = _container.Resolve<EntitiesLifeContext>();
			_configService = _container.Resolve<ConfigService>();
		}

		public Entity CreateRigidbodyTestEntity(Vector3 position)
		{
			PlayerConfig config = _configService.GetConfig<PlayerConfig>();

			Entity entity = CreateEmpty();

			MonoEntity monoEntity =
				_monoEntitiesFactory.Create(entity, position, "Prefabs/Gameplay/Entity/RigidbodyMonoEntity");

			entity
				.AddComponent(new MoveDirection() { Value = new ReactiveVabiable<Vector3>() })
				.AddComponent(new MoveSpeed()
					{ Value = new ReactiveVabiable<float>(config.PlayerMovementConfig.MoveSpeed) })
				.AddComponent(new RotateDirection() { Value = new ReactiveVabiable<Vector3>() })
				.AddComponent(new RotateSpeed()
					{ Value = new ReactiveVabiable<float>(config.PlayerMovementConfig.RotationSpeed) })
				.AddComponent(new MaxHealth()
					{ Value = new ReactiveVabiable<float>(config.PlayerHealthConfig.MaxHealth) })
				.AddComponent(new CurrentHealth()
					{ Value = new ReactiveVabiable<float>(config.PlayerHealthConfig.StartHealth) })
				.AddComponent(new IsDead() { Value = new ReactiveVabiable<bool>(false) })
				.AddComponent(new InDeathProcess() { Value = new ReactiveVabiable<bool>(false) })
				.AddComponent(new DeathProcessInitialTime()
					{ Value = new ReactiveVabiable<float>(config.PlayerHealthConfig.DeathProcessInitialTime) })
				.AddComponent(new DeathProcessCurrentTime() { Value = new ReactiveVabiable<float>() });

			ICompositeCondition canMove = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.GetComponent<IsDead>().Value.Value == false));

			ICompositeCondition canRotate = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.GetComponent<IsDead>().Value.Value == false));

			ICompositeCondition mustDie = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.GetComponent<CurrentHealth>().Value.Value <= 0f));

			ICompositeCondition mustRelease = new CompositeCondition()
				.AddCondition(new FuncCondition(() => entity.GetComponent<IsDead>().Value.Value
				                                      && entity.GetComponent<InDeathProcess>().Value.Value == false));

			entity
				.AddComponent(new CanMove() { Value = canMove })
				.AddComponent(new CanRotate() { Value = canRotate })
				.AddComponent(new MustDie() { Value = mustDie })
				.AddComponent(new MustSelfRelease() { Value = mustRelease });

		entity
				.AddSystem(new RigidbodyMovementSystem())
				.AddSystem(new RigidbodyRotationSystem())
				.AddSystem(new DeathSystem())
				.AddSystem(new DeathProcessTimerSystem())
				.AddSystem(new SelfReleaseSystem(_entitiesContext));
			
			_entitiesContext.Add(entity);

			
			return entity;
		}

		private Entity CreateEmpty() => new Entity();
	}
}