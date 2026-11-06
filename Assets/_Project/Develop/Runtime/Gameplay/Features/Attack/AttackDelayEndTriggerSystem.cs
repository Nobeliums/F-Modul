using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Attack
{
	public class AttackDelayEndTriggerSystem : IInitializableSystem, IDisposableSystem
	{
		private ReactiveEvent _attackDelayEndEvent;
		private ReactiveEvent _startAttackEvent;
		private ReactiveVabiable<float> _delay;
		private ReactiveVabiable<float> _attackProcessCurrentTime;

		private bool _alreadyAttacked;

		private IDisposable _disposable;
		private IDisposable _startAttackDisposable;

		public void OnInit(Entity entity)
		{
			_attackDelayEndEvent = entity.GetComponent<AttackDelayEndEvent>().Value;
			_delay = entity.GetComponent<AttackDelayTime>().Value;
			_attackProcessCurrentTime = entity.GetComponent<AttackProcessCurrentTime>().Value;
			_startAttackEvent = entity.GetComponent<StartAttackEvent>().Value;

			
			_disposable = _attackProcessCurrentTime.Subscribe(OnTimerChanged);
			_startAttackDisposable = _startAttackEvent.Subscribe(OnStartAttack);
		}

		private void OnStartAttack()
		{
			_alreadyAttacked = false;
		}

		private void OnTimerChanged(float arg1, float currentTime)
		{
			if (_alreadyAttacked) return;
			
			if (currentTime >= _delay.Value)
			{
				Debug.Log("Delay ended");
				_attackDelayEndEvent.Invoke();
				_alreadyAttacked = true;
			}
		}

		public void OnDispose()
		{
			_disposable.Dispose();
		}
	}
}