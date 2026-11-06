using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Gameplay.Features.LIfeCycle;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.ApplyDamage
{
	public class ApplyDamageSystem : IInitializableSystem, IDisposableSystem
	{
		private ReactiveEvent<float> _damageRequest;
		private ReactiveEvent<float> _damageEvent;

		private ReactiveVariable<float> _health;
		
		private ICompositeCondition _canApplyDamage;
		
		private IDisposable _requestDisposable;


		public void OnInit(Entity entity)
		{
			_damageRequest = entity.GetComponent<TakeDamageRequest>().Value;
			_damageEvent = entity.GetComponent<TakeDamageEvent>().Value;
			
			_health = entity.GetComponent<CurrentHealth>().Value;
			
			_canApplyDamage = entity.GetComponent<CanApplyDamage>().Value;

			_requestDisposable = _damageRequest.Subscribe(OnDamageRequest);
		}

		private void OnDamageRequest(float damage)
		{
			if (damage < 0)
				throw new ArgumentOutOfRangeException(nameof(damage), damage, "Value cannot be negative.");
			
			if (_canApplyDamage.Evaluate())
			{
				_health.Value = MathF.Max(_health.Value - damage, 0f);
				
				_damageEvent.Invoke(damage);
			}
		}

		public void OnDispose()
		{
			_requestDisposable.Dispose();
		}
	}
}