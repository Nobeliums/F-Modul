using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Develop.Runtime.Utilities.DataManagment.SaveLoadService;

namespace _Project.Develop.Runtime.Utilities.DataManagment.DataProvider
{
	public abstract class DataProvider<TData> where TData : ISaveData
	{
		private readonly ISaveLoadService _saveLoadService;
		
		private readonly List<IDataReader<TData>> _readers = new();
		private readonly List<IDataWriter<TData>> _writers = new();
		
		private TData _data;

		public DataProvider(ISaveLoadService saveLoadService)
		{
			_saveLoadService = saveLoadService;
		}

		public IEnumerator Load()
		{
			yield return _saveLoadService.Load<TData>(result => _data = result);
			
			SendDataToReaders();
		}

		public IEnumerator Save()
		{
			UpdateDataFromWriters();
			
			yield return _saveLoadService.Save(_data);
		}

		public IEnumerator Exists(Action<bool> onExists)
		{
			yield return _saveLoadService.Exists<TData>(result => onExists?.Invoke(result));
		}

		public IEnumerator Reset()
		{
			_data = GetOriginData();
			
			SendDataToReaders();

			yield break;
		}

		public void RegisterDataWriter(IDataWriter<TData> writer)
		{
			if (_writers.Contains(writer))
				throw new ArgumentException("DataWriter already registered");
			
			_writers.Add(writer);
		}

		public void RegisterDataReader(IDataReader<TData> reader)
		{
			if (_readers.Contains(reader))
				throw new ArgumentException("DataReader already registered");
			
			_readers.Add(reader);
		}

		protected abstract TData GetOriginData();

		private void SendDataToReaders()
		{
			foreach (IDataReader<TData> reader in _readers)
			{
				reader.ReadFrom(_data);
			}
		}

		private void UpdateDataFromWriters()
		{
			foreach (IDataWriter<TData> writer in _writers)
			{
				writer.WriteTo(_data);
			}
		}
	}
}