namespace Toolkit.ValueStore.Abstractions;

/// <summary>Serializes and deserializes value store files.</summary>
public interface IValueStoreSerializer
{
	/// <summary>Gets the file extension, including the leading dot.</summary>
	string FileExtension { get; }

	/// <summary>Serializes a value to text.</summary>
	/// <typeparam name="T">The value type.</typeparam>
	/// <param name="value">The value to serialize.</param>
	/// <returns>The serialized text.</returns>
	string Serialize<T>(T value);

	/// <summary>Deserializes text into a value.</summary>
	/// <typeparam name="T">The value type.</typeparam>
	/// <param name="content">The serialized text.</param>
	/// <returns>The deserialized value, or <see langword="null"/> when the text is empty.</returns>
	T? Deserialize<T>(string content);
}
