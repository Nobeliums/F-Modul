using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Utilities.StateMachineCore;

namespace _Project.Develop.Runtime.Gameplay.Features.AI
{
	public class AIStateMachine : StateMachine<IUpdatebleState>
	{
		public AIStateMachine(List<IDisposable> disposables) : base(disposables)
		{
		}

		public AIStateMachine() : base(new()) {}

		protected override void UpdateLogic(float deltaTime)
		{
			base.UpdateLogic(deltaTime);
			
			CurrentState?.Update(deltaTime);
		}
	}
}