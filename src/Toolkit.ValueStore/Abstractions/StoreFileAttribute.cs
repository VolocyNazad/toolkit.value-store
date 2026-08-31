namespace Toolkit.ValueStore.Abstractions;

[AttributeUsage(AttributeTargets.Class)]
public sealed class StoreFileAttribute(string fileName) : Attribute
{
	public string FileName { get; } = fileName;
}
