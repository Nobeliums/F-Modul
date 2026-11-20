using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.MainHero;
using _Project.Develop.Runtime.Utilities;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.StageFeature
{
	public class PreparationTriggerService
	{
		private ReactiveVariable<bool> _hasMainHeroContact = new();
		
		private EntitiesFactory _entitiesFactory;
		private EntitiesLifeContext _context;

		private Entity _nextStageTriggerEntity;
		private Buffer<Entity> _nextStageTriggerContacts;

		public PreparationTriggerService(EntitiesFactory entitiesFactory, EntitiesLifeContext context)
		{
			_entitiesFactory = entitiesFactory;
			_context = context;
		}
		
		public IReadOnlyReactiveVariable<bool> HasMainHeroContact => _hasMainHeroContact;

		public void Create(Vector3 position)
		{
			if (_nextStageTriggerEntity != null)
				throw new InvalidOperationException("Trigger entity already created");
			
			_nextStageTriggerEntity = _entitiesFactory.CreateContactTrigger(position);
			_nextStageTriggerContacts = _nextStageTriggerEntity.ContactEntitiesBuffer;
		}

		public void Update(float deltaTime)
		{
			if (_nextStageTriggerEntity == null)
				return;
			
			for (int i = 0; i < _nextStageTriggerContacts.Count; i++)
			{
				Entity contact = _nextStageTriggerContacts.Items[i];

				if (contact.HasComponent<IsMainHero>())
				{
					_hasMainHeroContact.Value = true;
					return;
				}
			}
			
			_hasMainHeroContact.Value = false;
		}

		public void Cleanup()
		{
			_context.Release(_nextStageTriggerEntity);
			_hasMainHeroContact.Value = false;
			_nextStageTriggerEntity = null;
			_nextStageTriggerContacts = null;
		}
	}
}