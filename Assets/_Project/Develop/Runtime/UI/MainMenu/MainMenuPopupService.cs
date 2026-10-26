using _Project.Develop.Runtime.UI.Core.Popups;
using _Project.Develop.Runtime.UI.Presenters;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Core.MainMenu
{
	public class MainMenuPopupService : PopupService
	{
		private readonly MainMenuCanvasRoot _canvasRoot;
		private readonly MainMenuPresenterFactory _mainMenuPresenterFactory;

		public MainMenuPopupService(
			ProjectPresenterFactory projectPresenterFactory,
			ViewFactory viewFactory,
			MainMenuCanvasRoot mainMenuCanvasRoot,
			MainMenuPresenterFactory mainMenuPresenterFactory) : base(projectPresenterFactory, viewFactory)
		{
			_canvasRoot = mainMenuCanvasRoot;
			_mainMenuPresenterFactory = mainMenuPresenterFactory;
		}

		protected override Transform PopupLayer => _canvasRoot.Popups;

		public ShopPopupPresenter CreateShopPopup()
		{
			ShopPopupView view = _viewFactory.Create<ShopPopupView>(ViewIds.ShopPopupView, PopupLayer);

			ShopPopupPresenter presenter = _mainMenuPresenterFactory.CreateShopPopupPresenter(view);
			
			OnPopupCreated(presenter, view);
			
			return presenter;
		}
	}
}