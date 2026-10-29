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

		public Entity CreateCharacterControllerTestEntity(Vector3 position)
		{
			PlayerMovementConfig config = _configService.GetConfig<PlayerMovementConfig>();
			
			Entity entity = CreateEmpty();

			MonoEntity monoEntity = _monoEntitiesFactory.Create(entity, position, "Prefabs/Gameplay/Entity/CharacterControllerMonoEntity");

			entity
				.AddComponent(new MoveDirection() { Value = new ReactiveVabiable<Vector3>(Vector3.forward) })
				.AddComponent(new SpeedComponent() { Value = new ReactiveVabiable<float>(config.MoveSpeed) })
				.AddComponent(new RotateDirection() { Value = new ReactiveVabiable<Quaternion>(Quaternion.identity) })
				.AddComponent(new RotateSpeedComponent() { Value = new ReactiveVabiable<float>(config.RotationSpeed) });

			entity
				.AddSystem(new CharacterControllerMovementSystem())
				.AddSystem(new CharacterControllerRotationSystem());
			
			_entitiesContext.Add(entity);

			
			return entity;
		}

		public Entity CreateRigidbodyTestEntity(Vector3 position)
		{
			PlayerMovementConfig config = _configService.GetConfig<PlayerMovementConfig>();
			
			Entity entity = CreateEmpty();

			MonoEntity monoEntity = _monoEntitiesFactory.Create(entity, position, "Prefabs/Gameplay/Entity/RigidbodyMonoEntity");

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