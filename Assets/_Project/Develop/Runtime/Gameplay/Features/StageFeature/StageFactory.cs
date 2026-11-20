using System;
using _Project.Develop.Runtime.Gameplay.Configs.Stages;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.Enemies;
using _Project.Develop.Runtime.Infrastructure.DI;

namespace _Project.Develop.Runtime.Gameplay.Features.StageFeature
{
	public class StageFactory
	{
		private readonly DIContainer _container;

		public StageFactory(DIContainer container)
		{
			_container = container;
		}

		public IStage Create(StageConfig config)
		{
			switch (config)
			{
				case ClearAllEnemiesStageConfig clearAllEnemiesStageConfig:
					return new ClearAllEnemiesStage(
						clearAllEnemiesStageConfig,
						_container.Resolve<EnemiesFactory>(),
						_container.Resolve<EntitiesLifeContext>());

				default:
					throw new ArgumentException($"Not support {config.GetType()}");
			}
		}
	}
}
