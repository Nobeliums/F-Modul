using System;
using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.ApplyDamage;
using _Project.Develop.Runtime.Gameplay.Features.Attack;
using _Project.Develop.Runtime.Gameplay.Features.CharsSelector;
using _Project.Develop.Runtime.Gameplay.Features.LIfeCycle;
using _Project.Develop.Runtime.Gameplay.Features.LIfeCycle.Energy;
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
		private DIContainer _container; // контейнер тут для теста чтобы проще и быстрее было

		private Entity _rigidbodyEntity;
		private Entity _characterControllerEntity;
		private Entity _playerEntity;

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

			_container.Resolve<EntitiesFactory>().CreateGhostEntity(Vector3.left);
			_container.Resolve<EntitiesFactory>().CreateGhostEntity(Vector3.right);
			_rigidbodyEntity = _container.Resolve<EntitiesFactory>().CreateTeleportGhost(Vector3.back * 3);
			_playerEntity =  _container.Resolve<EntitiesFactory>().CreateMainCharacterEntity(Vector3.forward * 3);
		}
		
		public void ProcessUpdate()
		{
			if (_isRunning == false)
				return;

			_playerEntity.GetComponent<MoveDirection>().Value.Value = InputService.GetMovementDirection();
			_playerEntity.GetComponent<RotateDirection>().Value.Value = InputService.GetMovementDirection();
			// _characterControllerEntity.GetComponent<MoveDirection>().Value.Value = InputService.GetMovementDirection();

			if (Input.GetKeyDown(KeyCode.R))
			{
				_playerEntity.GetComponent<StartAttackRequest>().Value.Invoke();
			}
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