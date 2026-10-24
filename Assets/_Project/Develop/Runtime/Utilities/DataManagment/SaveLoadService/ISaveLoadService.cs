using System;
using System.Collections;

namespace _Project.Develop.Runtime.Utilities.DataManagment.SaveLoadService
{
	public interface ISaveLoadService
	{
		IEnumerator Save<TData>(TData data) where TData : ISaveData;
		IEnumerator Load<TData>(Action<TData> onLoad) where TData : ISaveData;
		IEnumerator Remove<TData>() where TData : ISaveData;
		IEnumerator Exists<TData>(Action<bool> onExist) where TData : ISaveData;
	}
}