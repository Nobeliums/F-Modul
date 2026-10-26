using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.GameStatistics;
using _Project.Develop.Runtime.Meta.Features.ShopManagement;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.UI.Core.Popups;
using _Project.Develop.Runtime.UI.Presenters;
using _Project.Develop.Runtime.UI.View;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProvider;

namespace _Project.Develop.Runtime.UI.Core.MainMenu
{
	public class MainMenuPresenterFactory
	{
		private DIContainer _container;

		public MainMenuPresenterFactory(DIContainer container)
		{
			_container = container;
		}
		
		public MainMenuPresenter CreateMainMenuPresenter(MainMenuView mainMenuView)
		{
			return new MainMenuPresenter(
				mainMenuView,  
				_container.Resolve<ProjectPresenterFactory>(),
				_container.Resolve<MainMenuPopupService>());
		}

		public ShopItemButtonPresenter CreateShopItemButtonPresenter(
			Action callback,
			ButtonView  buttonView,
			ShopItemType shopItemTypeToAction)
		{
			return new ShopItemButtonPresenter(
				callback,
				buttonView,
				_container.Resolve<ShopService>(),
				shopItemTypeToAction
			);
		}

		public ShopPopupPresenter CreateShopPopupPresenter(ShopPopupView shopPopupView)
		{
			Dictionary<ShopItemType, Action> shopItemTypeToAction = new()
			{
				{ ShopItemType.ResetStatistics, _container.Resolve<GameStatisticsService>().ResetStatistics },
			};
	
			return new ShopPopupPresenter(
				shopPopupView,
				_container.Resolve<ICoroutinePerformer>(),
				_container.Resolve<ViewFactory>(),
				_container.Resolve<ShopService>(),
				_container.Resolve<GameStatisticsService>(),
				_container.Resolve<MainMenuPresenterFactory>(),
				shopItemTypeToAction,
				_container.Resolve<WalletService>(),
				_container.Resolve<PlayerDataProvider>());
		}
	}
}