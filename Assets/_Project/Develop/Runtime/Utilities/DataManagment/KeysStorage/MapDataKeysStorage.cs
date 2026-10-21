using System;
using System.Collections.Generic;

namespace _Project.Develop.Runtime.Utilities.DataManagment.KeysStorage
{
	public class MapDataKeysStorage : IDataKeysStorage
	{
		private readonly Dictionary<Type, string> Keys = new Dictionary<Type, string>()
		{
			
		};

		public string GetKeyFor<TData>() where TData : ISaveData
		{
			return Keys[typeof(TData)];
		}
	}
}