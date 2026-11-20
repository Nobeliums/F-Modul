using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.Input;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.States
{
	public class DefeatState : EndGameState, IUpdatebleState
	{
		private SceneSwitcherService _sceneSwitcherService;
		private ICoroutinePerformer _coroutinePerformer;

		public DefeatState(
			IInputService inputService,
			SceneSwitcherService sceneSwitcherService,
			ICoroutinePerformer coroutinePerformer) : base(inputService)
		{
			_sceneSwitcherService = sceneSwitcherService;
			_coroutinePerformer = coroutinePerformer;
		}

		public override void Enter()
		{
			base.Enter();

			Debug.Log("Поражение!");
		}

		public void Update(float deltaTime)
		{
			if (Input.GetKeyDown(KeyCode.Q))
				_coroutinePerformer.StartPerform(_sceneSwitcherService.ProcessSceneSwitchTo(Scenes.MainMenu));
		}
	}
}
