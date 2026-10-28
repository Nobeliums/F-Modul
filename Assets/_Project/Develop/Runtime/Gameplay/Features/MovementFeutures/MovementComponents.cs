using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Reactive;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.MovementFeuture
{
	public class SpeedComponent : IEntityComponent
	{
		public ReactiveVabiable<float> Value;
	}

	public class MoveDirection : IEntityComponent
	{
		public ReactiveVabiable<Vector3> Value;
	}
}