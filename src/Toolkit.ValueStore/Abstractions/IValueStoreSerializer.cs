namespace Toolkit.ValueStore.Abstractions;

public interface IValueStoreSerializer
{
	string FileExtension { get; }
	string Serialize<T>(T value);
	T? Deserialize<T>(string content);
}
