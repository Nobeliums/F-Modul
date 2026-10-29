using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Common
{
	public class RigidbodyEntityRegistrator : MonoEntityRegistrator
	{
		public override void Register(Entity entity)
		{
			entity.AddComponent(new RigidbodyComponent() { Value = GetComponent<Rigidbody>() });
		}
	}
}