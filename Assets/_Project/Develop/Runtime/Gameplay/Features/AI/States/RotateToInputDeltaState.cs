using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Input;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.AI.States
{
	public class RotateToInputDeltaState : State, IUpdatebleState
	{
		private ReactiveVariable<Vector3> _rotationDirection;
		private IInputService _inputService;
		private Transform _transform;

		private float _currentY;

		public RotateToInputDeltaState(Entity entity, IInputService inputService)
		{
			_rotationDirection = entity.RotateDirection;
			_inputService = inputService;
			_transform = entity.TransformComponent;
		}

		public override void Enter()
		{
			base.Enter();
			
			_currentY = _transform.eulerAngles.y;
		}

		public void Update(float deltaTime)
		{
			_rotationDirection.Value = (_inputService.CursorPosition - _transform.position).normalized;
		}
	}
}