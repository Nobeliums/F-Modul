using System;
using System.Collections;
using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Infrastructure
{
	public class MainMenuBootstrap : SceneBootstrap
	{
		private DIContainer _container;

		public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
		{
			_container  = container;
			
			MainMenuContextRegistrations.Process(container);
		}

		public override IEnumerator Initialize()
		{
			Debug.Log("[MainMenuBootstrap] initialized]");
			
			yield break;
		}

		public override void Run()
		{
			Debug.Log("[MainMenuBootstrap] Run");
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				SceneSwitcherService switcherService = _container.Resolve<SceneSwitcherService>();
				ICoroutinePerformer performer = _container.Resolve<ICoroutinePerformer>();
				performer.StartPerform(switcherService.ProcessSceneSwitchTo(Scenes.Gameplay, new GameplayInputArgs(GameCharsSequenceType.Digits)));
			}

			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				SceneSwitcherService switcherService = _container.Resolve<SceneSwitcherService>();
				ICoroutinePerformer performer = _container.Resolve<ICoroutinePerformer>();
				performer.StartPerform(switcherService.ProcessSceneSwitchTo(Scenes.Gameplay, new GameplayInputArgs(GameCharsSequenceType.Letters)));
			}
		}
	}
}
