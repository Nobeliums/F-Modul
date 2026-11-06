using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.LIfeCycle
{
	public class DeathProcessTimerSystem : IInitializableSystem, IUpdatableSystem, IDisposableSystem
	{
		private ReactiveVariable<bool> _isDead;
		private ReactiveVariable<bool> _inDeathProcess;

		private ReactiveVariable<float> _initialTime;
		private ReactiveVariable<float> _currentTime;

		private IDisposable _isDeadDisposable;

		public void OnInit(Entity entity)
		{
			_isDead = entity.GetComponent<IsDead>().Value;
			_inDeathProcess = entity.GetComponent<InDeathProcess>().Value;
			_initialTime = entity.GetComponent<DeathProcessInitialTime>().Value;
			_currentTime = entity.GetComponent<DeathProcessCurrentTime>().Value;

			_isDeadDisposable = _isDead.Subscribe(OnIsDeadChanged);
		}

		public void OnUpdate(float dt)
		{
			if (_inDeathProcess.Value == false)
				return;

			_currentTime.Value -= dt;

			if (CooldownIsOver())
				_inDeathProcess.Value = false;
		}

		private bool CooldownIsOver() => _currentTime.Value <= 0f;

		private void OnIsDeadChanged(bool arg1, bool isDead)
		{
			if (_isDead.Value)
			{
				_currentTime.Value = _initialTime.Value;
				_inDeathProcess.Value = true;
			}
			
		}

		public void OnDispose()
		{
			_isDeadDisposable.Dispose();
		}
	}
}