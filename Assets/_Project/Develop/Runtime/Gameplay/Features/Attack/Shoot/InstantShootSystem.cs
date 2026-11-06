using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Attack
{
	public class InstantShootSystem : IInitializableSystem, IDisposableSystem
	{
		private readonly EntitiesFactory _entitiesFactory;
		
		private ReactiveEvent _attackDelayEndEvent;
		private ReactiveVabiable<float> _damage;
		private Transform _shootPoint;

		private IDisposable _attackDelayEndDisposable;

		public InstantShootSystem(EntitiesFactory entitiesFactory)
		{
			_entitiesFactory = entitiesFactory;
		}

		public void OnInit(Entity entity)
		{
			_attackDelayEndEvent = entity.GetComponent<AttackDelayEndEvent>().Value;
			_damage = entity.GetComponent<InstantAttackDamage>().Value;
			_shootPoint = entity.GetComponent<ShootPoint>().Value;

			_attackDelayEndDisposable = _attackDelayEndEvent.Subscribe(OnAttackDelayEnd);
		}

		private void OnAttackDelayEnd()
		{
			_entitiesFactory.CreateProjectile(_shootPoint.position, _shootPoint.forward, _damage.Value);
		}

		public void OnDispose()
		{
			_attackDelayEndDisposable.Dispose();
		}
	}
}