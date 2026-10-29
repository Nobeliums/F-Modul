using _Project.Develop.Runtime.Gameplay.Common;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.MovementFeuture
{
	public class CharacterControllerRotationSystem : IInitializableSystem, IUpdatableSystem
	{
		private ReactiveVabiable<Vector3> _moveDirection; 
		private ReactiveVabiable<float> _rotationSpeed;
		private CharacterController _characterController;

		public void OnInit(Entity entity)
		{
			_characterController = entity.GetComponent<CharacterControllerComponent>().Value;
			_rotationSpeed = entity.GetComponent<RotateSpeed>().Value;
			_moveDirection = entity.GetComponent<RotateDirection>().Value;
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