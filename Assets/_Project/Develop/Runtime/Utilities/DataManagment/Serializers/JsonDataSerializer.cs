using Newtonsoft.Json;

namespace _Project.Develop.Runtime.Utilities.DataManagment.Serializers
{
	public class JsonDataSerializer : IDataSerializer
	{
		public string Serialize<TData>(TData value)
		{
			return JsonConvert.SerializeObject(value,
				new JsonSerializerSettings
				{
					Formatting = Formatting.Indented,
					TypeNameHandling = TypeNameHandling.Auto
				});
		}

		public TData Deserialize<TData>(string value)
		{
			return JsonConvert.DeserializeObject<TData>(value, 
				new JsonSerializerSettings
				{
					TypeNameHandling = TypeNameHandling.Auto
				});
		}
	}
}