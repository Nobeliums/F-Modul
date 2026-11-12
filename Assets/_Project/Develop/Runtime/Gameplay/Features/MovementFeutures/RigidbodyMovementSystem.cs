using _Project.Develop.Runtime.Gameplay.Common;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.MovementFeuture
{
	public class RigidbodyMovementSystem : IInitializableSystem, IUpdatableSystem
	{
		private ReactiveVariable<Vector3> _moveDirection; 
		private ReactiveVariable<float> _speed;
		private ICompositeCondition _canMove;
		private ReactiveVariable<bool> _isMoving;
		
		private Rigidbody _rigidbody;

		public void OnInit(Entity entity)
		{
			_moveDirection = entity.MoveDirection;
			_speed = entity.MoveSpeed;
			_rigidbody = entity.RigidbodyComponent;
			_canMove = entity.CanMove;
			_isMoving = entity.IsMoving;
		}

		public void OnUpdate(float dt)
		{
			if (_canMove.Evaluate() == false)
			{
				_rigidbody.velocity = Vector3.zero;
				return;
			}
			
			Vector3 moveDirectionVector = _moveDirection.Value * _speed.Value;
			_isMoving.Value = _rigidbody.velocity.magnitude > 0f;
			
			_rigidbody.velocity = moveDirectionVector;
		}
	}
}