using System;
using System.Collections;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.LoadingScreen;
using Object = UnityEngine.Object;

namespace _Project.Develop.Runtime.Utilities.SceneManagment
{
	public class SceneSwitcherService
	{
		private readonly SceneLoaderService _sceneLoaderService;
		private readonly ILoadingScreen _loadingScreen;
		private readonly DIContainer _container;

		public SceneSwitcherService(DIContainer container)
		{
			_container = container;
			_loadingScreen = _container.Resolve<ILoadingScreen>();
			_sceneLoaderService = _container.Resolve<SceneLoaderService>();
		}

		public IEnumerator ProcessSceneSwitchTo(string sceneName)
		{
			_loadingScreen.Show();
			
			yield return _sceneLoaderService.LoadScene(Scenes.Empty);
			yield return _sceneLoaderService.LoadScene(sceneName);
			
			SceneBootstrap bootstrap = Object.FindObjectOfType<SceneBootstrap>();

			if (bootstrap == null)
				throw new EntryPointNotFoundException("SceneBootstrap not found");
			
			yield return bootstrap.Initialize(_container);
			bootstrap.Run();
			
			_loadingScreen.Hide();
		}
	}
}