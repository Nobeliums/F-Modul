using System;
using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.Gameplay.Configs.Entities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.AI;
using _Project.Develop.Runtime.Gameplay.Features.TeamsFeutere;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.ConfigManagement;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Enemies
{
	public class EnemiesFactory
	{
		private readonly DIContainer _container;
		
		private readonly EntitiesFactory _entitiesFactory;
		private readonly BrainsFactory _brainsFactory;
		private readonly ConfigService _configService;
		private readonly EntitiesLifeContext _entitiesLifeContext;

		public EnemiesFactory(DIContainer container)
		{
			_container = container;

			_entitiesFactory = _container.Resolve<EntitiesFactory>();
			_brainsFactory = _container.Resolve<BrainsFactory>();
			_configService = _container.Resolve<ConfigService>();
			_entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
		}

		public Entity Create(Vector3 position, EntityConfig config)
		{
			Entity entity;

			switch (config)
			{
				case GhostConfig ghostConfig:
					entity = _entitiesFactory.CreateGhostEntity(position,  ghostConfig);
					_brainsFactory.CreateGhostBrain(entity);
					break;
				
				case GhostTeleportConfig ghostTeleportConfig:
					entity = _entitiesFactory.CreateTeleportGhost(position, ghostTeleportConfig);
					_brainsFactory.CreateGhostTeleportToRandomPointBrain(entity);
					break;
				
				default:
					throw new ArgumentException($"Not suppoort {config.GetType()}");
			}

			entity.AddTeam(new ReactiveVariable<Teams>(Teams.Enemy));

			return entity;
		}
	}
}