using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Sensors
{
	public class BodyCollider : IEntityComponent
	{
		public CapsuleCollider Value;
	}

	public class ContactsDetectingMask : IEntityComponent
	{
		public LayerMask Value;
	}

	public class ContactColliderBuffer : IEntityComponent
	{
		public Buffer<Collider> Value;
	}
	
	public class ContactEntitiesBuffer : IEntityComponent
	{
		public Buffer<Entity> Value;
	}
	
	public class DeathMask : IEntityComponent
	{
		public LayerMask Value;
	}
	
	public class IsTouchDeathMask : IEntityComponent
	{
		public ReactiveVabiable<bool> Value;
	}
}