using System;
using _Project.Develop.Runtime.Gameplay.Common;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Gameplay.Features.ApplyDamage;
using _Project.Develop.Runtime.Gameplay.Features.MovementFeuture;
using _Project.Develop.Runtime.Gameplay.Features.Sensors;
using _Project.Develop.Runtime.Utilities;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.TeleportTakeDamage
{
	public class TeleportTakeDamageSystem : IInitializableSystem, IDisposableSystem
	{
		private Entity _selfEntity;
		
		private ReactiveVariable<float> _damage;
		private ReactiveVariable<float> _takeDamageRadius;
		private LayerMask _damageLayerMask;

		private ReactiveEvent _teleportEvent;

		private Rigidbody _rigidbody;
		private Buffer<Collider> _colliderBuffer;
		
		private CollidersRegistryService  _collidersRegistry;

		private IDisposable _teleportDisposable;

		public TeleportTakeDamageSystem(CollidersRegistryService collidersRegistry)
		{
			_collidersRegistry = collidersRegistry;
		}

		public void OnInit(Entity entity)
		{
			_damage = entity.TeleportDamage;
			_takeDamageRadius = entity.TeleportTakeDamageRadius;

			_teleportEvent = entity.TeleportEvent;

			_rigidbody = entity.RigidbodyComponent;
			_damageLayerMask = entity.ContactsDetectingMask;
			_colliderBuffer = entity.TeleportTakeDamageColliderBuffer;

			_teleportDisposable = _teleportEvent.Subscribe(OnTeleport);
			_selfEntity = entity;
		}

		private void OnTeleport()
		{
			
			_colliderBuffer.Count = Physics.OverlapSphereNonAlloc(
				_rigidbody.transform.position,
				_takeDamageRadius.Value,
				_colliderBuffer.Items,
				_damageLayerMask,
				QueryTriggerInteraction.Ignore);

			if (_colliderBuffer.Count > 0)
			{
				ProcessDamage();
			}
			
			Debug.Log($"Damaged: " + _colliderBuffer.Count);
		}

		private void ProcessDamage()
		{
			for (int i = _colliderBuffer.Count - 1; i >= 0; i--)
			{
				Entity entity = _collidersRegistry.GetBy(_colliderBuffer.Items[i]);

				if (entity != null && entity != _selfEntity && entity.HasComponent<TakeDamageRequest>())
					entity.TakeDamageRequest.Invoke(_damage.Value);
			}
		}

		public void OnDispose()
		{
			_teleportDisposable.Dispose();
		}
	}
}