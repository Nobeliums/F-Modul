namespace _Project.Develop.Runtime.Utilities.DataManagment.Serializers
{
	public interface IDataSerializer
	{
		string Serialize<TData>(TData value);
		
		TData Deserialize<TData>(string value);
	}
}
