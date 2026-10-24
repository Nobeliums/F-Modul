using System;

namespace _Project.Develop.Runtime.Utilities.Reactive
{
	public interface IReadOnlyReactiveVariable<T>
	{
		T Value { get; }
		
		IDisposable Subscribe(Action<T, T> action);
	}
}