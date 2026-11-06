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
		private ReactiveVabiable<Vector3> _moveDirection; 
		private ReactiveVabiable<float> _speed;
		private ICompositeCondition _canMove;
		private ReactiveVabiable<bool> _isMoving;
		
		private Rigidbody _rigidbody;

		public void OnInit(Entity entity)
		{
			_moveDirection = entity.GetComponent<MoveDirection>().Value;;
			_speed = entity.GetComponent<MoveSpeed>().Value;
			_rigidbody = entity.GetComponent<RigidbodyComponent>().Value;
			_canMove = entity.GetComponent<CanMove>().Value;
			_isMoving = entity.GetComponent<IsMoving>().Value;
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