using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Project.Develop.Runtime.Gameplay.Features.MovementFeuture;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.EntitiesCore
{
	public class EntitiesFactory
	{
		private readonly DIContainer _container;
		private readonly EntitiesLifeContext _entitiesContext;

		private readonly MonoEntitiesFactory _monoEntitiesFactory;

		public EntitiesFactory(DIContainer container)
		{
			_container = container;
			_monoEntitiesFactory = _container.Resolve<MonoEntitiesFactory>();
			_entitiesContext = _container.Resolve<EntitiesLifeContext>();
		}

		public Entity CreateTestEntity(Vector3 position)
		{
			Entity entity = CreateEmpty();

			MonoEntity monoEntity = _monoEntitiesFactory.Create(entity, position, "Prefabs/Gameplay/Entity/MonoEntity");

			entity
				.AddComponent(new MoveDirection() { Value = new ReactiveVabiable<Vector3>(Vector3.forward) })
				.AddComponent(new SpeedComponent() { Value = new ReactiveVabiable<float>(10) });

			entity .AddSystem(new RigidbodyMovementSystem());
			
			_entitiesContext.Add(entity);

			
			return entity;
		}

		private Entity CreateEmpty() => new Entity();
	}
}