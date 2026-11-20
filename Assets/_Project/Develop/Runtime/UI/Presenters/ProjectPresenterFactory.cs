using _Project.Develop.Runtime.Gameplay.Configs.Levels;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Configs;
using _Project.Develop.Runtime.Meta.Features.GameStatistics;
using _Project.Develop.Runtime.Meta.Features.LevelsProgression;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Core.Popups;
using _Project.Develop.Runtime.UI.View;
using _Project.Develop.Runtime.Utilities.ConfigManagement;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.UI.Presenters
{
	public class ProjectPresenterFactory
	{
		private DIContainer _container;

		public ProjectPresenterFactory(DIContainer container)
		{
			_container = container;
		}
		
		public GameStatisticPresenter CreateGameStatisticPresenter(
			TextBoxView loseBoxView,
			TextBoxView winBoxView)
		{
			GameStatisticsService statService = _container.Resolve<GameStatisticsService>();
			
			return new GameStatisticPresenter(
				statService.LoseGameCount,
				statService.WinGameCount,
				loseBoxView,
				winBoxView);
		}

		public CurrencyPresenter CreateCurrencyPresenter(
			IconTextView currencyView,
			WalletCurrencyType currencyType,
			IReadOnlyReactiveVariable<int> currencyAmount)
		{
			StartWalletViewConfig walletViewConfig = _container.Resolve<ConfigService>().GetConfig<StartWalletViewConfig>();
			CurrencyPresenter currencyPresenter = new CurrencyPresenter(
				currencyView,
				currencyType,
				currencyAmount,
				walletViewConfig);

			return currencyPresenter;
		}

		public HorizontalGameStatisticsViewPresenter CreateHorizontalGameStatisticsViewPresenter(
			IconTextListView iconTextListView,
			TextBoxListView textBoxListView)
		{
			return new HorizontalGameStatisticsViewPresenter(
				_container.Resolve<ViewFactory>(),
				this,
				_container.Resolve<WalletService>(),
				iconTextListView,
				textBoxListView);
		}

		public SelectLevelPopupViewPresenter CreateSelectLevelPopupViewPresenter(SelectLevelPopupView view)
		{
			return new SelectLevelPopupViewPresenter(
				view,
				_container.Resolve<ICoroutinePerformer>(),
				_container.Resolve<ViewFactory>(),
				_container.Resolve<ProjectPresenterFactory>(),
				_container.Resolve<ConfigService>().GetConfig<LevelListConfig>());
		}

		public LevelButtonPresenter CreateLevelButtonPresenter(
			int levelNumber,
			ButtonView buttonView)
		{
			return new LevelButtonPresenter(
				_container.Resolve<LevelsProgressionService>(),
				_container.Resolve<SceneSwitcherService>(),
				_container.Resolve<ICoroutinePerformer>(),
				levelNumber,
				buttonView);
		}
	}
}