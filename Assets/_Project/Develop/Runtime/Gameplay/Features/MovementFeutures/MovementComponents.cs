using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.MovementFeuture
{
	public class MoveSpeed : IEntityComponent
	{
		public ReactiveVabiable<float> Value;
	}

	public class IsMoving : IEntityComponent
	{
		public ReactiveVabiable<bool> Value;
	}

	public class RotateSpeed : IEntityComponent
	{
		public ReactiveVabiable<float> Value;
	}

	public class MoveDirection : IEntityComponent
	{
		public ReactiveVabiable<Vector3> Value;
	}

	public class RotateDirection : IEntityComponent
	{
		public ReactiveVabiable<Vector3> Value;
	}

	public class CanMove : IEntityComponent
	{
		public ICompositeCondition Value;
	}

	public class CanRotate : IEntityComponent
	{
		public ICompositeCondition Value;
	}
}