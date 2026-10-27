using System;
using _Project.Develop.Runtime.Meta.Features.GameStatistics;
using _Project.Develop.Runtime.UI.View;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.UI.Presenters
{
	public class GameStatisticPresenter : IPresenter
	{
		private IReadOnlyReactiveVariable<int> _loseStatistic;
		private IReadOnlyReactiveVariable<int> _winStatistic;

		private TextBoxView _loseTextBoxView;
		private TextBoxView _winTextBoxView;

		private IDisposable _loseSubscription;
		private IDisposable _winSubscription;

		public GameStatisticPresenter(
			IReadOnlyReactiveVariable<int> loseStatistic,
			IReadOnlyReactiveVariable<int> winStatistic,
			TextBoxView loseTextBoxView,
			TextBoxView winTextBoxView)
		{
			_loseStatistic = loseStatistic;
			_winStatistic = winStatistic;
			_loseTextBoxView = loseTextBoxView;
			_winTextBoxView = winTextBoxView;
		}
		
		public TextBoxView LoseTextBoxView => _loseTextBoxView;
		public TextBoxView WinTextBoxView => _winTextBoxView;
		
		public void Initialize()
		{
			_loseTextBoxView.SetText($"Loses: {_loseStatistic.Value}");
			_winTextBoxView.SetText($"Wins: {_winStatistic.Value.ToString()}");
	
			_loseSubscription = _loseStatistic.Subscribe(OnLoseCountChanged);
			_winSubscription = _winStatistic.Subscribe(OnWinCountChanged);
		}
		
		public void Dispose()
		{
			_loseSubscription.Dispose();
			_winSubscription.Dispose();
		}

		private void OnLoseCountChanged(int oldValue, int newValue)
		{
			_loseTextBoxView.SetText($"Loses: {newValue}");
		}

		private void OnWinCountChanged(int oldValue, int newValue)
		{
			_winTextBoxView.SetText($"Wins: {newValue}");
		}
	}
}