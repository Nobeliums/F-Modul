using System;
using System.Collections;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
	public class GameplayBootstrap : SceneBootstrap
	{
		private DIContainer _container;
		
		public override IEnumerator Initialize(DIContainer c)
		{
			_container = c;
			
			Debug.Log("[Gameplay Bootstrap] Gameplay Bootstrap initialized]");
			
			yield break;
		}

		public override void Run()
		{
			Debug.Log("[Gameplay Bootstrap] Gameplay Bootstrap Run");
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				SceneSwitcherService switcherService = _container.Resolve<SceneSwitcherService>();
				ICoroutinePerformer performer = _container.Resolve<ICoroutinePerformer>();
				performer.StartPerform(switcherService.ProcessSceneSwitchTo(Scenes.MainMenu));
			}
		}
	}
}