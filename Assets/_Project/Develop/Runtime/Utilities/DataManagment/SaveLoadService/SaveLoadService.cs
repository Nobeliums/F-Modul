using System;
using System.Collections;
using _Project.Develop.Runtime.Utilities.DataManagment.DataRepository;
using _Project.Develop.Runtime.Utilities.DataManagment.KeysStorage;
using _Project.Develop.Runtime.Utilities.DataManagment.Serializers;

namespace _Project.Develop.Runtime.Utilities.DataManagment.SaveLoadService
{
	public class SaveLoadService : ISaveLoadService
	{
		private IDataRepository _dataRepository;
		private IDataSerializer _dataSerializer;
		private IDataKeysStorage _dataKeysStorage;

		public SaveLoadService(
			IDataRepository dataRepository,
			IDataSerializer dataSerializer,
			IDataKeysStorage dataKeysStorage)
		{
			_dataRepository = dataRepository;
			_dataSerializer = dataSerializer;
			_dataKeysStorage = dataKeysStorage;
		}


		public IEnumerator Save<TData>(TData data) where TData : ISaveData
		{
			string key = _dataKeysStorage.GetKeyFor<TData>();
			
			string serialized = _dataSerializer.Serialize(data);
			
			yield return _dataRepository.Write(key, serialized);
		}

		public IEnumerator Load<TData>(Action<TData> onLoad) where TData : ISaveData
		{
			string key =  _dataKeysStorage.GetKeyFor<TData>();

			string loadedTextData = "";
			
			yield return _dataRepository.Read(key, result => loadedTextData = result);
			
			TData loadedData = _dataSerializer.Deserialize<TData>(loadedTextData);
			onLoad?.Invoke(loadedData);
		}

		public IEnumerator Remove<TData>() where TData : ISaveData
		{
			string key =  _dataKeysStorage.GetKeyFor<TData>();
			
			yield return _dataRepository.Remove(key);
		}

		public IEnumerator Exists<TData>(Action<bool> onExist) where TData : ISaveData
		{
			string key = _dataKeysStorage.GetKeyFor<TData>();
			
			yield return _dataRepository.Exists(key,  result => onExist?.Invoke(result));
		}
	}
}