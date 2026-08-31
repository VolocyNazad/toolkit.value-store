# AGENTS.md

## Policy

The stack documented below is the default and takes priority over
whatever an agent might otherwise reach for. Prefer what's already in use
over introducing an alternative. If a deviation seems necessary, say so
explicitly to the user and get confirmation before adding it.

If a change affects the folder structure or the tech stack (a new/removed project, a new dependency, a version bump worth recording, a new convention), update this file accordingly as part of the same change - don't leave it to a later pass.

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
- No central package management (`Directory.Packages.props` is empty) -
  package versions are set per-`<PackageReference>`

## History

Extracted from `revit.linter` (namespace was `Revit.Linter.ValueStore`)
on 2026-08-31. A byte-for-byte duplicate also existed as an internal,
unpublished `Toolkit.ValueStore` inside the `impact.revit` monorepo
(added 2026-08-24) - both call sites were migrated to depend on this
package instead of keeping their own copy.
