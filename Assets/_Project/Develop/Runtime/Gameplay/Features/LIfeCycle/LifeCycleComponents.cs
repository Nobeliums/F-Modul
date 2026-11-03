using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.LIfeCycle
{
	public class CurrentHealth : IEntityComponent
	{
		public ReactiveVabiable<float> Value;
	}

	public class MaxHealth : IEntityComponent
	{
		public ReactiveVabiable<float> Value;
	}

	public class IsDead : IEntityComponent
	{
		public ReactiveVabiable<bool> Value;
	}
}