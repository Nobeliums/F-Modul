using System;
using UnityEngine;

namespace _Project.Develop.Runtime.Utilities.LoadingScreen
{
	public class StandartLoadingScreen : MonoBehaviour, ILoadingScreen
	{
		public bool IsShowing { get; private set; }

		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}

		public void Show()
		{
			IsShowing = true;
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			IsShowing = false;
			gameObject.SetActive(false);
		}
	}
}