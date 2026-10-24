using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Gameplay.Utilities.WalletService;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Utilities.GameStatistics;
using _Project.Develop.Runtime.Meta.Utilities.ShopManagement;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using _Project.Develop.Runtime.Utilities.Input;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Utilities.MainMenuCycle
{
	public class MainMenuCycle
	{
		private DIContainer _container;
		private ICoroutinePerformer _coroutinePerformer;
		private SceneSwitcherService _sceneSwitcherService;

		public MainMenuCycle(DIContainer container)
		{
			_container = container;
		}

		public void Run()
		{
			_coroutinePerformer = _container.Resolve<ICoroutinePerformer>();
			_sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
			
			GameStatisticsService stats = _container.Resolve<GameStatisticsService>();
			
			Debug.Log("S - Текущая статистика");
			Debug.Log($"G - Купить сброс статистики");
		}
		
		public void ProcessUpdate()
		{
			if (_container == null)
				return;

			if (InputService.GetStatisticsButton())
			{
				GameStatisticsService stats = _container.Resolve<GameStatisticsService>();
				WalletService wallet = _container.Resolve<WalletService>();

				Debug.Log($"Ваша статистика: Выигрышей - {stats.WinGameCount.Value} Проигрышей - {stats.LoseGameCount.Value}");
				Debug.Log($"Ваше золото: {wallet.GetCurrency(WalletCurrencyType.Gold).Value}");
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
				GameStatisticsService statistics = _container.Resolve<GameStatisticsService>();
	
				bool isSucess = _container.Resolve<ShopService>()
					.TryBuyShopItem(ShopItemType.ResetStatistics, statistics.ResetStatistics);

				if (isSucess)
				{
					PlayerDataProvider playerData = _container.Resolve<PlayerDataProvider>();

					_container.Resolve<ICoroutinePerformer>().StartPerform(playerData.Save());
				}
			}
		}
	}
}