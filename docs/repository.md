# Repository guide

Paths in this document are relative to the repository root.
Read and follow the [development policy](policies/development.md) alongside this guide.

## About

Source for `VolocyNazad.ValueStore`: a small, file-backed, DI-friendly
value store with YAML serialization, change notifications and external
file-change watching (`FileSystemWatcher` + polling). Despite living
alongside the `toolkit.revit.*` family (consumed by `revit.linter` and
`impact.revit`), it has **no dependency on the Revit API** - it's a plain
.NET library usable in any app that wants a simple settings/config file
with hot-reload.

## Repository structure

```
.
├── src/
│   └── Toolkit.ValueStore/            the library
└── tests/
    └── Toolkit.ValueStore.Tests/
```

## Tech stack

- Plain .NET library, `Microsoft.NET.Sdk`, targeting `net48;net8.0-windows`
  (matches what `revit.linter`/`impact.revit` build against) - **not**
  `VolocyNazad.Revit.Sdk`, no Revit-version build matrix, package version
  is not coupled to a Revit major version
- YamlDotNet (default serializer), Microsoft.Extensions.DependencyInjection
  / Options / Logging.Abstractions
- MinVer (plain git-tag-based semantic versioning), PolySharp (polyfills
  for net48)
- Tests: xunit.v3 + coverlet.collector + Microsoft.Extensions.DependencyInjection
- Package versions are set directly on each `PackageReference`; this repository does not use central package management.

## Documentation layout

- `AGENTS.md` links to the required repository guidance.
- `docs/policies/development.md` contains the development policy.
- `docs/repository.md` describes the project, repository structure, and technology stack.

The root solution exposes the documentation files under a `docs` solution folder in Visual Studio, preserving their subfolder structure. When adding documentation files, also add them as solution items; solution folders do not automatically include new files.

The root `global.json` selects stable .NET SDK 10.0 (minimum `10.0.103`, `rollForward: latestFeature`). CI and publishing install the SDK from this file. Additional SDK installations may provide older test runtimes. See the [SDK selection policy](policies/development.md#net-sdk-selection).

## Solution items

The root solution exposes repository-level documents and configuration under `solutionItems`, GitHub files and maintenance scripts in matching subfolders, and documentation under `docs/`. The list is explicit, not a filesystem glob; keep links up to date when files change. See the [solution items policy](policies/development.md#solution-items).
## Repository validation

`scripts/Validate-Repository.ps1` enforces the required repository documents,
their navigation links, and complete, valid Solution Items. The
`.github/workflows/repository-policy.yml` workflow runs it for pushes and pull
requests. See the [development policy](policies/development.md#repository-validation).

## Formatting

The root `.editorconfig` defines the portable formatting baseline. Existing repositories may add stricter C# or analyzer-specific settings. See the [development policy](policies/development.md#formatting-baseline).

## Versioning and release tags

The library uses MinVer 8 with stable tags in the `MAJOR.MINOR.PATCH` format and no `v` prefix. The tag is also the NuGet package version; it is not coupled to a Revit year. Manual publishing accepts exactly one matching tag at HEAD and verifies the package ID and version before pushing to NuGet.
