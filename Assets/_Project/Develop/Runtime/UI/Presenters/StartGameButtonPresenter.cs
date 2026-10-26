using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.UI.View;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using Unity.VisualScripting;

namespace _Project.Develop.Runtime.UI.Presenters
{
	public class StartGameButtonPresenter : IPresenter
	{
		private SceneSwitcherService _sceneSwitcherService;
		private ICoroutinePerformer _coroutinePerformer;

		private readonly string _gameCharsType;
		private readonly GameCharsSequenceType _gameCharsSequenceType;
		private ButtonView _buttonView;

		public StartGameButtonPresenter(
			GameCharsSequenceType gameCharsSequenceType,
			SceneSwitcherService sceneSwitcherService,
			ICoroutinePerformer coroutinePerformer,
			ButtonView buttonView)
		{
			_sceneSwitcherService = sceneSwitcherService;
			_coroutinePerformer = coroutinePerformer;
			_gameCharsType = gameCharsSequenceType.ToString();
			_gameCharsSequenceType =  gameCharsSequenceType;
			_buttonView = buttonView;
		}
		
		public ButtonView ButtonView => _buttonView;

		public void Initialize()
		{
			_buttonView.SetText(_gameCharsType);

			_buttonView.ClickEvent += StartGame;
		}

		public void Dispose()
		{
			_buttonView.ClickEvent -= StartGame;
		}

		private void StartGame()
		{
			GameplayInputArgs args = new GameplayInputArgs(_gameCharsSequenceType);

			_buttonView.ClickEvent += () => _coroutinePerformer.StartPerform(
				_sceneSwitcherService
					.ProcessSceneSwitchTo(Scenes.Gameplay, args));
		}
	}
}