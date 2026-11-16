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
	public class TeleportMovementSystem : IInitializableSystem, IDisposableSystem
	{
		private Rigidbody _rigidbody;

		private ReactiveEvent _teleportEvent;
		private ReactiveEvent<Vector3> _teleportRequest; 
		
		private IDisposable _teleportRequestDisposable;
		
		public void OnInit(Entity entity)
		{
			_rigidbody = entity.RigidbodyComponent;

			_teleportRequest = entity.TeleportRequest;
			_teleportEvent = entity.TeleportEvent;
			_teleportRequestDisposable = _teleportRequest.Subscribe(TeleportProcess);
		}

		private void TeleportProcess(Vector3 teleportPoint)
		{
			Vector3 newPosition = _rigidbody.position + teleportPoint;
				
			_rigidbody.Move(newPosition, _rigidbody.rotation);
			
			_teleportEvent.Invoke();
		}

		public void OnDispose()
		{
			_teleportRequestDisposable.Dispose();
		}
	}
}