using System;
using System.Collections;
using System.IO;

namespace _Project.Develop.Runtime.Utilities.DataManagment.DataRepository
{
	public class LocalFileDataRepository : IDataRepository
	{
		private readonly string _fileNameExtension = "json";
		private readonly string _filePath;

		public LocalFileDataRepository(string filePath)
		{
			_filePath = filePath;
		}
		
		public IEnumerator Read(string key, Action<string> onRead)
		{
			string result = File.ReadAllText(GetFullPathFor(key));
			
			onRead?.Invoke(result);
			
			yield break;
		}

		public IEnumerator Write(string key, string value)
		{
			File.WriteAllText(GetFullPathFor(key), value);
			
			yield break;
		}

		public IEnumerator Remove(string key)
		{
			File.Delete(GetFullPathFor(key));
			
			yield break;
		}

		public IEnumerator Exists(string key, Action<bool> onExist)
		{
			bool isExists = File.Exists(GetFullPathFor(key));
			
			onExist?.Invoke(isExists);
			
			yield break;
		}

		private string GetFullPathFor(string key)
		{
			return Path.Combine(_filePath, $"{key}.{_fileNameExtension}");
		}
	}
}