using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Utilities.StateMachineCore
{
	public abstract class State : IState
	{
		private ReactiveEvent _entered = new();
		private ReactiveEvent _exited = new();
		
		public IReadOnlyReactiveEvent Entered =>  _entered;
		public IReadOnlyReactiveEvent Exited => _exited;

		public virtual void Enter()
		{
			_entered.Invoke();
		}

		public virtual void Exit()
		{
			_exited.Invoke();
		}
	}
}