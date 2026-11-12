using _Project.Develop.Runtime.Gameplay.Common;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.MovementFeuture
{
	public class CharacterControllerRotationSystem : IInitializableSystem, IUpdatableSystem
	{
		private ReactiveVariable<Vector3> _moveDirection; 
		private ReactiveVariable<float> _rotationSpeed;
		private CharacterController _characterController;

		public void OnInit(Entity entity)
		{
			_characterController = entity.CharacterControllerComponent;
			_rotationSpeed = entity.RotateSpeed;
			_moveDirection = entity.RotateDirection;
		}

		public void OnUpdate(float dt)
		{
			if (_moveDirection.Value == Vector3.zero)
				return;
			
			Quaternion rotation = Quaternion.RotateTowards(
				_characterController.transform.rotation,
				Quaternion.LookRotation(_moveDirection.Value), 
				dt * _rotationSpeed.Value);
			
			_characterController.transform.rotation = rotation;
		}
	}
}