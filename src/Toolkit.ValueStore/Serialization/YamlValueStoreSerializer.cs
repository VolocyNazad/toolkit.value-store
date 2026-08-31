using Toolkit.ValueStore.Abstractions;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Toolkit.ValueStore.Serialization;

public sealed class YamlValueStoreSerializer : IValueStoreSerializer
{
	private readonly ISerializer _serializer = new SerializerBuilder()
		.WithNamingConvention(CamelCaseNamingConvention.Instance)
		.Build();
	private readonly IDeserializer _deserializer = new DeserializerBuilder()
		.WithNamingConvention(CamelCaseNamingConvention.Instance)
		.Build();

	public string FileExtension => ".yml";

	public string Serialize<T>(T value) => _serializer.Serialize(value);

	public T? Deserialize<T>(string content) => _deserializer.Deserialize<T>(content);
}
