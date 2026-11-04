using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.LIfeCycle
{
	public class SelfReleaseSystem : IInitializableSystem, IUpdatableSystem
	{
		
		private Entity _entity;
		
		private readonly EntitiesLifeContext  _entitiesContext;
		private ICompositeCondition _releaseCondition;

		public SelfReleaseSystem(EntitiesLifeContext entitiesLifeContext)
		{
			_entitiesContext = entitiesLifeContext;
		}
		
		public void OnInit(Entity entity)
		{
			_releaseCondition = entity.GetComponent<MustSelfRelease>().Value;

			_entity = entity;
		}

		public void OnUpdate(float dt)
		{
			if (_releaseCondition.Evaluate())
			{
				_entitiesContext.Release(_entity);
				Debug.Log("Released");
			}
		}
	}
}