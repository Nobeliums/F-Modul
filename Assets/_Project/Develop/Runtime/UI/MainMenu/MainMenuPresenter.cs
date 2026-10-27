using System.Collections.Generic;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.UI.Presenters;

namespace _Project.Develop.Runtime.UI.Core.MainMenu
{
	public class MainMenuPresenter : IPresenter
	{
		private MainMenuView _mainMenuView;

		private ProjectPresenterFactory _factory;
		private MainMenuPopupService _popupService;
		
		private List<IPresenter> _children = new();

		public MainMenuPresenter(
			MainMenuView mainMenuView, 
			ProjectPresenterFactory factory,
			MainMenuPopupService popupService)
		{
			_mainMenuView = mainMenuView;
			_factory = factory;
			_popupService = popupService;
		}
		
		public void Initialize()
		{
			CreateHorizontalGameStatisticsView();

			_mainMenuView.StartGameClick += OpenSelectGameTypePopup;
			_mainMenuView.ShopOpenButtonClick += OpenShopPopup;
			
			foreach(var child in _children)
				child.Initialize();
		}
		
		public void Dispose()
		{
			_mainMenuView.StartGameClick -= OpenSelectGameTypePopup;
			_mainMenuView.ShopOpenButtonClick -= OpenShopPopup;
			
			foreach(var child in _children)
				child.Dispose();
			
			_children.Clear();
		}

		private void CreateHorizontalGameStatisticsView()
		{
			HorizontalGameStatisticsViewPresenter gameStatisticsPresenter = _factory
				.CreateHorizontalGameStatisticsViewPresenter(_mainMenuView.WalletView, _mainMenuView.GameStatisticsView);

			_children.Add(gameStatisticsPresenter);
		}

		private void OpenSelectGameTypePopup()
		{
			_popupService.CreateStartGamePopup();
		}

		private void OpenShopPopup()
		{
			_popupService.CreateShopPopup();
		}
	}
}