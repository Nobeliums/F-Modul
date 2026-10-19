using System.Collections;
using _Project.Develop.Runtime.Utilities.ConfigManagement;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.LoadingScreen;
using _Project.Develop.Runtime.Utilities.ResourcesManagement;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Develop.Runtime.Infrastructure.EntryPoint
{
	public class EntryPointRegistrations
	{
		public static void Process(DIContainer container)
		{
			container.RegistryAsSingle(CreateLoadService);
			container.RegistryAsSingle<ICoroutinePerformer>(CreateCoroutinePerformer);
			container.RegistryAsSingle(CreateConfigService);
			container.RegistryAsSingle(CreateSceneLoaderService);
			container.RegistryAsSingle(CreateLoadingScreen);
			container.RegistryAsSingle(CreateSceneSwitcherService);
		}

		private static SceneSwitcherService CreateSceneSwitcherService(DIContainer c)
			=> new SceneSwitcherService(c);
		
		private static ResourceLoadService CreateLoadService(DIContainer c)
		{
			return new ResourceLoadService();
		}

		private static CoroutinePerformer CreateCoroutinePerformer(DIContainer c)
		{
			ResourceLoadService loadService = c.Resolve<ResourceLoadService>();
    
			CoroutinePerformer performerPrefab = loadService.Load<CoroutinePerformer>("CoroutinePerformer");
			return Object.Instantiate(performerPrefab);
		}

		private static ConfigService CreateConfigService(DIContainer c)
		{
			ResourceLoadService loadService = c.Resolve<ResourceLoadService>();
            
			return new ConfigService(new ResourcesConfigLoader(loadService));
		}

		private static ILoadingScreen CreateLoadingScreen(DIContainer c)
		{
			ResourceLoadService loadService = c.Resolve<ResourceLoadService>();
			
			StandartLoadingScreen screen = loadService.Load<StandartLoadingScreen>("LoadingScreen");

			return Object.Instantiate(screen);
		}

		private static SceneLoaderService CreateSceneLoaderService(DIContainer c) => new SceneLoaderService();
	}
}