using System;
using System.Collections;
using _Project.Develop.Runtime.Gameplay.Configs;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
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
		}

		public override IEnumerator Initialize()
		{
			Debug.Log("[MainMenuBootstrap] initialized]");
			
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
		}
	}
}
