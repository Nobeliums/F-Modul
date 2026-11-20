using System;
using _Project.Develop.Runtime.Gameplay.States;

namespace _Project.Develop.Runtime.Features.Gamecycle
{
	public class GameplayCycle : IDisposable
	{
		private readonly GameplayStateMachine _stateMachine;

		public GameplayCycle(GameplayStateMachine stateMachine)
		{
			_stateMachine = stateMachine;
		}

		public void Run()
		{
			_stateMachine.Enter();
		}

		public void Update(float deltaTime)
		{
			_stateMachine.Update(deltaTime);
		}

		public void Dispose()
		{
			_stateMachine.Dispose();
		}
	}
}
