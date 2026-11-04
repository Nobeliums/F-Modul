using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Sensors
{
	public class CapsuleColliderEntityRegistrator : MonoEntityRegistrator
	{
		[SerializeField] private CapsuleCollider _collider;
		
		public override void Register(Entity entity)
		{
			entity.AddComponent(new BodyCollider() { Value = _collider });
		}
	}
}