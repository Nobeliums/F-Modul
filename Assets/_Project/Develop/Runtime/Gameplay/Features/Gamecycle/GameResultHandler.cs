using _Project.Develop.Runtime.Meta.Features.GameStatistics;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProvider;

namespace _Project.Develop.Runtime.Features.Gamecycle
{
	public class GameResultHandler
	{
		private readonly GameStatisticsService _gameStatisticsService;
		private readonly PlayerDataProvider _playerDataProvider;
		private readonly ICoroutinePerformer _coroutinePerformer;

		public GameResultHandler(
			GameStatisticsService gameStatisticsService,
			PlayerDataProvider playerDataProvider,
			ICoroutinePerformer coroutinePerformer)
		{
			_gameStatisticsService = gameStatisticsService;
			_playerDataProvider = playerDataProvider;
			_coroutinePerformer = coroutinePerformer;
		}

		public void OnWin()
		{
			_gameStatisticsService.OnWinGame();
		}

		public void OnDefeat()
		{
			_gameStatisticsService.OnLoseGame();
			_coroutinePerformer.StartPerform(_playerDataProvider.SaveAsync());
		}
	}
}
