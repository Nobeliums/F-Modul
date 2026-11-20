using _Project.Develop.Runtime.Gameplay.Features.StageFeature;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.States
{
	public class PreparationState : State, IUpdatebleState
	{
		private PreparationTriggerService _preparationTriggerService;

		public PreparationState(PreparationTriggerService preparationTriggerService)
		{
			_preparationTriggerService = preparationTriggerService;
		}

		public override void Enter()
		{
			base.Enter();

			_preparationTriggerService.Create(Vector3.forward * 4);
		}

		public void Update(float deltaTime)
		{
			_preparationTriggerService.Update(deltaTime);
		}

		public override void Exit()
		{
			base.Exit();

			_preparationTriggerService.Cleanup();
		}
	}
}
