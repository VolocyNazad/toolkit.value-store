using Toolkit.ValueStore.Abstractions;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Toolkit.ValueStore.Serialization;

/// <summary>Serializes value store files as YAML using camelCase naming.</summary>
public sealed class YamlValueStoreSerializer : IValueStoreSerializer
{
	private readonly ISerializer _serializer = new SerializerBuilder()
		.WithNamingConvention(CamelCaseNamingConvention.Instance)
		.Build();
	private readonly IDeserializer _deserializer = new DeserializerBuilder()
		.WithNamingConvention(CamelCaseNamingConvention.Instance)
		.Build();

	/// <summary>Gets the YAML file extension.</summary>
	public string FileExtension => ".yml";

	/// <summary>Serializes a value to YAML.</summary>
	/// <typeparam name="T">The value type.</typeparam>
	/// <param name="value">The value to serialize.</param>
	/// <returns>The serialized YAML text.</returns>
	public string Serialize<T>(T value) => _serializer.Serialize(value);

	/// <summary>Deserializes YAML into a value.</summary>
	/// <typeparam name="T">The value type.</typeparam>
	/// <param name="content">The YAML text.</param>
	/// <returns>The deserialized value, or <see langword="null"/> when the text is empty.</returns>
	public T? Deserialize<T>(string content) => _deserializer.Deserialize<T>(content);
}
