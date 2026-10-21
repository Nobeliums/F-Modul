using System;
using System.Collections;
using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.DataManagment;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using _Project.Develop.Runtime.Utilities.Input;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Infrastructure
{
	public class MainMenuBootstrap : SceneBootstrap
	{
		private DIContainer _container;
		private SceneSwitcherService _sceneSwitcher;
		private ICoroutinePerformer _performer;

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
			
			playerDataProvider.Exists(result => isExists = result);
			
			if (isExists)
				_container.Resolve<ICoroutinePerformer>().StartPerform(playerDataProvider.Load());
			else
				_container.Resolve<ICoroutinePerformer>().StartPerform(playerDataProvider.Reset());
			
			yield break;
		}

		public override void Run()
		{
			Debug.Log("[MainMenuBootstrap] Run");
			_sceneSwitcher = _container.Resolve<SceneSwitcherService>();
			_performer = _container.Resolve<ICoroutinePerformer>();
		}

		private void Update()
		{
			if (_container == null)
				return;

			if (InputService.GetDigitsGameModeButton())
			{
				_performer.StartPerform(_sceneSwitcher.ProcessSceneSwitchTo(
					Scenes.Gameplay,
					new GameplayInputArgs(GameCharsSequenceType.Digits)));
			}

			if (InputService.GetLettersGameModeButton())
			{
				_performer.StartPerform(_sceneSwitcher.ProcessSceneSwitchTo(
					Scenes.Gameplay,
					new GameplayInputArgs(GameCharsSequenceType.Letters)));
			}

			if (Input.GetKeyDown(KeyCode.S))
			{
				PlayerDataProvider playerData = _container.Resolve<PlayerDataProvider>();

				_container.Resolve<ICoroutinePerformer>().StartPerform(playerData.Save());
				
				Debug.Log("[MainMenuBootstrap] Saved player data");
			}
		}
		
		
	}
}
