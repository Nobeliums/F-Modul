using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.MovementFeuture;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.Input;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay
{
	public class TestGameplay : MonoBehaviour
	{
		private bool _isRunning;
		private DIContainer _container;

		private Entity _rigidbodyEntity;
		private Entity _characterControllerEntity;
		
		public void Initialize(DIContainer container)
		{
			_container = container;
		}

		public void Run()
		{
			_isRunning = true;

			_rigidbodyEntity = _container.Resolve<EntitiesFactory>().CreateRigidbodyTestEntity(Vector3.left);
			_characterControllerEntity =  _container.Resolve<EntitiesFactory>().CreateCharacterControllerTestEntity(Vector3.right);
		}

		private void Update()
		{
			if (_isRunning == false)
				return;

			_rigidbodyEntity.GetComponent<MoveDirection>().Value.Value = InputService.GetMovementDirection();
			_characterControllerEntity.GetComponent<MoveDirection>().Value.Value = InputService.GetMovementDirection();
		}
	}
}