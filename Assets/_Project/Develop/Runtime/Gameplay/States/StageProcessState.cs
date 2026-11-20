using _Project.Develop.Runtime.Gameplay.Features.StageFeature;
using _Project.Develop.Runtime.Utilities.StateMachineCore;

namespace _Project.Develop.Runtime.Gameplay.States
{
	public class StageProcessState : State, IUpdatebleState
	{
		private StageProviderService _stageProviderService;

		public StageProcessState(StageProviderService stageProviderService)
		{
			_stageProviderService = stageProviderService;
		}

		public override void Enter()
		{
			base.Enter();

			_stageProviderService.SwitchToNext();
			_stageProviderService.StartCurrent();
		}

		public void Update(float deltaTime)
		{
			_stageProviderService.UpdateCurrent(deltaTime);
		}

		public override void Exit()
		{
			base.Exit();

			_stageProviderService.CleanupCurrent();
		}
	}
}
