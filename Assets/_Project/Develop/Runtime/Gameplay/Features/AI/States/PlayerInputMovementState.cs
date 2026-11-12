using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.MovementFeuture;
using _Project.Develop.Runtime.Utilities.Input;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.AI.States
{
	public class PlayerInputMovementState : State, IUpdatebleState
	{
		private IInputService _inputService;

		private ReactiveVariable<Vector3> _moveDirection;
		private ReactiveVariable<Vector3> _rotationDirection;

		public PlayerInputMovementState(IInputService inputService, Entity entity)
		{
			_inputService = inputService;
			_moveDirection = entity.GetComponent<MoveDirection>().Value;
			_rotationDirection = entity.GetComponent<RotateDirection>().Value;
		}

		public void Update(float deltaTime)
		{
			_moveDirection.Value = _inputService.Direction;
			_rotationDirection.Value = _inputService.Direction;
		}
	}
}