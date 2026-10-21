using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Toolkit.ValueStore.Abstractions;
using Toolkit.ValueStore.Serialization;
using Toolkit.ValueStore.Services;

namespace Toolkit.ValueStore.DI;

/// <summary>Registers value store services.</summary>
[SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "C# 14 extension blocks are compiled into a nested type.")]
public static class ServiceCollectionExtensions
{
	extension(IServiceCollection services)
	{
		/// <summary>Registers the value store with the default YAML serializer.</summary>
		/// <param name="configure">The callback that configures the store options.</param>
		/// <returns>The same service collection.</returns>
		public IServiceCollection AddValueStore(Action<ValueStoreOptions> configure)
		{
			Guard.NotNull(configure);
			services.AddOptions<ValueStoreOptions>().Configure(configure);
			services.TryAddSingleton<ValueStoreNotificationHub>();
			services.TryAddSingleton<IValueStoreNotificationSource>(provider =>
				provider.GetRequiredService<ValueStoreNotificationHub>());
			services.TryAddSingleton<IValueStoreSerializer, YamlValueStoreSerializer>();
			services.TryAddSingleton(typeof(IValueStore<>), typeof(FileValueStore<>));
			return services;
		}

		/// <summary>Registers the value store with a custom serializer.</summary>
		/// <typeparam name="TSerializer">The serializer type.</typeparam>
		/// <param name="configure">The callback that configures the store options.</param>
		/// <returns>The same service collection.</returns>
		public IServiceCollection AddValueStore<TSerializer>(Action<ValueStoreOptions> configure)
			where TSerializer : class, IValueStoreSerializer
		{
			Guard.NotNull(configure);
			services.AddOptions<ValueStoreOptions>().Configure(configure);
			services.TryAddSingleton<ValueStoreNotificationHub>();
			services.TryAddSingleton<IValueStoreNotificationSource>(provider =>
				provider.GetRequiredService<ValueStoreNotificationHub>());
			services.Replace(ServiceDescriptor.Singleton<IValueStoreSerializer, TSerializer>());
			services.TryAddSingleton(typeof(IValueStore<>), typeof(FileValueStore<>));
			return services;
		}
	}
}
