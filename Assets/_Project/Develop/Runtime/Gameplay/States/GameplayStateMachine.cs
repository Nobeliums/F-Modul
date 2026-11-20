using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Utilities.StateMachineCore;

namespace _Project.Develop.Runtime.Gameplay.States
{
	public class GameplayStateMachine : StateMachine<IUpdatebleState>
	{
		public GameplayStateMachine(List<IDisposable> disposables) : base(disposables)
		{
		}

		public GameplayStateMachine() : base(new()) {}

		protected override void UpdateLogic(float deltaTime)
		{
			base.UpdateLogic(deltaTime);

			CurrentState?.Update(deltaTime);
		}
	}
}
