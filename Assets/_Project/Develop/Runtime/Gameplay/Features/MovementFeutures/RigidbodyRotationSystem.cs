using _Project.Develop.Runtime.Gameplay.Common;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Input;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.MovementFeuture
{
	public class RigidbodyRotationSystem : IInitializableSystem, IUpdatableSystem
	{
		private Rigidbody _rigidbody;
		private ReactiveVariable<float> _rotateSpeed;
		private ReactiveVariable<Vector3> _rotateDirection;
		private ICompositeCondition _canRotate;

		public void OnInit(Entity entity)
		{
			_rigidbody = entity.RigidbodyComponent;
			_rotateSpeed = entity.RotateSpeed;
			_rotateDirection = entity.RotateDirection;
			_canRotate = entity.CanRotate;

			if (_rotateDirection.Value != Vector3.zero)
				_rigidbody.transform.rotation = Quaternion.LookRotation(_rotateDirection.Value.normalized);
		}

		public void OnUpdate(float dt)
		{
			if (_rotateDirection.Value == Vector3.zero || _canRotate.Evaluate() == false)
				return;
			
			Quaternion rotation = Quaternion.RotateTowards(
				_rigidbody.rotation,
				Quaternion.LookRotation(_rotateDirection.Value), 
				dt * _rotateSpeed.Value);
			
			_rigidbody.MoveRotation(rotation);
		}
	}
}