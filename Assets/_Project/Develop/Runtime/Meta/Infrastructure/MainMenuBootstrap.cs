using System;
using System.Collections;
using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Gameplay.Features.WalletService;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.DataManagment;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using _Project.Develop.Runtime.Utilities.Input;
using _Project.Develop.Runtime.Meta.Utilities.MainMenuCycle;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Infrastructure
{
	public class MainMenuBootstrap : SceneBootstrap
	{
		private DIContainer _container;
		private SceneSwitcherService _sceneSwitcher;
		private ICoroutinePerformer _performer;
		
		private MainMenuCycle _mainMenuCycle;

		public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
		{
			_container = container;
			
			MainMenuContextRegistrations.Process(container);
			
			_container.Initialize();
		}

		public override IEnumerator Initialize()
		{
			Debug.Log("[MainMenuBootstrap] initialized]");
			
			PlayerDataProvider playerDataProvider = _container.Resolve<PlayerDataProvider>();

			bool isExists = false;
			
			_performer = _container.Resolve<ICoroutinePerformer>();
			_sceneSwitcher = _container.Resolve<SceneSwitcherService>();
			_mainMenuCycle = _container.Resolve<MainMenuCycle>();
			
			_performer.StartPerform(playerDataProvider.Exists(result => isExists = result));
			
			if (isExists)
				_container.Resolve<ICoroutinePerformer>().StartPerform(playerDataProvider.Load());
			else
				_container.Resolve<ICoroutinePerformer>().StartPerform(playerDataProvider.Reset());
			
			yield break;
		}

		public override void Run()
		{
			Debug.Log("[MainMenuBootstrap] Run");
			_mainMenuCycle.Run();
		}

		private void Update()
		{
			if (_mainMenuCycle == null)
				return;
			
			_mainMenuCycle.ProcessUpdate();
		}
	}
}
