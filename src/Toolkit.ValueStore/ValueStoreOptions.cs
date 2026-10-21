namespace Toolkit.ValueStore;

/// <summary>Configures the value store.</summary>
public sealed class ValueStoreOptions
{
	/// <summary>Gets or sets the directory where value files are stored.</summary>
	public string DirectoryPath { get; set; } = string.Empty;

	/// <summary>Gets or sets a factory that produces a file name for a value type.</summary>
	public Func<Type, string>? FileNameFactory { get; set; }

	/// <summary>Gets or sets a value indicating whether external file changes are watched.</summary>
	public bool WatchForExternalChanges { get; set; }

	/// <summary>Gets or sets the interval used to poll the file for external changes.</summary>
	public TimeSpan PollingInterval { get; set; } = TimeSpan.FromSeconds(1);
}
