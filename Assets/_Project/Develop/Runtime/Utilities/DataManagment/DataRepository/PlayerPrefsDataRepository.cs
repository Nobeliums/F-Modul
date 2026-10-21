using System;
using System.Collections;
using UnityEngine;

namespace _Project.Develop.Runtime.Utilities.DataManagment.DataRepository
{
	public class PlayerPrefsDataRepository : IDataRepository
	{
		public IEnumerator Read(string key, Action<string> onRead)
		{
			string result = PlayerPrefs.GetString(key);
			
			onRead(result);
			
			yield break;
		}

		public IEnumerator Write(string key, string value)
		{
			PlayerPrefs.SetString(key, value);
			
			yield break;
		}

		public IEnumerator Remove(string key)
		{
			PlayerPrefs.DeleteKey(key);
			
			yield break;
		}

		public IEnumerator Exists(string key, Action<bool> onExist)
		{
			PlayerPrefs.HasKey(key);
			
			yield break;
		}
	}
}