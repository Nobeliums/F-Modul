using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

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

	public class DeathProcessInitialTime : IEntityComponent
	{
		public ReactiveVabiable<float> Value;
	}

	public class DeathProcessCurrentTime : IEntityComponent
	{
		public ReactiveVabiable<float> Value;
	}

	public class InDeathProcess : IEntityComponent
	{
		public ReactiveVabiable<bool> Value;
	}

	public class MustDie : IEntityComponent
	{
		public ICompositeCondition Value;
	}

	public class MustSelfRelease : IEntityComponent
	{
		public ICompositeCondition Value;
	}

	public class DisableCollidersOnDeath : IEntityComponent
	{
		public List<Collider> Value;
	}
}