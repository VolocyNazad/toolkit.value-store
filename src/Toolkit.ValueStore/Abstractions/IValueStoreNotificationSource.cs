namespace Toolkit.ValueStore.Abstractions;

/// <summary>Notifies subscribers when a value store file fails to load.</summary>
public interface IValueStoreNotificationSource
{
	/// <summary>Subscribes to load failures.</summary>
	/// <param name="listener">The callback invoked for the current and subsequent failures.</param>
	/// <returns>A subscription that stops notifications when disposed.</returns>
	IDisposable OnLoadFailed(Action<ValueStoreLoadFailedEventArgs> listener);
}
