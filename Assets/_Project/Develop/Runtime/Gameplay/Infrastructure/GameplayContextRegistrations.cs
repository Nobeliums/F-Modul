using _Project.Develop.Runtime.Features.Gamecycle;
using _Project.Develop.Runtime.Gameplay.Configs.Levels;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Project.Develop.Runtime.Gameplay.Features.AI;
using _Project.Develop.Runtime.Gameplay.Features.Enemies;
using _Project.Develop.Runtime.Gameplay.Features.MainHero;
using _Project.Develop.Runtime.Gameplay.Features.StageFeature;
using _Project.Develop.Runtime.Gameplay.States;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using UnityEngine;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.GameStatistics;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Core.Gameplay;
using _Project.Develop.Runtime.UI.Presenters;
using _Project.Develop.Runtime.Utilities.ConfigManagement;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using _Project.Develop.Runtime.Utilities.Input;
using _Project.Develop.Runtime.Utilities.ResourcesManagement;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
	public class GameplayContextRegistrations
	{
		public static void Process(DIContainer container, GameplayInputArgs gameplayAgrs)
		{
			container.RegistryAsSingle((container) => gameplayAgrs);
			container.RegistryAsSingle(CreateGameplayCycle);
			container.RegistryAsSingle(CreateGameplayPresenterFactory);
			container.RegistryAsSingle(CreateGameplayCanvasRoot).NonLazy();
			container.RegistryAsSingle(CreateGameplayPresenter).NonLazy();
			container.RegistryAsSingle(CreateGameplayPopupService);
			container.RegistryAsSingle(CreateEntitiesFactory);
			container.RegistryAsSingle(CreateEntitiesLifeContext);
			container.RegistryAsSingle(CreateMonoEntitiesFactory).NonLazy();
			container.RegistryAsSingle(CreateCollidersRegistryService);
			container.RegistryAsSingle(CreateBrainsFactory);
			container.RegistryAsSingle(CreateAIBrainsContext);
			container.RegistryAsSingle(CreateDesktopInputService);
			container.RegistryAsSingle(CreateEnemiesFactory);
			container.RegistryAsSingle(CreateMainHeroFactory);
			container.RegistryAsSingle(CreateStageFactory);
			container.RegistryAsSingle(CreateStageProviderService);
			container.RegistryAsSingle(CreatePreparationTriggerService);
			container.RegistryAsSingle(CreateMainHeroHolderService).NonLazy();
			container.RegistryAsSingle(CreateGameplayStatesFactory);
		}

		private static EnemiesFactory CreateEnemiesFactory(DIContainer c)
			=> new EnemiesFactory(c);

		private static StageFactory CreateStageFactory(DIContainer c)
			=> new StageFactory(c);

		private static StageProviderService CreateStageProviderService(DIContainer c)
		{
			LevelListConfig levelListConfig = c.Resolve<ConfigService>().GetConfig<LevelListConfig>();
			LevelConfig levelConfig = levelListConfig.GetBy(c.Resolve<GameplayInputArgs>().LevelNumber);

			return new StageProviderService(levelConfig, c.Resolve<StageFactory>());
		}

		private static PreparationTriggerService CreatePreparationTriggerService(DIContainer c)
		{
			return new PreparationTriggerService(
				c.Resolve<EntitiesFactory>(),
				c.Resolve<EntitiesLifeContext>());
		}

		private static MainHeroHolderService CreateMainHeroHolderService(DIContainer c)
		{
			return new MainHeroHolderService(c.Resolve<EntitiesLifeContext>());
		}

		private static GameplayStatesFactory CreateGameplayStatesFactory(DIContainer c)
		{
			return new GameplayStatesFactory(c);
		}

		private static MainHeroFactory CreateMainHeroFactory(DIContainer c)
			=> new MainHeroFactory(c);

		private static IInputService CreateDesktopInputService(DIContainer c)
			=> new DesktopInputService();

		private static AIBrainsContext CreateAIBrainsContext(DIContainer c)
			=> new AIBrainsContext();

		private static BrainsFactory CreateBrainsFactory(DIContainer c)
			=> new BrainsFactory(c);

		private static CollidersRegistryService CreateCollidersRegistryService(DIContainer c)
			=> new CollidersRegistryService();

		private static MonoEntitiesFactory CreateMonoEntitiesFactory(DIContainer c)
		{
			return new MonoEntitiesFactory(
				c.Resolve<ResourceLoadService>(),
				c.Resolve<EntitiesLifeContext>(),
				c.Resolve<CollidersRegistryService>());
		}

		private static EntitiesLifeContext CreateEntitiesLifeContext(DIContainer c)
		{
			return new EntitiesLifeContext();
		}


	private static EntitiesFactory CreateEntitiesFactory(DIContainer c)
		{
			return new EntitiesFactory(c);
		}

		private static GameplayPopupService CreateGameplayPopupService(DIContainer c)
			=> new GameplayPopupService(
				c.Resolve<ProjectPresenterFactory>(),
				c.Resolve<ViewFactory>(),
				c.Resolve<GameplayCanvasRoot>(),
				c.Resolve<GameplayPresenterFactory>());

		private static GameplayPresenterFactory CreateGameplayPresenterFactory(DIContainer c)
			=> new GameplayPresenterFactory(c);

		private static GameplayPresenter CreateGameplayPresenter(DIContainer c)
		{
			GameplayCanvasRoot canvasRoot = c.Resolve<GameplayCanvasRoot>();
			
			GameplayView view = c.Resolve<ViewFactory>().Create<GameplayView>(ViewIds.GameplayView, canvasRoot.HUD);
			
			return c.Resolve<GameplayPresenterFactory>().CreateGameplayPresenter(view);
		}

		private static GameplayCanvasRoot CreateGameplayCanvasRoot(DIContainer c)
		{
			ResourceLoadService resourceLoadService = c.Resolve<ResourceLoadService>();
			
			GameplayCanvasRoot canvasRoot = resourceLoadService.Load<GameplayCanvasRoot>("Prefabs/UI/GameplayCanvasRoot");
			
			return Object.Instantiate(canvasRoot);
		}

		private static GameplayCycle CreateGameplayCycle(DIContainer c)
		{
			GameplayStateMachine stateMachine = c.Resolve<GameplayStatesFactory>()
				.CreateGameplayStateMachine(c.Resolve<GameplayInputArgs>());

			return new GameplayCycle(stateMachine);
		}
	}
}