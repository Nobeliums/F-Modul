using System;
using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.CharsSelector;
using _Project.Develop.Runtime.Gameplay.Features.MovementFeuture;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.Meta.Features.GameStatistics;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.DataManagment;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using _Project.Develop.Runtime.Utilities.Input;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Features.Gamecycle
{
	public class GameplayCycle : IDisposable
	{
		public event Action WinGame;
		public event Action LoseGame;
		public event Action GameEnd;
		
		private GameStatisticsService _gameStatistics;
		private PlayerDataProvider _playerDataProvider;
		private ICoroutinePerformer _coroutinePerformer;
		
		private bool _isRunning;
		private DIContainer _container;

		private Entity _rigidbodyEntity;
		private Entity _characterControllerEntity;

		public GameplayCycle(
			GameStatisticsService gameStatistics,
			PlayerDataProvider playerDataProvider,
			ICoroutinePerformer coroutinePerformer,
			DIContainer container)
		{
			_gameStatistics = gameStatistics;
			_playerDataProvider = playerDataProvider;
			_coroutinePerformer = coroutinePerformer;
			_container = container;
		}
		
		public bool IsWin { get; private set; }
		public bool IsLose { get; private set; }

		public void Initialize()
		{

		}

		public void Dispose()
		{

		}

		public void Run()
		{
			_isRunning = true;

			_rigidbodyEntity = _container.Resolve<EntitiesFactory>().CreateRigidbodyTestEntity(Vector3.left);
			_characterControllerEntity =  _container.Resolve<EntitiesFactory>().CreateCharacterControllerTestEntity(Vector3.right);
		}
		
		public void ProcessUpdate()
		{
			if (_isRunning == false)
				return;

			_rigidbodyEntity.GetComponent<MoveDirection>().Value.Value = InputService.GetMovementDirection();
			_characterControllerEntity.GetComponent<MoveDirection>().Value.Value = InputService.GetMovementDirection();
		}
		
		private void OnWinGame()
		{
			WinGame?.Invoke();
			GameEnd?.Invoke();
			_gameStatistics.OnWinGame();
			
			_coroutinePerformer.StartPerform(_playerDataProvider.Save());
		}

		private void OnLoseGame()
		{
			LoseGame?.Invoke();
			GameEnd?.Invoke();
			_gameStatistics.OnLoseGame();

			_coroutinePerformer.StartPerform(_playerDataProvider.Save());
		}
	}
}