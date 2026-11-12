using _Project.Develop.Runtime.Gameplay.Common;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.MovementFeuture
{
	public class CharacterControllerMovementSystem : IInitializableSystem, IUpdatableSystem
	{
		private CharacterController _characterController;
		private ReactiveVariable<float> _moveSpeed;
		private ReactiveVariable<Vector3> _moveDirection;
		
		public void OnInit(Entity entity)
		{
			_characterController = entity.CharacterControllerComponent;
			_moveSpeed = entity.MoveSpeed;
			_moveDirection = entity.MoveDirection;
		}

		public void OnUpdate(float dt)
		{
			_characterController.Move(_moveDirection.Value * (_moveSpeed.Value * dt));
		}
	}
}