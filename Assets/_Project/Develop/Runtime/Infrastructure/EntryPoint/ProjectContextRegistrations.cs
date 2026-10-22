using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.Utilities.WalletService;
using _Project.Develop.Runtime.Utilities.ConfigManagement;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Utilities.GameStatistics;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using _Project.Develop.Runtime.Utilities.DataManagment.DataRepository;
using _Project.Develop.Runtime.Utilities.DataManagment.KeysStorage;
using _Project.Develop.Runtime.Utilities.DataManagment.SaveLoadService;
using _Project.Develop.Runtime.Utilities.DataManagment.Serializers;
using _Project.Develop.Runtime.Utilities.LoadingScreen;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.ResourcesManagement;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Develop.Runtime.Infrastructure.EntryPoint
{
	public class ProjectContextRegistrations
	{
		public static void Process(DIContainer container)
		{
			container.RegistryAsSingle(CreateLoadService);
			container.RegistryAsSingle<ICoroutinePerformer>(CreateCoroutinePerformer);
			container.RegistryAsSingle(CreateConfigService);
			container.RegistryAsSingle(CreateSceneLoaderService);
			container.RegistryAsSingle(CreateLoadingScreen);
			container.RegistryAsSingle(CreateSceneSwitcherService);
			container.RegistryAsSingle(CreatePlayerDataProvider);
			container.RegistryAsSingle(CreateWalletService).NonLazy();
			container.RegistryAsSingle(CreateSaveLoadService);
			container.RegistryAsSingle(CreateGameStatisticsService).NonLazy();
		}

		private static GameStatisticsService CreateGameStatisticsService(DIContainer c)
		{
			return new GameStatisticsService(c.Resolve<PlayerDataProvider>());
		}

		private static WalletService CreateWalletService(DIContainer c)
		{
			Dictionary<WalletCurrencyType, ReactiveVabiable<int>> currencies = new();

			foreach (WalletCurrencyType currencyType in Enum.GetValues(typeof(WalletCurrencyType)))
			{
				currencies[currencyType] = new ReactiveVabiable<int>();
			}
			
			return new WalletService(
				currencies,
				c.Resolve<PlayerDataProvider>());
		}

		private static PlayerDataProvider CreatePlayerDataProvider(DIContainer c)
			=> new PlayerDataProvider(
				c.Resolve<ISaveLoadService>(),
				c.Resolve<ConfigService>());

		private static ISaveLoadService CreateSaveLoadService(DIContainer c)
		{
			string path = Application.isEditor ? Application.dataPath : Application.persistentDataPath;

			IDataRepository dataRepository = new LocalFileDataRepository(path);
			IDataKeysStorage dataKeysStorage = new MapDataKeysStorage();
			IDataSerializer serializer = new JsonDataSerializer();
			
			return new SaveLoadService(dataRepository, serializer, dataKeysStorage);

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