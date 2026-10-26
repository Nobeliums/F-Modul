using _Project.Develop.Runtime.Features.Gamecycle;
using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.Gameplay.Features.CharsSelector;
using _Project.Develop.Runtime.Gameplay.Features.Gamecycle.Condition;
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
using _Project.Develop.Runtime.Utilities.ResourcesManagement;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
	public class GameplayContextRegistrations
	{
		public static void Process(DIContainer container, GameplayInputArgs gameplayAgrs)
		{
			container.RegistryAsSingle((container) => gameplayAgrs);
			container.RegistryAsSingle(CreateGameCharsSelector);
			container.RegistryAsSingle(CreateGameplayCycle);
			container.RegistryAsSingle(CreateGameplayPresenterFactory);
			container.RegistryAsSingle(CreateGameplayCanvasRoot).NonLazy();
			container.RegistryAsSingle(CreateGameplayPresenter).NonLazy();
			container.RegistryAsSingle(CreateGameplayPopupService);
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

		private static IGameCharsSelector CreateGameCharsSelector(DIContainer c)
		{
			GameModeConfig config = c.Resolve<ConfigService>().GetConfig<GameModeConfig>();

			string sequence = config.GetGameCharsBy(c.Resolve<GameplayInputArgs>().GameCharsSequenceType);

			return new GameCharsSelector(sequence);
		}

		private static GameplayCycle CreateGameplayCycle(DIContainer c)
		{
			IGameCharsSelector charsSelector = c.Resolve<IGameCharsSelector>();
			ConfigService configService = c.Resolve<ConfigService>();
			GameModeConfig config = configService.GetConfig<GameModeConfig>();
			GameStatisticsService gameStatisticsService = c.Resolve<GameStatisticsService>();
			WalletService walletService = c.Resolve<WalletService>();
			PlayerDataProvider playerDataProvider = c.Resolve<PlayerDataProvider>();
			ICoroutinePerformer coroutinePerformer = c.Resolve<ICoroutinePerformer>();
	
			Gamemode gamemode = new Gamemode(new AllCharsEqualsCondition(), new CharNotEqualCondition());

			return new GameplayCycle(gamemode,
				charsSelector,
				config,
				gameStatisticsService,
				walletService,
				playerDataProvider,
				coroutinePerformer);
		}
	}
}