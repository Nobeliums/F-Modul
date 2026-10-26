using System.Collections.Generic;
using _Project.Develop.Runtime.Meta.Features.GameStatistics;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Core.MainMenu;
using _Project.Develop.Runtime.UI.View;

namespace _Project.Develop.Runtime.UI.Presenters
{
	public class HorizontalGameStatisticsViewPresenter : IPresenter
	{
		private readonly ViewFactory _viewFactory;
		private readonly ProjectPresenterFactory _projectPresenterFactory;
		private readonly WalletService _walletService;
		private readonly List<CurrencyPresenter> _currencyPresenters = new();
		
		private readonly IconTextListView _iconTextListView;
		private readonly TextBoxListView _textBoxListView;
		
		private GameStatisticPresenter _gameStatisticsPresenter;

		public HorizontalGameStatisticsViewPresenter(
			ViewFactory viewFactory,
			ProjectPresenterFactory presenterFactory,
			WalletService walletService,
			IconTextListView iconTextListView,
			TextBoxListView textBoxListView)
		{
			_viewFactory = viewFactory;
			_projectPresenterFactory = presenterFactory;
			_walletService = walletService;
			_iconTextListView = iconTextListView;
			_textBoxListView = textBoxListView;
		}

		public void Initialize()
		{
			foreach (WalletCurrencyType currencyType in _walletService.AvailableCurrencies)
			{
				IconTextView view = _viewFactory.Create<IconTextView>(ViewIds.CurrencyView);

				CurrencyPresenter presenter = _projectPresenterFactory.CreateCurrencyPresenter(
					view,
					currencyType,
					_walletService.GetCurrency(currencyType));
				
				_currencyPresenters.Add(presenter);
				_iconTextListView.Add(view);
	
				presenter.Initialize();
			}
			
			TextBoxView loseBoxView = _viewFactory.Create<TextBoxView>(ViewIds.GameStatTextView);
			TextBoxView winBoxView = _viewFactory.Create<TextBoxView>(ViewIds.GameStatTextView);

			_gameStatisticsPresenter = _projectPresenterFactory.CreateGameStatisticPresenter(loseBoxView, winBoxView);
			_gameStatisticsPresenter.Initialize();
			_textBoxListView.Add(loseBoxView, winBoxView);
		}
		
		public void Dispose()
		{
			foreach (var currencyPresenter in _currencyPresenters)
			{
				_iconTextListView.Remove(currencyPresenter.View);
				_viewFactory.Release(currencyPresenter.View);

				currencyPresenter.Dispose();
			}
			
			_textBoxListView.Remove(_gameStatisticsPresenter.LoseTextBoxView);
			_textBoxListView.Remove(_gameStatisticsPresenter.WinTextBoxView);
			
			_viewFactory.Release(_gameStatisticsPresenter.LoseTextBoxView);
			_viewFactory.Release(_gameStatisticsPresenter.WinTextBoxView);
			
			_gameStatisticsPresenter.Dispose();
		}
	}
}