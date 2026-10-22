using System;
using System.Collections;
using _Project.Develop.Runtime.Gamecycle;
using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.Gameplay.Utilities.CharsSelector;
using _Project.Develop.Runtime.Meta.Utilities.GameStatistics;
using _Project.Develop.Runtime.Gameplay.Utilities.WalletService;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.ConfigManagement;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using _Project.Develop.Runtime.Utilities.Input;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
	public class GameplayBootstrap : SceneBootstrap
	{
		private DIContainer _container;
		private Gamemode _gameMode;

		public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
		{
			_container = container;
			
			if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
				throw new ArgumentException($"{nameof(sceneArgs)} must be of type GameplayInputArgs");
			
			GameplayContextRegistrations.Process(_container, gameplayInputArgs);
			
			_container.Initialize();
		}

		public override IEnumerator Initialize()
		{
			_gameMode = _container.Resolve<Gamemode>();
			
			ConfigService configService = _container.Resolve<ConfigService>();
			GameModeConfig config = configService.GetConfig<GameModeConfig>();

			string targetPhrase = _container.Resolve<IGameCharsSelector>().GetGameChars(config.PhraseLenght);
			
			_gameMode.Initialize(targetPhrase);
			_gameMode.WinEvent += OnWinGame;
			_gameMode.LoseEvent += OnLoseGame;

			yield break;
		}

		public override void Run()
		{
			_gameMode.Run();
			
			Debug.Log("[Gameplay Bootstrap] Gameplay Bootstrap Run");
		}

		private void Update()
		{
			if (_container == null)
				return;
			
			_gameMode.ProcessUpdate();
			
			if (_gameMode.IsStarted == false && InputService.GetEndGameButton())
			{
				SceneSwitcherService switcherService = _container.Resolve<SceneSwitcherService>();
				ICoroutinePerformer performer = _container.Resolve<ICoroutinePerformer>();
				PlayerDataProvider playerDataProvider = _container.Resolve<PlayerDataProvider>();

				performer.StartPerform(playerDataProvider.Save());
				
				if (_gameMode.IsWin)
				{
					performer.StartPerform(switcherService.ProcessSceneSwitchTo(Scenes.MainMenu));
				}
				
				else if (_gameMode.IsLose)
				{
					GameplayInputArgs gameplayInputArgs = _container.Resolve<GameplayInputArgs>();
	
					performer.StartPerform(switcherService.ProcessSceneSwitchTo(Scenes.Gameplay, gameplayInputArgs));
				}
			}
		}

		private void OnDestroy()
		{
			_gameMode.WinEvent -= OnWinGame;
			_gameMode.LoseEvent -= OnLoseGame;
			
			Debug.Log("[Gameplay Bootstrap] Gameplay Bootstrap OnDestroy");
		}

		private void OnWinGame()
		{
			_container.Resolve<GameStatisticsService>().OnWinGame();
			
			ConfigService configService = _container.Resolve<ConfigService>();
			
			_container.Resolve<WalletService>().Add(
				configService.GetConfig<GameModeConfig>().WinReward,
				WalletCurrencyType.Gold);
		}

		private void OnLoseGame()
		{
			_container.Resolve<GameStatisticsService>().OnLoseGame();
			
			ConfigService configService = _container.Resolve<ConfigService>();
			WalletService walletService = _container.Resolve<WalletService>();

			int penalty = configService.GetConfig<GameModeConfig>().LosePenalty;
			
			if (walletService.IsEnough(penalty, WalletCurrencyType.Gold))
				walletService.Debit(penalty, WalletCurrencyType.Gold);
			else
				walletService.Debit(walletService.GetCurrency(WalletCurrencyType.Gold).Value, WalletCurrencyType.Gold);
		}
	}
}