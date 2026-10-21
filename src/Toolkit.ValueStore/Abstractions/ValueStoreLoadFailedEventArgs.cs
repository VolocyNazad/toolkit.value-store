namespace Toolkit.ValueStore.Abstractions;

/// <summary>Provides data for a value store load failure.</summary>
/// <param name="valueType">The type of the value that failed to load.</param>
/// <param name="filePath">The path of the file that failed to load.</param>
/// <param name="exception">The exception that caused the failure.</param>
public sealed class ValueStoreLoadFailedEventArgs(Type valueType, string filePath, Exception exception) : EventArgs
{
	/// <summary>Gets the type of the value that failed to load.</summary>
	public Type ValueType { get; } = valueType;

	/// <summary>Gets the path of the file that failed to load.</summary>
	public string FilePath { get; } = filePath;

	/// <summary>Gets the exception that caused the failure.</summary>
	public Exception Exception { get; } = exception;
}
