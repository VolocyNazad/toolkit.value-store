# Toolkit.ValueStore

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![VolocyNazad](https://img.shields.io/badge/VolocyNazad-blue.svg)](https://github.com/VolocyNazad)

> A file-backed DI settings store with YAML serialization and change notifications.

Toolkit.ValueStore — a service for storing typed settings in a file (YAML by default), with atomic writes, optional tracking of external file changes, and subscriber notifications. **Does not depend on the Revit API** — despite its origin (extracted from `revit.linter`), it's a regular .NET library suitable for any application.

## Features

- `IValueStore<T>` — `CurrentValue`, `Update(Action<T> change)`, `OnChange(Action<T> listener)`.
- Persistence to a file (the default serializer is `YamlValueStoreSerializer`, swappable via `IValueStoreSerializer`).
- Atomic writes (temp file + `File.Replace`) and resilience to a temporarily locked file (retry with a pause).
- Optional tracking of external file changes (`FileSystemWatcher` + polling) with subscriber notification via `OnChange`.
- `IValueStoreNotificationSource.OnLoadFailed` — notifications about load/deserialization errors, replaying the last error to new subscribers.
- The file name is configurable via `[StoreFile("name.yml")]` on the settings class, or via `ValueStoreOptions.FileNameFactory`.
- One-line DI registration: `AddValueStore(...)` / `AddValueStore<TSerializer>(...)`.

## Installation

```
dotnet add package VolocyNazad.ValueStore
```

## Usage

DI registration:

```csharp
using Microsoft.Extensions.DependencyInjection;
using Toolkit.ValueStore.DI;
using Toolkit.ValueStore.Serialization;

services.AddValueStore<YamlValueStoreSerializer>(options =>
{
    options.DirectoryPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "MyApp");
    options.WatchForExternalChanges = true;
});
```

Settings and usage:

```csharp
using Toolkit.ValueStore.Abstractions;

[StoreFile("my-settings.yml")]
public sealed class MySettings
{
    public bool IsEnabled { get; set; }
}

public sealed class MyService(IValueStore<MySettings> store)
{
    public void Toggle() => store.Update(s => s.IsEnabled = !s.IsEnabled);
}
```

Handling file load errors (e.g. malformed YAML):

```csharp
using Toolkit.ValueStore.Abstractions;

public sealed class SettingsErrorNotifier(IValueStoreNotificationSource source) : IDisposable
{
    private readonly IDisposable _subscription = source.OnLoadFailed(args =>
        Console.WriteLine($"Failed to load {args.FilePath}: {args.Exception.Message}"));

    public void Dispose() => _subscription.Dispose();
}
```

## Requirements

- .NET SDK 10.0.103+ (see `global.json`)
- `net48` or `net8.0-windows`

## License

MIT, see [LICENSE.md](LICENSE.md).

## Development documentation

- [Development policy](docs/policies/development.md)
- [Repository guide and technology stack](docs/repository.md)

## Contributing

Read [CONTRIBUTING.md](CONTRIBUTING.md) before submitting changes.
