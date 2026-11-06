using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.MovementFeuture
{
	public class MoveSpeed : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class IsMoving : IEntityComponent
	{
		public ReactiveVariable<bool> Value;
	}

	public class RotateSpeed : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class MoveDirection : IEntityComponent
	{
		public ReactiveVariable<Vector3> Value;
	}

	public class RotateDirection : IEntityComponent
	{
		public ReactiveVariable<Vector3> Value;
	}

	public class CanMove : IEntityComponent
	{
		public ICompositeCondition Value;
	}

	public class CanRotate : IEntityComponent
	{
		public ICompositeCondition Value;
	}

	public class TeleportRadius : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class TeleportEnergyCost : IEntityComponent
	{
		public  ReactiveVariable<float> Value;
	}

	public class TeleportEvent : IEntityComponent
	{
		public ReactiveEvent Value;
	}

	public class TeleportCooldownInitialTime : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class TeleportCooldownCurrentTime : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class InTeleportCooldown : IEntityComponent
	{
		public ReactiveVariable<bool> Value;
	}

	public class CanTeleport : IEntityComponent
	{
		public ICompositeCondition Value;
	}
}