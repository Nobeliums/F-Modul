using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.LIfeCycle
{
	public class DisableCollidersOnDeathSystem : IInitializableSystem, IDisposableSystem
	{
		private List<Collider> _colliders;
		private ReactiveVabiable<bool> _isDead;

		private IDisposable _isDeadDisposable;

		public void OnInit(Entity entity)
		{
			_colliders = entity.GetComponent<DisableCollidersOnDeath>().Value;
			_isDead = entity.GetComponent<IsDead>().Value;

			_isDeadDisposable = _isDead.Subscribe(OnDead);
		}

		private void OnDead(bool arg1, bool isDead)
		{
			if (isDead)
			{
				foreach (Collider collider in _colliders)
					collider.enabled = false;
			}
		}

		public void OnDispose()
		{
			_isDeadDisposable.Dispose();
		}
	}
}