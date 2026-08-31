# Toolkit.ValueStore

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![VolocyNazad](https://img.shields.io/badge/VolocyNazad-blue.svg)](https://github.com/VolocyNazad)

> Файловый DI-стор настроек с YAML-сериализацией и уведомлениями об изменениях.

Toolkit.ValueStore — сервис хранения типизированных настроек в файле (по умолчанию YAML) с атомарной записью, опциональным отслеживанием внешних изменений файла и оповещением подписчиков. **Не зависит от Revit API** — несмотря на происхождение (выделен из `revit.linter`), это обычная .NET-библиотека, пригодная для любого приложения.

## Возможности

- `IValueStore<T>` — `CurrentValue`, `Update(Action<T> change)`, `OnChange(Action<T> listener)`.
- Персистентность в файл (сериализатор по умолчанию — `YamlValueStoreSerializer`, заменяем через `IValueStoreSerializer`).
- Атомарная запись (временный файл + `File.Replace`) и устойчивость к временно заблокированному файлу (retry с паузой).
- Опциональное отслеживание внешних изменений файла (`FileSystemWatcher` + поллинг) с уведомлением подписчиков через `OnChange`.
- `IValueStoreNotificationSource.OnLoadFailed` — уведомления об ошибках загрузки/десериализации, с replay последней ошибки новым подписчикам.
- Имя файла настраивается через `[StoreFile("name.yml")]` на классе настроек либо через `ValueStoreOptions.FileNameFactory`.
- Регистрация в DI одной строкой: `AddValueStore(...)` / `AddValueStore<TSerializer>(...)`.

## Установка

```
dotnet add package VolocyNazad.ValueStore
```

## Использование

Регистрация в DI:

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

Настройки и использование:

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

Обработка ошибок загрузки файла (например, битый YAML):

```csharp
using Toolkit.ValueStore.Abstractions;

public sealed class SettingsErrorNotifier(IValueStoreNotificationSource source) : IDisposable
{
    private readonly IDisposable _subscription = source.OnLoadFailed(args =>
        Console.WriteLine($"Failed to load {args.FilePath}: {args.Exception.Message}"));

    public void Dispose() => _subscription.Dispose();
}
```

## Требования

- .NET SDK 10.0.103+ (см. `global.json`)
- `net48` или `net8.0-windows`

## Лицензия

MIT, см. [LICENSE.md](LICENSE.md).
