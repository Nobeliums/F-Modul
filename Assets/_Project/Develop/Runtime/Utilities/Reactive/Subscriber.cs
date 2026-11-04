using System;

namespace _Project.Develop.Runtime.Utilities.Reactive
{
	public class Subscriber<T, K> : IDisposable
	{
		private Action<T, K> _action;
		private Action<Subscriber<T, K>> _removeAction;
		
		public Subscriber(Action<T, K> action, Action<Subscriber<T, K>> removeAction)
		{
			_action = action;
			_removeAction = removeAction;
		}

		public void Invoke(T arg1, K arg2) => _action?.Invoke(arg1, arg2);
		
		public void Dispose() => _removeAction.Invoke(this);
	}
	
	public class Subscriber<T> : IDisposable
	{
		private Action<T> _action;
		private Action<Subscriber<T>> _removeAction;
		
		public Subscriber(Action<T> action, Action<Subscriber<T>> removeAction)
		{
			_action = action;
			_removeAction = removeAction;
		}

		public void Invoke(T arg1) => _action?.Invoke(arg1);
		
		public void Dispose() => _removeAction.Invoke(this);
	}
	
	public class Subscriber : IDisposable
	{
		private Action _action;
		private Action<Subscriber> _removeAction;
		
		public Subscriber(Action action, Action<Subscriber> removeAction)
		{
			_action = action;
			_removeAction = removeAction;
		}

		public void Invoke() => _action?.Invoke();
		
		public void Dispose() => _removeAction.Invoke(this);
	}
}