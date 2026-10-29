using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono
{
	public abstract class MonoEntityRegistrator : MonoBehaviour
	{
		public abstract void Register(Entity entity);
	}
}