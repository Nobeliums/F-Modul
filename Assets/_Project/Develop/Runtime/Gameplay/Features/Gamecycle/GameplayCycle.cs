using System;
using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.Gameplay.Features.CharsSelector;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.Meta.Features.GameStatistics;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.DataManagment;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Features.Gamecycle
{
	public class GameplayCycle : IDisposable
	{
		public event Action WinGame;
		public event Action LoseGame;
		public event Action GameEnd;
		
		private Gamemode _gameMode;
		private IGameCharsSelector _charsSelector;

		private GameModeConfig _config;
		private GameStatisticsService _gameStatistics;
		private WalletService _walletService;
		private PlayerDataProvider _playerDataProvider;
		private ICoroutinePerformer _coroutinePerformer;

		public GameplayCycle(Gamemode gamemode,
			IGameCharsSelector charsSelector,
			GameModeConfig config,
			GameStatisticsService gameStatistics,
			WalletService walletService,
			PlayerDataProvider playerDataProvider,
			ICoroutinePerformer coroutinePerformer)
		{
			_gameMode = gamemode;
			_charsSelector = charsSelector;
			_config = config;
			_gameStatistics = gameStatistics;
			_walletService = walletService;
			_playerDataProvider = playerDataProvider;
			_coroutinePerformer = coroutinePerformer;
		}
		
		public IReadOnlyReactiveVariable<string> GamePhrase => _gameMode.GamePhrase;
		public IReadOnlyReactiveVariable<string> PlayerInputPhrase => _gameMode.PlayerInputPhrase;
		
		public bool IsWin => _gameMode.IsWin;
		public bool IsLose => _gameMode.IsLose;

		public void Initialize()
		{
			string targetPhrase = _charsSelector.GetGameChars(_config.PhraseLenght);
			
			_gameMode.Initialize(targetPhrase);
			_gameMode.WinEvent += OnWinGame;
			_gameMode.LoseEvent += OnLoseGame;
		}
		
		public void Dispose()
		{
			_gameMode.WinEvent -= OnWinGame;
			_gameMode.LoseEvent -= OnLoseGame;
			_gameMode.Dispose();
		}

		public void Run()
		{
			_gameMode.Run();
		}
		
		public void ProcessUpdate()
		{
			_gameMode.ProcessUpdate();
		}
		
		private void OnWinGame()
		{
			WinGame?.Invoke();
			GameEnd?.Invoke();
			_gameStatistics.OnWinGame();
			
			_walletService.Add(
				_config.WinReward,
				WalletCurrencyType.Gold);
			
			_coroutinePerformer.StartPerform(_playerDataProvider.Save());
		}

		private void OnLoseGame()
		{
			LoseGame?.Invoke();
			GameEnd?.Invoke();
			_gameStatistics.OnLoseGame();

			int penalty = _config.LosePenalty;
			
			if (_walletService.IsEnough(penalty, WalletCurrencyType.Gold))
				_walletService.Debit(penalty, WalletCurrencyType.Gold);
			else
				_walletService.Debit(_walletService.GetCurrency(WalletCurrencyType.Gold).Value, WalletCurrencyType.Gold);

			_coroutinePerformer.StartPerform(_playerDataProvider.Save());
		}
	}
}