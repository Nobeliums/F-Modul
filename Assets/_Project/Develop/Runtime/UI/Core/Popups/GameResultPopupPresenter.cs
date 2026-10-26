using _Project.Develop.Runtime.Features.Gamecycle;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.DataManagment;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.UI.Core.Popups
{
	public class GameResultPopupPresenter : PopupBasePresenter
	{
		private const string GameWinResult = "You Won!";
		private const string GameLoseResult = "You Lose!";
		private const string GameLoseCloseButtonText = "Restart";
		private const string GameWinCloseButtonText = "Main Menu";
		
		private readonly GameResultPopupView _gameResultPopup;
		private readonly GameplayCycle _gameplayCycle;
		private readonly GameplayInputArgs _gameplayAgr;

		private readonly ViewFactory _viewFactory;
		private readonly SceneSwitcherService _sceneSwitcher;
		
		public GameResultPopupPresenter(
			GameResultPopupView gameResultPopup,
			GameplayCycle gameplayCycle,
			ICoroutinePerformer coroutinePerformer,
			ViewFactory viewFactory,
			SceneSwitcherService sceneSwitcher,
			GameplayInputArgs gameplayAgr) : base(coroutinePerformer)
		{
			_gameResultPopup = gameResultPopup;
			_gameplayCycle = gameplayCycle;
			_viewFactory = viewFactory;
			_sceneSwitcher = sceneSwitcher;
			_gameplayAgr = gameplayAgr;
		}

		protected override PopupBaseView View => _gameResultPopup;

		public override void Initialize()
		{
			base.Initialize();

			_gameResultPopup.GameEndButton.ClickEvent += OnGameEndClicked;

			if (_gameplayCycle.IsWin)
			{
				OnGameWin();
			}
			else
			{
				OnLoseGame();
			}
		}

		public override void Dispose()
		{
			base.Dispose();
			
			_gameResultPopup.GameEndButton.ClickEvent -= OnGameEndClicked;
			
			_viewFactory.Release(_gameResultPopup);
		}

		private void OnLoseGame()
		{
			_gameResultPopup.SetGameResult(GameLoseResult);
			_gameResultPopup.GameEndButton.SetText(GameLoseCloseButtonText);
		}

		private void OnGameWin()
		{
			_gameResultPopup.SetGameResult(GameWinResult);
			_gameResultPopup.GameEndButton.SetText(GameWinCloseButtonText);
		}

		private void OnGameEndClicked()
		{
			if (_gameplayCycle.IsWin)
			{
				_coroutinePerformer.StartPerform(_sceneSwitcher.ProcessSceneSwitchTo(Scenes.MainMenu));
			}
			else
			{
				_coroutinePerformer.StartPerform(_sceneSwitcher.ProcessSceneSwitchTo(Scenes.Gameplay, _gameplayAgr));
			}
		}
	}
}