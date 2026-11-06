using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.TeleportTakeDamage
{
	public class TeleportDamage : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class TeleportTakeDamageRadius : IEntityComponent
	{
		public ReactiveVariable<float> Value;
	}

	public class TeleportTakeDamageColliderBuffer : IEntityComponent
	{
		public Buffer<Collider> Value;
	}
}