using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.Attack
{
	public class AttackCooldownTimerSystem : IInitializableSystem, IUpdatableSystem, IDisposableSystem
	{
		private ReactiveVabiable<float> _cooldownInitialTime;
		private ReactiveVabiable<float> _cooldownCurrentTime;
		private ReactiveVabiable<bool> _inAttackCooldown;

		private ReactiveEvent _endAttack;

		private IDisposable _endAttackDisposable;
		
		public void OnInit(Entity entity)
		{
			_cooldownInitialTime = entity.GetComponent<AttackCooldownInitialTime>().Value;
			_cooldownCurrentTime = entity.GetComponent<AttackCooldownCurrentTime>().Value;
			_inAttackCooldown = entity.GetComponent<InAttackCooldown>().Value;
			_endAttack = entity.GetComponent<EndAttackEvent>().Value;

			_endAttackDisposable = _endAttack.Subscribe(OnEndAttack);
		}

		public void OnUpdate(float dt)
		{
			if (_inAttackCooldown.Value == false)
				return;
			
			_cooldownCurrentTime.Value += dt;
			
			if (IsCooldownOver())
				_inAttackCooldown.Value = false;
		}

		private void OnEndAttack()
		{
			_cooldownCurrentTime.Value = 0f;
			_inAttackCooldown.Value = true;
		}

		public void OnDispose()
		{
			_endAttackDisposable.Dispose();
		}

		private bool IsCooldownOver()
		{
			return _cooldownCurrentTime.Value >= _cooldownInitialTime.Value;
		}
	}
}