using System;
using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.Gameplay.Features.CharsSelector;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.Meta.Features.GameStatistics;

namespace _Project.Develop.Runtime.Features.Gamecycle
{
	public class GameplayCycle : IDisposable
	{
		private Gamemode _gameMode;
		private IGameCharsSelector _charsSelector;

		private GameModeConfig _config;
		private GameStatisticsService _gameStatistics;
		private WalletService _walletService;

		public GameplayCycle(Gamemode gamemode,
			IGameCharsSelector charsSelector,
			GameModeConfig config,
			GameStatisticsService gameStatistics,
			WalletService walletService)
		{
			_gameMode = gamemode;
			_charsSelector = charsSelector;
			_config = config;
			_gameStatistics = gameStatistics;
			_walletService = walletService;
		}

		public bool IsStarted => _gameMode.IsStarted;
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
			_gameStatistics.OnWinGame();
			
			_walletService.Add(
				_config.WinReward,
				WalletCurrencyType.Gold);
		}

		private void OnLoseGame()
		{
			_gameStatistics.OnLoseGame();

			int penalty = _config.LosePenalty;
			
			if (_walletService.IsEnough(penalty, WalletCurrencyType.Gold))
				_walletService.Debit(penalty, WalletCurrencyType.Gold);
			else
				_walletService.Debit(_walletService.GetCurrency(WalletCurrencyType.Gold).Value, WalletCurrencyType.Gold);
		}
	}
}