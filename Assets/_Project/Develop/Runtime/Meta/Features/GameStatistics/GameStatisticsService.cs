using _Project.Develop.Runtime.Utilities.DataManagment;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Meta.Features.GameStatistics
{
	public class GameStatisticsService : IDataReader<PlayerData>, IDataWriter<PlayerData>
	{
		private ReactiveVariable<int> _loseGameCount;
		private ReactiveVariable<int> _winGameCount;
		
		public IReadOnlyReactiveVariable<int> LoseGameCount => _loseGameCount;
		public IReadOnlyReactiveVariable<int> WinGameCount => _winGameCount;

		public GameStatisticsService(PlayerDataProvider playerDataProvider)
		{
			_loseGameCount = new ReactiveVariable<int>();
			_winGameCount = new ReactiveVariable<int>();
			
			playerDataProvider.RegisterDataReader(this);
			playerDataProvider.RegisterDataWriter(this);
		}

		public void OnLoseGame()
		{
			_loseGameCount.Value++;
		}

		public void OnWinGame()
		{
			_winGameCount.Value++;
		}

		public void ResetStatistics()
		{
			_loseGameCount.Value = 0;
			_winGameCount.Value = 0;
		}

		public void ReadFrom(PlayerData data)
		{
			_loseGameCount.Value = data.LoseCount;
			_winGameCount.Value = data.WinsCount;
		}

		public void WriteTo(PlayerData data)
		{
			data.WinsCount = _winGameCount.Value;
			data.LoseCount = _loseGameCount.Value;
		}
	}
}