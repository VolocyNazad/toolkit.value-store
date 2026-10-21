# Changelog

All notable changes to this project are documented in this file.

The format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [Unreleased]

### Changed

- Centralize package versions in `Directory.Packages.props` and move test-only versions into `tests/Directory.Packages.props`.
- Add XML documentation to the public API.

## [1.0.1] - 2026-09-14

### Added

- Validate required repository files, navigation links, and Solution Items in CI.

### Changed

- Update Microsoft.Extensions packages to 10.0.11, MinVer to 8.0.0, and the test toolchain.
- Clean package output and run tests before validating and publishing the NuGet package.
- Standardize GitHub Actions workflow filenames and display names by responsibility.
- Clarify package version management and remove redundant central package configuration.
- Configure xUnit v3 test execution through Microsoft.Testing.Platform and fail test runs when no tests are discovered.
- Establish a shared EditorConfig baseline and use the repository-policy validator as the single structural CI check.
- Complete solution items for repository documents, configuration, workflows and maintenance scripts; document the shared layout.
- Standardize local and CI SDK selection on stable .NET 10.0 through global.json, restrict roll-forward to that major/minor line, and configure setup-dotnet to read the file.
- Show documentation in Visual Studio Solution Explorer under a `docs` solution folder with matching subfolders.
- Move development policies and repository guidance from `AGENTS.md` to `docs/policies/development.md` and `docs/repository.md`; keep required reading links in `AGENTS.md` and add README navigation.
