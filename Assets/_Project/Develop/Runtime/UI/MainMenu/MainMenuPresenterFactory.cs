using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.UI.Presenters;

namespace _Project.Develop.Runtime.UI.Core.MainMenu
{
	public class MainMenuPresenterFactory
	{
		private DIContainer _container;

		public MainMenuPresenterFactory(DIContainer container)
		{
			_container = container;
		}
		
		public MainMenuPresenter CreateMainMenuPresenter(MainMenuView mainMenuView)
		{
			return new MainMenuPresenter(mainMenuView,  _container.Resolve<ProjectPresenterFactory>());
		}
	}
}