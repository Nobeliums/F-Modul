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

		private Entity _entity;
		
		public void Initialize(DIContainer container)
		{
			_container = container;
		}

		public void Run()
		{
			_isRunning = true;

			_entity = _container.Resolve<EntitiesFactory>().CreateTestEntity(Vector3.zero);
		}

		private void Update()
		{
			if (_isRunning == false)
				return;

			_entity.GetComponent<MoveDirection>().Value.Value = InputService.GetMovementDirection();
		}
	}
}