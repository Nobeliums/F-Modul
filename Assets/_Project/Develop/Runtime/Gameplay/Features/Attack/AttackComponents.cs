using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Attack
{
	public class StartAttackRequest : IEntityComponent
	{
		public ReactiveEvent Value;
	}

	public class StartAttackEvent : IEntityComponent
	{
		public ReactiveEvent Value;
	}

	public class CanStartAttack : IEntityComponent
	{
		public ICompositeCondition Value;
	}

	public class AttackProcessInitialTime : IEntityComponent
	{
		public ReactiveVabiable<float> Value;
	}

	public class AttackProcessCurrentTime : IEntityComponent
	{
		public ReactiveVabiable<float> Value;
	}

	public class InAttackProcess : IEntityComponent
	{
		public ReactiveVabiable<bool> Value;
	}

	public class AttackDelayTime : IEntityComponent
	{
		public ReactiveVabiable<float> Value;
	}
	
	public class AttackDelayEndEvent : IEntityComponent
	{
		public ReactiveEvent Value;
	}

	public class EndAttackEvent : IEntityComponent
	{
		public ReactiveEvent Value;
	}

	public class InstantAttackDamage : IEntityComponent
	{
		public ReactiveVabiable<float> Value;
	}

	public class ShootPoint : IEntityComponent
	{
		public Transform Value;
	}

	public class MustCancelAttack : IEntityComponent
	{
		public ICompositeCondition Value;
	}

	public class AttackCanceledEvent : IEntityComponent
	{
		public ReactiveEvent Value;
	}

	public class AttackCooldownInitialTime : IEntityComponent
	{
		public ReactiveVabiable<float> Value;
	}

	public class AttackCooldownCurrentTime : IEntityComponent
	{
		public ReactiveVabiable<float> Value;
	}

	public class InAttackCooldown : IEntityComponent
	{
		public ReactiveVabiable<bool> Value;
	}
}