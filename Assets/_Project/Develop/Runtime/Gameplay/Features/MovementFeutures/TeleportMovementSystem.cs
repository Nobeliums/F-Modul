using System;
using _Project.Develop.Runtime.Gameplay.Common;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Gameplay.Features.LIfeCycle.Energy;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Develop.Runtime.Gameplay.Features.MovementFeuture
{
	public class TeleportMovementSystem : IInitializableSystem, IUpdatableSystem
	{
		private ReactiveVariable<float> _teleportRadius;
		private ReactiveVariable<float> _energyCost;
		private Rigidbody _rigidbody;

		private ReactiveEvent _teleportEvent;
		private ReactiveEvent<float> _spendEnergyRequest;

		private ICompositeCondition _canTeleport;
		
		public void OnInit(Entity entity)
		{
			_teleportRadius = entity.GetComponent<TeleportRadius>().Value;
			_energyCost = entity.GetComponent<TeleportEnergyCost>().Value;
			_canTeleport = entity.GetComponent<CanTeleport>().Value;
			_rigidbody = entity.GetComponent<RigidbodyComponent>().Value;
			_spendEnergyRequest = entity.GetComponent<SpendEnergyRequest>().Value;
			
			_teleportEvent = entity.GetComponent<TeleportEvent>().Value;
		}

		public void OnUpdate(float dt)
		{
			if (_canTeleport.Evaluate())
			{
				TeleportProcess();
			}
		}

		private void TeleportProcess()
		{
			Vector3 teleportDirection = GetRandomTeleportDirection();
			float teleportRadius = GetRandomTeleportRadius();
				
			Vector3 newPosition = _rigidbody.position + (teleportDirection * teleportRadius);
				
			_rigidbody.Move(newPosition, _rigidbody.rotation);
				
			_spendEnergyRequest.Invoke(_energyCost.Value);
			_teleportEvent.Invoke();
		}

		private float GetRandomTeleportRadius()
			=> Random.Range(0f, _teleportRadius.Value);

		private Vector3 GetRandomTeleportDirection()
		{
			float randomXDirection = Random.Range(-1f, 1f);
			float randomZDirection = Random.Range(-1f, 1f);
			
			return new Vector3(randomXDirection, 0f, randomZDirection).normalized;
		}


	}
}