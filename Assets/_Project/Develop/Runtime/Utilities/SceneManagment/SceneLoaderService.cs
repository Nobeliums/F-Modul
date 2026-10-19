using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Develop.Runtime.Utilities.SceneManagment
{
	public class SceneLoaderService
	{
		public IEnumerator LoadScene(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
		{
			AsyncOperation loadStatus = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
			
			yield return new WaitWhile(() => loadStatus.isDone == false);
		}

		public IEnumerator UnloadScene(string sceneName)
		{
			AsyncOperation unloadStatus = SceneManager.UnloadSceneAsync(sceneName);
			
			yield return new WaitWhile(() => unloadStatus.isDone ==  false);
		}
	}
}