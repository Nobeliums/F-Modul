using System;
using System.Collections;
using _Project.Develop.Runtime.Features.Gamecycle;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using _Project.Develop.Runtime.Utilities.Input;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
	public class GameplayBootstrap : SceneBootstrap
	{
		private DIContainer _container;
		private GameplayCycle _gameCycle;

		public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
		{
			_container = container;

			if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
				throw new ArgumentException($"{nameof(sceneArgs)} must be of type GameplayInputArgs");

			GameplayContextRegistrations.Process(_container, gameplayInputArgs);

			_container.Initialize();
		}

		public override IEnumerator Initialize()
		{
			_gameCycle = _container.Resolve<GameplayCycle>();
			_gameCycle.Initialize();

			yield break;
		}

		public override void Run()
		{
			_gameCycle.Run();
		}

		private void Update()
		{
			if (_gameCycle == null)
				return;

			_gameCycle.ProcessUpdate();

			ProcessSceneSwitch();

		}

		private void OnDestroy()
		{
			_gameCycle.Dispose();
		}

		private void ProcessSceneSwitch()
		{
		}
	}
}
