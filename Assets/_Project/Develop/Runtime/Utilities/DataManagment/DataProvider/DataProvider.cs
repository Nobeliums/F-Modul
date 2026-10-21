using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Develop.Runtime.Utilities.DataManagment.SaveLoadService;

namespace _Project.Develop.Runtime.Utilities.DataManagment.DataProvider
{
	public abstract class DataProvider<TData> where TData : ISaveData
	{
		private readonly ISaveLoadService _saveLoadService;
		
		private readonly List<IDataReader<TData>> _readers;
		private readonly List<IDataWriter<TData>> _writers;
		
		private TData _data;

		public DataProvider(ISaveLoadService saveLoadService)
		{
			_saveLoadService = saveLoadService;
		}

		public IEnumerator Load()
		{
			yield return _saveLoadService.Load<TData>(result => _data = result);
		}

		public IEnumerator Save()
		{
			yield return _saveLoadService.Save(_data);
		}

		public IEnumerator Exists(Action<bool> onExists)
		{
			yield return _saveLoadService.Exists<TData>(result => onExists?.Invoke(result));
		}

		public IEnumerator Reset()
		{
			_data = GetOriginData();

			yield break;
		}

		protected abstract TData GetOriginData();
	}
}