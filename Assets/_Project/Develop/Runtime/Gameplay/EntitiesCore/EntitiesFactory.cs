using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Project.Develop.Runtime.Gameplay.Features.MovementFeuture;
using _Project.Develop.Runtime.Infrastructure.DI;
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

		public Entity CreateTestEntity(Vector3 position)
		{
			PlayerRigidbodyMovementConfig config = _configService.GetConfig<PlayerRigidbodyMovementConfig>();
			
			Entity entity = CreateEmpty();

			MonoEntity monoEntity = _monoEntitiesFactory.Create(entity, position, "Prefabs/Gameplay/Entity/MonoEntity");

			entity
				.AddComponent(new MoveDirection() { Value = new ReactiveVabiable<Vector3>(Vector3.forward) })
				.AddComponent(new SpeedComponent() { Value = new ReactiveVabiable<float>(config.MoveSpeed) })
				.AddComponent(new RotateDirection() { Value = new ReactiveVabiable<Quaternion>(Quaternion.identity) })
				.AddComponent(new RotateSpeedComponent() { Value = new ReactiveVabiable<float>(config.RotationSpeed) });

			entity
				.AddSystem(new RigidbodyMovementSystem())
				.AddSystem(new RigidbodyRotationSystem());
			
			_entitiesContext.Add(entity);

			
			return entity;
		}

		private Entity CreateEmpty() => new Entity();
	}
}