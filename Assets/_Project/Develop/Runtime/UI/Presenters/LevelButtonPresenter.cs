using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Meta.Features.LevelsProgression;
using _Project.Develop.Runtime.UI.View;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.UI.Presenters
{
	public class LevelButtonPresenter : IPresenter
	{
		private readonly LevelsProgressionService _levelsProgressionService;
		private readonly SceneSwitcherService _sceneSwitcherService;
		private readonly ICoroutinePerformer _coroutinePerformer;

		private readonly int _levelNumber;
		private readonly ButtonView _buttonView;

		public LevelButtonPresenter(
			LevelsProgressionService levelsProgressionService,
			SceneSwitcherService sceneSwitcherService,
			ICoroutinePerformer coroutinePerformer,
			int levelNumber,
			ButtonView buttonView)
		{
			_levelsProgressionService = levelsProgressionService;
			_sceneSwitcherService = sceneSwitcherService;
			_coroutinePerformer = coroutinePerformer;
			_levelNumber = levelNumber;
			_buttonView = buttonView;
		}

		public ButtonView ButtonView => _buttonView;

		public void Initialize()
		{
			_buttonView.SetText(_levelNumber.ToString());

			if (_levelsProgressionService.CanPlay(_levelNumber))
			{
				_buttonView.SetAvailable(true);

				if (_levelsProgressionService.IsLevelCompleted(_levelNumber))
					_buttonView.SetActiveColor();
				else
					_buttonView.SetAvailableColor();
			}
			else
			{
				_buttonView.SetAvailable(false);
				_buttonView.SetUnavailableColor();
			}

			_buttonView.ClickEvent += OnClicked;
		}

		public void Dispose()
		{
			_buttonView.ClickEvent -= OnClicked;
		}

		private void OnClicked()
		{
			if (_levelsProgressionService.CanPlay(_levelNumber) == false)
				return;

			GameplayInputArgs args = new GameplayInputArgs(_levelNumber);

			_coroutinePerformer.StartPerform(
				_sceneSwitcherService.ProcessSceneSwitchTo(Scenes.Gameplay, args));
		}
	}
}
