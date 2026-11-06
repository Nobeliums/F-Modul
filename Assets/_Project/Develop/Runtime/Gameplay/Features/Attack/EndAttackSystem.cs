using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Attack
{
	public class EndAttackSystem : IInitializableSystem, IDisposableSystem
	{
		private ReactiveEvent _endAttackEvent;
		private ReactiveVariable<bool> _inAttackProcess;
		private ReactiveVariable<float> _attackProcessInitialTime;
		private ReactiveVariable<float> _attackProcessCurrentTime;

		private IDisposable _timerDisposable;


		public void OnInit(Entity entity)
		{
			_endAttackEvent = entity.GetComponent<EndAttackEvent>().Value;
			_inAttackProcess = entity.GetComponent<InAttackProcess>().Value;
			_attackProcessCurrentTime = entity.GetComponent<AttackProcessCurrentTime>().Value;
			_attackProcessInitialTime = entity.GetComponent<AttackProcessInitialTime>().Value;

			_timerDisposable = _attackProcessCurrentTime.Subscribe(OnTimerChanged);
		}

		private void OnTimerChanged(float arg1, float currentTime)
		{
			if (TimeIsDone(currentTime))
			{
				Debug.Log("Attack ended");
				_inAttackProcess.Value = false;
				_endAttackEvent.Invoke();
			}
		}

		public void OnDispose()
		{
			_timerDisposable.Dispose();
		}

		private bool TimeIsDone(float currentTime) => currentTime >= _attackProcessInitialTime.Value;
	}
}