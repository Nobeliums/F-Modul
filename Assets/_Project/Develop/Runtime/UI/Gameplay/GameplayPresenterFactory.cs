using _Project.Develop.Runtime.Features.Gamecycle;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.UI.Core.Popups;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.UI.Core.Gameplay
{
	public class GameplayPresenterFactory
	{
		private DIContainer _container;

		public GameplayPresenterFactory(DIContainer container)
		{
			_container = container;
		}

		public GameplayPresenter CreateGameplayPresenter(GameplayView gameplayView)
		{
			return new GameplayPresenter(gameplayView, 
				_container.Resolve<GameplayCycle>(), 
				_container.Resolve<GameplayPopupService>());
		}

		public GameResultPopupPresenter CreateGameResultPopupPresenter(GameResultPopupView gameResultPopupView)
		{
			return new GameResultPopupPresenter(
				gameResultPopupView,
				_container.Resolve<GameplayCycle>(),
				_container.Resolve<ICoroutinePerformer>(),
				_container.Resolve<ViewFactory>(),
				_container.Resolve<SceneSwitcherService>(),
				_container.Resolve<GameplayInputArgs>());
		}
	}
}