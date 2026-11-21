using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.TeamsFeutere;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Tower
{
	public class TowerFactory
	{
		private readonly EntitiesFactory _entitiesFactory;
		private readonly EntitiesLifeContext _entitiesLifeContext;

		public TowerFactory(DIContainer container)
		{
			_entitiesFactory = container.Resolve<EntitiesFactory>();
			_entitiesLifeContext = container.Resolve<EntitiesLifeContext>();
		}

		public Entity Create(Vector3 position, float maxHealth)
		{
			Entity towerEntity = _entitiesFactory.CreateTowerEntity(position, maxHealth);

			towerEntity
				.AddIsTower()
				.AddTeam(new ReactiveVariable<Teams>(Teams.Player));

			_entitiesLifeContext.Add(towerEntity);

			return towerEntity;
		}
	}
}
