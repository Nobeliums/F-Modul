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
		private ReactiveVabiable<Vector3> _rotateDirection;

		public void OnInit(Entity entity)
		{
			_rigidbody = entity.GetComponent<RigidbodyComponent>().Value;
			_rotateSpeed = entity.GetComponent<RotateSpeed>().Value;
			_rotateDirection = entity.GetComponent<RotateDirection>().Value;
		}

		public void OnUpdate(float dt)
		{
			if (_rotateDirection.Value == Vector3.zero)
				return;
			
			Quaternion rotation = Quaternion.RotateTowards(
				_rigidbody.rotation,
				Quaternion.LookRotation(_rotateDirection.Value), 
				dt * _rotateSpeed.Value);
			
			_rigidbody.MoveRotation(rotation);
		}
	}
}