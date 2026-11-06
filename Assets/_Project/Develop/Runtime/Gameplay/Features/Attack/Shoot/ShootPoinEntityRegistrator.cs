using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Attack
{
	public class ShootPoinEntityRegistrator : MonoEntityRegistrator
	{
		[SerializeField] private Transform _shootPoint;
		
		public override void Register(Entity entity)
		{
			entity.AddComponent(new ShootPoint() { Value = _shootPoint });
		}
	}
}