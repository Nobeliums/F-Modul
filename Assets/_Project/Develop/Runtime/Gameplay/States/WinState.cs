using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Meta.Features.LevelsProgression;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using _Project.Develop.Runtime.Utilities.Input;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.States
{
	public class WinState : EndGameState, IUpdatebleState
	{
		private LevelsProgressionService _levelsProgressionService;
		private GameplayInputArgs _gameplayInputArgs;
		private PlayerDataProvider _playerDataProvider;
		private SceneSwitcherService _sceneSwitcherService;
		private ICoroutinePerformer _coroutinePerformer;

		public WinState(
			IInputService inputService,
			LevelsProgressionService levelsProgressionService,
			GameplayInputArgs gameplayInputArgs,
			PlayerDataProvider playerDataProvider,
			SceneSwitcherService sceneSwitcherService,
			ICoroutinePerformer coroutinePerformer) : base(inputService)
		{
			_levelsProgressionService = levelsProgressionService;
			_gameplayInputArgs = gameplayInputArgs;
			_playerDataProvider = playerDataProvider;
			_sceneSwitcherService = sceneSwitcherService;
			_coroutinePerformer = coroutinePerformer;
		}

		public override void Enter()
		{
			base.Enter();

			Debug.Log("Победа!");

			_levelsProgressionService.AddLevelToCompleted(_gameplayInputArgs.LevelNumber);
			_coroutinePerformer.StartPerform(_playerDataProvider.SaveAsync());
		}

		public void Update(float deltaTime)
		{
			if (Input.GetKeyDown(KeyCode.Q))
				_coroutinePerformer.StartPerform(_sceneSwitcherService.ProcessSceneSwitchTo(Scenes.MainMenu));
		}
	}
}
