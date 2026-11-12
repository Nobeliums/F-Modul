using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.Attack
{
	public class AttackCooldownTimerSystem : IInitializableSystem, IUpdatableSystem, IDisposableSystem
	{
		private ReactiveVariable<float> _cooldownInitialTime;
		private ReactiveVariable<float> _cooldownCurrentTime;
		private ReactiveVariable<bool> _inAttackCooldown;

		private ReactiveEvent _endAttack;

		private IDisposable _endAttackDisposable;
		
		public void OnInit(Entity entity)
		{
			_cooldownInitialTime = entity.AttackCooldownInitialTime;
			_cooldownCurrentTime = entity.AttackCooldownCurrentTime;
			_inAttackCooldown = entity.InAttackCooldown;
			_endAttack = entity.EndAttackEvent;

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