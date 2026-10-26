using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Configs;
using _Project.Develop.Runtime.Meta.Features.GameStatistics;
using _Project.Develop.Runtime.Meta.Features.MainMenuCycle;
using _Project.Develop.Runtime.Meta.Features.ShopManagement;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Core.MainMenu;
using _Project.Develop.Runtime.Utilities.ConfigManagement;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using _Project.Develop.Runtime.Utilities.ResourcesManagement;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Infrastructure
{
	public static class MainMenuContextRegistrations
	{
		public static void Process(DIContainer container)
		{
			Debug.Log("[MainMenuContextRegistrations] Process");

			container.RegistryAsSingle(CreateMainMenuCycle);
			container.RegistryAsSingle(CreateShopService);
			container.RegistryAsSingle(CreateMainMenuCanvasRoot).NonLazy();
			container.RegistryAsSingle(CreateMainMenuPresenterFactory);
			container.RegistryAsSingle(CreateMainMenuPresenter).NonLazy();
		}

		private static MainMenuPresenter CreateMainMenuPresenter(DIContainer c)
		{
			MainMenuView mainMenuView = c.Resolve<ViewFactory>()
				.Create<MainMenuView>(
					ViewIds.MainMenuView,
					c.Resolve<MainMenuCanvasRoot>().HUD);
			
			return c.Resolve<MainMenuPresenterFactory>().CreateMainMenuPresenter(mainMenuView);
		}

		private static MainMenuPresenterFactory CreateMainMenuPresenterFactory(DIContainer c)
			=> new MainMenuPresenterFactory(c);

		private static MainMenuCanvasRoot CreateMainMenuCanvasRoot(DIContainer container)
		{
			ResourceLoadService resourceLoadService = container.Resolve<ResourceLoadService>();
			
			MainMenuCanvasRoot canvasRoot = resourceLoadService.Load<MainMenuCanvasRoot>("Prefabs/UI/MainMenuCanvasRoot");
			
			return Object.Instantiate(canvasRoot);
		}
		
		private static ShopService CreateShopService(DIContainer c)
			=> new ShopService(c.Resolve<WalletService>(), c.Resolve<ConfigService>().GetConfig<ShopConfig>());

		private static MainMenuCycle CreateMainMenuCycle(DIContainer c)
		{
			ICoroutinePerformer coroutinePerformer = c.Resolve<ICoroutinePerformer>();
			SceneSwitcherService sceneSwitcherService = c.Resolve<SceneSwitcherService>();
			GameStatisticsService gameStatisticsService = c.Resolve<GameStatisticsService>();
			WalletService walletService = c.Resolve<WalletService>();
			ShopService shopService = c.Resolve<ShopService>();
			PlayerDataProvider playerDataProvider = c.Resolve<PlayerDataProvider>();

			return new MainMenuCycle(
				coroutinePerformer,
				sceneSwitcherService,
				gameStatisticsService,
				walletService,
				shopService,
				playerDataProvider);
		}
	}
}