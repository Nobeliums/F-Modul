using _Project.Develop.Runtime.Utilities.Input;
using _Project.Develop.Runtime.Utilities.StateMachineCore;

namespace _Project.Develop.Runtime.Gameplay.States
{
	public class EndGameState : State
	{
		private IInputService _inputService;

		public EndGameState(IInputService inputService)
		{
			_inputService = inputService;
		}

		public override void Enter()
		{
			base.Enter();

			_inputService.IsEnabled = false;
		}

		public override void Exit()
		{
			base.Exit();

			_inputService.IsEnabled = true;
		}
	}
}
