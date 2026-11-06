using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.Attack
{
	public class AttackProcessTimerSystem : IInitializableSystem, IDisposableSystem, IUpdatableSystem
	{
		private ReactiveVabiable<float> _currentTime;
		private ReactiveVabiable<bool> _inAttackProcess;
		private ReactiveEvent _startAttackEvent;

		private IDisposable _startAttackDisposable;

		public void OnInit(Entity entity)
		{
			_currentTime = entity.GetComponent<AttackProcessCurrentTime>().Value;
			_inAttackProcess = entity.GetComponent<InAttackProcess>().Value;
			_startAttackEvent = entity.GetComponent<StartAttackEvent>().Value;

			_startAttackDisposable = _startAttackEvent.Subscribe(OnStartAttack);
		}
		
		public void OnUpdate(float dt)
		{
			if (_inAttackProcess.Value == false)
				return;
			
			_currentTime.Value += dt;
		}

		private void OnStartAttack()
		{
			_currentTime.Value = 0;
		}

		public void OnDispose()
		{
			_startAttackDisposable.Dispose();
		}
	}
}