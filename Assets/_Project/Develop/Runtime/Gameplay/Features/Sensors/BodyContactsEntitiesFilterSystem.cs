using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Sensors
{
	public class BodyContactsEntitiesFilterSystem : IInitializableSystem, IUpdatableSystem
	{
		private Buffer<Collider> _contacts;
		
		private Buffer<Entity> _entities;

		private CollidersRegistryService _collidersRegistryService;

		public BodyContactsEntitiesFilterSystem(CollidersRegistryService collidersRegistryService)
		{
			_collidersRegistryService = collidersRegistryService;
		}

		public void OnInit(Entity entity)
		{
			_contacts = entity.GetComponent<ContactColliderBuffer>().Value;
			
			_entities = entity.GetComponent<ContactEntitiesBuffer>().Value;
		}

		public void OnUpdate(float dt)
		{
			_entities.Count = 0;

			for (int i = 0; i < _contacts.Count; i++)
			{
				Collider collider = _contacts.Items[i];

				Entity contactEntity = _collidersRegistryService.GetBy(collider);

				if (contactEntity != null)
				{
					_entities.Items[_entities.Count] = contactEntity;
					_entities.Count++;
				}
			}
		}
	}
}