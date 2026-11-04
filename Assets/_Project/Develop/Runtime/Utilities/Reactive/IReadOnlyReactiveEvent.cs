using System;

namespace _Project.Develop.Runtime.Utilities.Reactive
{
	public interface IReadOnlyReactiveEvent<T>
	{
		IDisposable Subscribe(Action<T> action);
	}

	public interface IReadOnlyReactiveEvent
	{
		IDisposable Subscribe(Action action);
	}
}