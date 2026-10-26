using System.Collections.Generic;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.UI.Presenters;

namespace _Project.Develop.Runtime.UI.Core.MainMenu
{
	public class MainMenuPresenter : IPresenter
	{
		private MainMenuView _mainMenuView;

		private ProjectPresenterFactory _factory;
		
		private List<IPresenter> _children = new();

		public MainMenuPresenter(MainMenuView mainMenuView,  ProjectPresenterFactory factory)
		{
			_mainMenuView = mainMenuView;
			_factory = factory;
		}
		
		public void Initialize()
		{
			CreateHorizontalGameStatisticsView();
			
			foreach(var child in _children)
				child.Initialize();
		}
		
		public void Dispose()
		{
			foreach(var child in _children)
				child.Dispose();
			
			_children.Clear();
		}

		private void CreateHorizontalGameStatisticsView()
		{
			HorizontalGameStatisticsViewPresenter gameStatisticsPresenter = _factory
				.CreateHorizontalGameStatisticsViewPresenter(_mainMenuView.WalletView, _mainMenuView.GameStatisticsView);

			_children.Add(gameStatisticsPresenter);
		}
	}
}