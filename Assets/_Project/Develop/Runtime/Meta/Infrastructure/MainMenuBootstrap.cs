using System.Collections;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
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
			
			_performer = _container.Resolve<ICoroutinePerformer>();
			_sceneSwitcher = _container.Resolve<SceneSwitcherService>();
			
			yield break;
		}

		public override void Run()
		{
		}
	}
}
