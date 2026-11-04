using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Sensors
{
	public class BodyContactsDetectingSystem : IInitializableSystem, IUpdatableSystem
	{
		private Buffer<Collider> _contacts;
		private LayerMask _layerMask;

		private CapsuleCollider _body;
		
		public void OnInit(Entity entity)
		{
			_contacts = entity.GetComponent<ContactColliderBuffer>().Value;
			_layerMask = entity.GetComponent<ContactsDetectingMask>().Value;

			_body = entity.GetComponent<BodyCollider>().Value;
		}

		public void OnUpdate(float dt)
		{
			_contacts.Count = Physics.OverlapCapsuleNonAlloc(
				_body.bounds.min, 
				_body.bounds.max,
				_body.radius,
				_contacts.Items,
				_layerMask,
				QueryTriggerInteraction.Ignore);

			RemoveSelfFromContacts();
		}

		private void RemoveSelfFromContacts()
		{
			int indexToRemove = -1;
			
			for (int i = 0; i < _contacts.Count; i++)
			{
				if (_contacts.Items[i] == _body)
				{
					indexToRemove = i;
					break;
				}
			}

			if (indexToRemove >= 0)
			{
				for (int i = indexToRemove; i < _contacts.Count - 1; i++)
				{
					_contacts.Items[i] = _contacts.Items[i + 1];
				}

				_contacts.Count--;
			}
		}
	}
}