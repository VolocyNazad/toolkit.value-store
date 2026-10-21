using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Toolkit.ValueStore;

internal static class Guard
{
	public static void NotNull(
		[NotNull] object? argument,
		[CallerArgumentExpression(nameof(argument))] string? paramName = null)
	{
#if NET8_0_OR_GREATER
		ArgumentNullException.ThrowIfNull(argument, paramName);
#else
		if (argument is null) throw new ArgumentNullException(paramName);
#endif
	}

	public static void ThrowIfDisposed(bool disposed, object instance)
	{
#if NET8_0_OR_GREATER
		ObjectDisposedException.ThrowIf(disposed, instance);
#else
		if (disposed) throw new ObjectDisposedException(instance.GetType().FullName);
#endif
	}
}
