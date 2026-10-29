using _Project.Develop.Runtime.Gameplay.Common;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Input;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.MovementFeuture
{
	public class RigidbodyRotationSystem : IInitializableSystem, IUpdatableSystem
	{
		private Rigidbody _rigidbody;
		private ReactiveVabiable<float> _rotateSpeed;
		
		public void OnInit(Entity entity)
		{
			_rigidbody = entity.GetComponent<RigidbodyComponent>().Value;
			_rotateSpeed = entity.GetComponent<RotateSpeedComponent>().Value;
		}

		public void OnUpdate(float dt)
		{
			Vector3 input = InputService.GetMovementDirection();

			if (input == Vector3.zero)
				return;
			
			Quaternion rotation = Quaternion.RotateTowards(
				_rigidbody.rotation,
				Quaternion.LookRotation(input), 
				dt * _rotateSpeed.Value);
			
			_rigidbody.MoveRotation(rotation);
		}
	}
}