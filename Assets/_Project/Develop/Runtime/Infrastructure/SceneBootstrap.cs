using System.Collections;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure
{
	public abstract class SceneBootstrap : MonoBehaviour
	{
		public abstract IEnumerator Initialize(DIContainer c, IInputSceneArgs sceneArgs = null);
		
		public abstract void Run();
	}
}