using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Gameplay.Features.MovementFeuture;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.LIfeCycle
{
	public class SelfReleaseSystem : IInitializableSystem, IUpdatableSystem
	{
		private ReactiveVabiable<bool> _isDead;
		
		private Entity _entity;
		
		EntitiesLifeContext  _entitiesContext;

		public SelfReleaseSystem(EntitiesLifeContext entitiesLifeContext)
		{
			_entitiesContext = entitiesLifeContext;
		}
		
		public void OnInit(Entity entity)
		{
			_isDead = entity.GetComponent<IsDead>().Value;
			_entity = entity;
		}

		public void OnUpdate(float dt)
		{
			if (_isDead.Value)
			{
				_entitiesContext.Release(_entity);
				Debug.Log("Released");
			}
		}
	}
}