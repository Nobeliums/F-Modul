using System.Collections;
using _Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure
{
	public abstract class SceneBootstrap : MonoBehaviour
	{
		public abstract IEnumerator Initialize(DIContainer c);
		
		public abstract void Run();
	}
}