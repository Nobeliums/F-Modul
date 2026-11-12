using _Project.Develop.Runtime.Gameplay.Common;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.MovementFeuture;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.AI.States
{
	public class RotateToTargetState : State, IUpdatebleState
	{
		private ReactiveVariable<Vector3> _rotationDirection;
		private ReactiveVariable<Entity> _target;
		private Transform _transform;

		public RotateToTargetState(Entity entity)
		{
			_rotationDirection = entity.GetComponent<RotateDirection>().Value;
			_target = entity.GetComponent<CurrentTarget>().Value;
			_transform = entity.GetComponent<TransformComponent>().Value;
		}
		
		public void Update(float deltaTime)
		{
			if (_target.Value != null)
				_rotationDirection.Value = (_target.Value.GetComponent<TransformComponent>().Value.position - _transform.position).normalized;
		}
	}
}