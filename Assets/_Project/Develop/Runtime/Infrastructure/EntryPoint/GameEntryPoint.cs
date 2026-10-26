using System;
using System.Collections;
using _Project.Develop.Runtime.Utilities.ConfigManagement;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using _Project.Develop.Runtime.Utilities.LoadingScreen;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.EntryPoint
{
	public class GameEntryPoint : MonoBehaviour
	{
		private void Awake()
		{
			SetupAppSettings();
			
			DIContainer projectContainer = new DIContainer();
			
			ProjectContextRegistrations.Process(projectContainer);
			
			projectContainer.Initialize();
			
			projectContainer.Resolve<ICoroutinePerformer>().StartPerform(Initialize(projectContainer));
		}

		private void SetupAppSettings()
		{
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = 60;
		}

		private IEnumerator Initialize(DIContainer container)
		{
			ILoadingScreen loadingScreen = container.Resolve<ILoadingScreen>();
			
			PlayerDataProvider playerDataProvider = container.Resolve<PlayerDataProvider>();
			
			bool isExists = false;
			
			container.Resolve<ICoroutinePerformer>()
				.StartPerform(playerDataProvider.Exists(result => isExists = result));
			
			if (isExists)
				container.Resolve<ICoroutinePerformer>().StartPerform(playerDataProvider.Load());
			else
				container.Resolve<ICoroutinePerformer>().StartPerform(playerDataProvider.Reset());

			yield return container.Resolve<ConfigService>().LoadAsync();

			yield return container.Resolve<SceneSwitcherService>().ProcessSceneSwitchTo(Scenes.MainMenu);

			yield return new WaitForSeconds(1f);
			
			loadingScreen.Hide();
		}
	}
}