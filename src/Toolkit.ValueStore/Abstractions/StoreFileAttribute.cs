namespace Toolkit.ValueStore.Abstractions;

/// <summary>Specifies the file name used to persist a value type.</summary>
/// <param name="fileName">The file name, without a directory path.</param>
[AttributeUsage(AttributeTargets.Class)]
public sealed class StoreFileAttribute(string fileName) : Attribute
{
	/// <summary>Gets the file name.</summary>
	public string FileName { get; } = fileName;
}
