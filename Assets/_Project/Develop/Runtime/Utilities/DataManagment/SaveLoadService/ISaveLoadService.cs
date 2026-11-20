using System;
using System.Collections;

namespace _Project.Develop.Runtime.Utilities.DataManagment.SaveLoadService
{
	public interface ISaveLoadService
	{
		IEnumerator SaveAsync<TData>(TData data) where TData : ISaveData;
		IEnumerator LoadAsync<TData>(Action<TData> onLoad) where TData : ISaveData;
		IEnumerator Remove<TData>() where TData : ISaveData;
		IEnumerator ExistsAsync<TData>(Action<bool> onExist) where TData : ISaveData;
	}
}