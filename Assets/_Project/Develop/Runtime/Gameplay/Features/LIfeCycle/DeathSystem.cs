using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.LIfeCycle
{
	public class DeathSystem : IInitializableSystem, IUpdatableSystem
	{
		private ReactiveVabiable<bool> _isDead;
		
		private ReactiveVabiable<float> _currentHealth;


		public void OnInit(Entity entity)
		{
			_isDead = entity.GetComponent<IsDead>().Value;
			_currentHealth = entity.GetComponent<CurrentHealth>().Value;
		}

		public void OnUpdate(float dt)
		{
			if (_isDead.Value)
				return;

			if (_currentHealth.Value <= 0)
			{
				_isDead.Value = true;
				Debug.Log("Я мертв");
			}
		}
	}
}