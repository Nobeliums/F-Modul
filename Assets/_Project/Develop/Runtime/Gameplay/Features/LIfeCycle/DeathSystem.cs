using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.LIfeCycle
{
	public class DeathSystem : IInitializableSystem, IUpdatableSystem
	{
		private ReactiveVariable<bool> _isDead;
		
		private ICompositeCondition _deathCondition;


		public void OnInit(Entity entity)
		{
			_isDead = entity.GetComponent<IsDead>().Value;
			_deathCondition = entity.GetComponent<MustDie>().Value;
		}

		public void OnUpdate(float dt)
		{
			if (_isDead.Value)
				return;

			if (_deathCondition.Evaluate())
			{
				_isDead.Value = true;
				Debug.Log("I Die");
			}
		}
	}
}