using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.LIfeCycle
{
	public class DisableCollidersOnDeathRegistrator : MonoEntityRegistrator
	{
		[SerializeField] private List<Collider> _colliders;
		
		public override void Register(Entity entity)
		{
			entity.AddComponent(new DisableCollidersOnDeath() { Value = _colliders });
		}
	}
}