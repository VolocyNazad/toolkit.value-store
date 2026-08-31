namespace Toolkit.ValueStore.Abstractions;

public interface IValueStoreNotificationSource
{
	IDisposable OnLoadFailed(Action<ValueStoreLoadFailedEventArgs> listener);
}
