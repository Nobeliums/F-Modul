using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.LIfeCycle
{
	public class CurrentHealth : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class MaxHealth : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class IsDead : IEntityComponent
	{
		public ReactiveVariable<bool> Value;
	}

	public class DeathProcessInitialTime : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class DeathProcessCurrentTime : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class InDeathProcess : IEntityComponent
	{
		public ReactiveVariable<bool> Value;
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