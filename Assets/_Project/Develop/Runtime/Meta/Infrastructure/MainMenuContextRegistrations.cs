using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Configs;
using _Project.Develop.Runtime.Meta.Features.ShopManagement;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Core.MainMenu;
using _Project.Develop.Runtime.UI.Presenters;
using _Project.Develop.Runtime.Utilities.ConfigManagement;
using _Project.Develop.Runtime.Utilities.ResourcesManagement;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Infrastructure
{
	public static class MainMenuContextRegistrations
	{
		public static void Process(DIContainer container)
		{
			Debug.Log("[MainMenuContextRegistrations] Process");

			container.RegistryAsSingle(CreateShopService);
			container.RegistryAsSingle(CreateMainMenuCanvasRoot).NonLazy();
			container.RegistryAsSingle(CreateMainMenuPresenterFactory);
			container.RegistryAsSingle(CreateMainMenuPresenter).NonLazy();
			container.RegistryAsSingle(CreateMainMenuPopupService);
		}

		private static MainMenuPopupService CreateMainMenuPopupService(DIContainer c)
		{
			return new MainMenuPopupService(
				c.Resolve<ProjectPresenterFactory>(),
				c.Resolve<ViewFactory>(),
				c.Resolve<MainMenuCanvasRoot>(),
				c.Resolve<MainMenuPresenterFactory>());
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
	}
}