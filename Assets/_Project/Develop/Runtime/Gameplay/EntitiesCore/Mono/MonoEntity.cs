using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono
{
	public class MonoEntity : MonoBehaviour
	{
		public void Setup(Entity entity)
		{
			MonoEntityRegistrator[] registations = GetComponentsInChildren<MonoEntityRegistrator>();
			
			if (registations != null)
				foreach (MonoEntityRegistrator registator in registations)
					registator.Register(entity);
		}

		public void Cleanup(Entity entity)
		{
			
		}
	}
}