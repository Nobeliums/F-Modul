using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Gameplay.Features.WalletService;
using _Project.Develop.Runtime.Meta.Features.GameStatistics;
using _Project.Develop.Runtime.Meta.Features.ShopManagement;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using _Project.Develop.Runtime.Utilities.Input;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Utilities.MainMenuCycle
{
	public class MainMenuCycle
	{
		private ICoroutinePerformer _coroutinePerformer;
		private SceneSwitcherService _sceneSwitcherService;
		private GameStatisticsService _gameStatisticsService;
		private WalletService _walletService;
		private ShopService _shopService;
		private PlayerDataProvider  _playerDataProvider;

		public MainMenuCycle(
			ICoroutinePerformer coroutinePerformer,
			SceneSwitcherService sceneSwitcherService,
			GameStatisticsService gameStatisticsService,
			WalletService walletService,
			ShopService shopService,
			PlayerDataProvider playerDataProvider)
		{
			_coroutinePerformer = coroutinePerformer;
			_sceneSwitcherService = sceneSwitcherService;
			_gameStatisticsService = gameStatisticsService;
			_walletService = walletService;
			_shopService = shopService;
			_playerDataProvider =  playerDataProvider;
		}

		public void Run()
		{
			
			Debug.Log("S - Текущая статистика");
			Debug.Log($"G - Купить сброс статистики");
		}
		
		public void ProcessUpdate()
		{
			if (InputService.GetStatisticsButton())
			{
				Debug.Log($"Ваша статистика: Выигрышей - {_gameStatisticsService.WinGameCount.Value} Проигрышей - {_gameStatisticsService.LoseGameCount.Value}");
				Debug.Log($"Ваше золото: {_walletService.GetCurrency(WalletCurrencyType.Gold).Value}");
			}

			if (InputService.GetDigitsGameModeButton())
			{
				_coroutinePerformer.StartPerform(_sceneSwitcherService.ProcessSceneSwitchTo(
					Scenes.Gameplay,
					new GameplayInputArgs(GameCharsSequenceType.Digits)));
			}

			if (InputService.GetLettersGameModeButton())
			{
				_coroutinePerformer.StartPerform(_sceneSwitcherService.ProcessSceneSwitchTo(
					Scenes.Gameplay,
					new GameplayInputArgs(GameCharsSequenceType.Letters)));
			}

			if (InputService.GetBuyItemButton())
			{
	
				bool isSucess = _shopService
					.TryBuyShopItem(ShopItemType.ResetStatistics, _gameStatisticsService.ResetStatistics);

				if (isSucess)
				{
					_coroutinePerformer.StartPerform(_playerDataProvider.Save());
				}
			}
		}
	}
}