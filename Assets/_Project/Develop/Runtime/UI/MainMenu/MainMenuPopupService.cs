using _Project.Develop.Runtime.UI.Presenters;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Core.MainMenu
{
	public class MainMenuPopupService : PopupService
	{
		private MainMenuCanvasRoot _canvasRoot;
		
		public MainMenuPopupService(
			ProjectPresenterFactory projectPresenterFactory,
			ViewFactory viewFactory,
			MainMenuCanvasRoot mainMenuCanvasRoot) : base(projectPresenterFactory, viewFactory)
		{
			_canvasRoot = mainMenuCanvasRoot;
		}

		protected override Transform PopupLayer => _canvasRoot.Popups;
	}
}