using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.AI;
using _Project.Develop.Runtime.Gameplay.Features.AI.States;
using _Project.Develop.Runtime.Gameplay.Features.TeamsFeutere;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.ConfigManagement;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.MainHero
{
	public class MainHeroFactory
	{
		private readonly DIContainer _container;
		
		private readonly EntitiesFactory _entitiesFactory;
		private readonly BrainsFactory _brainsFactory;
		private readonly ConfigService _configService;
		private readonly EntitiesLifeContext _entitiesLifeContext;

		public MainHeroFactory(DIContainer container)
		{
			_container = container;

			_entitiesFactory = _container.Resolve<EntitiesFactory>();
			_brainsFactory = _container.Resolve<BrainsFactory>();
			_configService = _container.Resolve<ConfigService>();
			_entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
		}

		public Entity Create(Vector3 position)
		{
			PlayerConfig playerConfig = _configService.GetConfig<PlayerConfig>();

			Entity heroEntity = _entitiesFactory.CreateMainHeroEntity(position, playerConfig);

			heroEntity
				.AddIsMainHero()
				.AddTeam(new ReactiveVariable<Teams>(Teams.MainHero));

			_brainsFactory.CreateMainHeroBrain(heroEntity, new NearestDamageableTargetSelector(heroEntity));
			_entitiesLifeContext.Add(heroEntity);
			
			return heroEntity;
		}
	}
}