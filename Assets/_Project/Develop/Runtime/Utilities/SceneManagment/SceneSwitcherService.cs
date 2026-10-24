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
		private readonly DIContainer _projectContainer;
		private DIContainer _sceneContainer;

		public SceneSwitcherService(DIContainer projectContainer)
		{
			_projectContainer = projectContainer;
			_loadingScreen = _projectContainer.Resolve<ILoadingScreen>();
			_sceneLoaderService = _projectContainer.Resolve<SceneLoaderService>();
		}

		public IEnumerator ProcessSceneSwitchTo(string sceneName, IInputSceneArgs sceneArgs = null)
		{
			_sceneContainer?.Dispose();
			
			_loadingScreen.Show();
			
			yield return _sceneLoaderService.LoadScene(Scenes.Empty);
			yield return _sceneLoaderService.LoadScene(sceneName);
			
			SceneBootstrap bootstrap = Object.FindObjectOfType<SceneBootstrap>();

			if (bootstrap == null)
				throw new EntryPointNotFoundException("SceneBootstrap not found");
			
			_sceneContainer = new DIContainer(_projectContainer);
			
			bootstrap.ProcessRegistrations(_sceneContainer, sceneArgs);
			
			yield return bootstrap.Initialize();
			bootstrap.Run();
			
			_loadingScreen.Hide();
		}
	}
}