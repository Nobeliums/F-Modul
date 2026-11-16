using System;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Utilities.StateMachineCore
{
	public interface IState
	{
		IReadOnlyReactiveEvent Entered { get; }
		IReadOnlyReactiveEvent Exited { get; }
		
		void Enter();
		void Exit();
	}
}