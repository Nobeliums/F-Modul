using System;
using System.Collections;
using _Project.Develop.Runtime.Features.Gamecycle;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
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
		[SerializeField] private TestGameplay _testGameplay;
		private EntitiesLifeContext _entitiesContext;

		public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
		{
			_container = container;

			if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
				throw new ArgumentException($"{nameof(sceneArgs)} must be of type GameplayInputArgs");

			GameplayContextRegistrations.Process(_container, gameplayInputArgs);

			_container.Initialize();
			_testGameplay.Initialize(_container);
		}

		public override IEnumerator Initialize()
		{
			// _gameCycle = _container.Resolve<GameplayCycle>();
			// _gameCycle.Initialize();
			_entitiesContext =  _container.Resolve<EntitiesLifeContext>();

			yield break;
		}

		public override void Run()
		{
			// _gameCycle.Run();
			_testGameplay.Run();
		}

		private void Update()
		{
			_entitiesContext?.Update(Time.deltaTime);
			
			// if (_gameCycle == null)
				// return;

			// _gameCycle.ProcessUpdate();
		}

		private void OnDestroy()
		{
			// _gameCycle.Dispose();
		}
	}
}
