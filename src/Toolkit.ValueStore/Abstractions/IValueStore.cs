namespace Toolkit.ValueStore.Abstractions;

/// <summary>Represents a file-backed store for a single value.</summary>
/// <typeparam name="T">The stored value type.</typeparam>
public interface IValueStore<out T> where T : class
{
	/// <summary>Gets a snapshot of the current value.</summary>
	T CurrentValue { get; }

	/// <summary>Subscribes to value changes.</summary>
	/// <param name="listener">The callback invoked with a snapshot of the new value.</param>
	/// <returns>A subscription that stops notifications when disposed.</returns>
	IDisposable OnChange(Action<T> listener);

	/// <summary>Updates the value and persists it immediately.</summary>
	/// <param name="change">The callback that mutates a snapshot of the value.</param>
	void Update(Action<T> change);
}
