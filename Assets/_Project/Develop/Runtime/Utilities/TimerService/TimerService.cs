using System;
using System.Collections;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.Reactive;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Develop.Runtime.Utilities.TimerService
{
	public class TimerService : IDisposable
	{
		private float _cooldown;
		private ReactiveVariable<float> _currentTime;

		private ReactiveEvent _timerEnded;

		private ICoroutinePerformer _performer;
		private Coroutine _timerProcess;

		public TimerService(float cooldown, ICoroutinePerformer performer)
		{
			_cooldown = cooldown;
			_performer = performer;

			_currentTime = new();
			_timerEnded = new();
		}

		public IReadOnlyReactiveVariable<float> CurrentTime => _currentTime;
		
		public IReadOnlyReactiveEvent TimerEnded => _timerEnded;
		
		public bool IsOver => _currentTime.Value <= 0f;

		public void Stop()
		{
			if (_timerProcess != null)
				_performer.StopPerform(_timerProcess);
		}

		public void Restart()
		{
			Stop();
			
			_timerProcess = _performer.StartPerform(TimerProcess());
		}

		public void Dispose()
		{
			Stop();
		}

		private IEnumerator TimerProcess()
		{
			_currentTime.Value = _cooldown;
			
			while (IsOver == false)
			{
				_currentTime.Value -= Time.deltaTime;
				yield return null;
			}
			
			_timerEnded.Invoke();
		}
	}
}