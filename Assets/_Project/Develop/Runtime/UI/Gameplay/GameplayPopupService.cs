using _Project.Develop.Runtime.UI.Core.Popups;
using _Project.Develop.Runtime.UI.Presenters;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Core.Gameplay
{
	public class GameplayPopupService : PopupService
	{
		private GameplayCanvasRoot _gameplayCanvasRoot;
		private GameplayPresenterFactory _gameplayPresenterFactory;
		
		public GameplayPopupService(
			ProjectPresenterFactory projectPresenterFactory,
			ViewFactory viewFactory,
			GameplayCanvasRoot gameplayCanvasRoot,
			GameplayPresenterFactory gameplayPresenterFactory) : base(projectPresenterFactory, viewFactory)
		{
			_gameplayCanvasRoot = gameplayCanvasRoot;
			_gameplayPresenterFactory = gameplayPresenterFactory;
		}

		protected override Transform PopupLayer => _gameplayCanvasRoot.Popups;

		public GameResultPopupPresenter CreateGameResultPopup()
		{
			GameResultPopupView view = _viewFactory.Create<GameResultPopupView>(ViewIds.GameResultPopupView, PopupLayer);

			GameResultPopupPresenter presenter = _gameplayPresenterFactory.CreateGameResultPopupPresenter(view);
			
			OnPopupCreated(presenter, view);
			
			return presenter;
		}
	}
}