using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono
{
	public class MonoEntity : MonoBehaviour
	{
		private CollidersRegistryService _collidersRegistryService;
		private Entity _linkedEntity;
		
		public Entity LinkedEntity => _linkedEntity;

		public void Initialize(CollidersRegistryService collidersRegistryService)
		{
			_collidersRegistryService = collidersRegistryService;
		}

		public void Link(Entity entity)
		{
			_linkedEntity = entity;
			
			MonoEntityRegistrator[] registations = GetComponentsInChildren<MonoEntityRegistrator>();
			
			if (registations != null)
				foreach (MonoEntityRegistrator registator in registations)
					registator.Register(entity);

			foreach (Collider collider in GetComponentsInChildren<Collider>())
				_collidersRegistryService.Register(collider, entity);
		}

		public void Cleanup(Entity entity)
		{
			foreach (Collider collider in GetComponentsInChildren<Collider>())
				_collidersRegistryService.Unregister(collider);
		}
	}
}