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
├── Directory.Packages.props           central package versions
├── src/
│   └── Toolkit.ValueStore/            the library
└── tests/
    ├── Directory.Packages.props       test-only package versions
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
- Package versions use central package management (`Directory.Packages.props`, `ManagePackageVersionsCentrally=true`); test-only versions live in `tests/Directory.Packages.props`, which imports the root file.

## Documentation layout

- `AGENTS.md` links to the required repository guidance and routes a change to the document that owns it.
- `docs/policies/` contains the repository policies — the rules a change must follow. The
  [policy index](policies/README.md) says what each policy covers.
- `docs/repository.md` describes the project, repository structure, and technology stack.
- `changelog.d/` holds the pending changelog fragments: a consumer-visible change adds one, and the
  release preparation composes them into `CHANGELOG.md`; the rules are in the
  [changelog policy](policies/changelog.md). Git does not track empty directories, so the folder
  exists only while a fragment is pending.
- `scripts/` holds the repository validator, the changelog tool and the release verification.

The root solution exposes the documentation files under a `docs` solution folder in Visual Studio, preserving their subfolder structure. When adding documentation files, also add them as solution items; solution folders do not automatically include new files.

The root `global.json` selects stable .NET SDK 10.0 (minimum `10.0.103`, `rollForward: latestFeature`). CI and publishing install the SDK from this file. Additional SDK installations may provide older test runtimes. See the [SDK selection policy](policies/build.md#sdk-selection).

## Solution items

The root solution exposes repository-level documents and configuration under `solutionItems`, GitHub files and maintenance scripts in matching subfolders, and documentation under `docs/`. The list is explicit, not a filesystem glob; keep links up to date when files change. See the [solution items policy](policies/repository-maintenance.md#solution-items).
## Repository validation

`scripts/Validate-Repository.ps1` enforces the required repository documents,
their navigation links, the complete and valid Solution Items, and the workflow action pins. The
`.github/workflows/repository-policy.yml` workflow runs it, the pending fragment check
(`Update-Changelog.ps1 -Verify`) and the release structure check (`Verify-Release.ps1`) for pushes
and pull requests. See the [repository maintenance policy](policies/repository-maintenance.md#repository-validation).

## Formatting

The root `.editorconfig` defines the portable formatting baseline. Existing repositories may add stricter C# or analyzer-specific settings. See the [repository maintenance policy](policies/repository-maintenance.md#formatting-baseline).

## Continuous integration

`build-and-test` builds the solution in `Release` with warnings as errors and documentation
warnings promoted to errors, runs the headless tests and uploads the package. `repository-policy`
runs the validator, the pending fragment check and the release structure check. `publish-nuget` is
manual and refuses a commit without a single stable tag and a matching changelog section.

## Versioning and release tags

The library uses MinVer 8 with stable tags in the `MAJOR.MINOR.PATCH` format and no `v` prefix. The tag is also the NuGet package version; it is not coupled to a Revit year. `scripts/Verify-Release.ps1 -FromHeadTag` accepts exactly one matching tag at `HEAD`, and the publish workflow verifies the package ID and version before pushing to NuGet.

The tagged commit must already contain the release section in `CHANGELOG.md` with an empty `Unreleased` section above it and no pending fragment; the publish workflow stops otherwise. See the [release policy](policies/releases.md) and the [changelog policy](policies/changelog.md#release).
