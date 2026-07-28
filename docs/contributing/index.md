# Contributing

Contributions to this package are most welcome. Start with the
[Contributing Guidelines](https://github.com/marceldigital/Umbraco-Automate-ClickUp/blob/main/.github/CONTRIBUTING.md)
in the repository, which cover commit message conventions and the release process.

## Repository layout

| Path | Purpose |
|---|---|
| `src/Automate.ClickUp` | The shipped package, `Umbraco.Community.Automate.ClickUp`. |
| `src/Automate.ClickUp.TestSite` | A local Umbraco site for manual testing, seeded via uSync. |
| `tests/Automate.ClickUp.Tests` | Unit tests, using `Umbraco.Automate.Testing`. |
| `docs` | This documentation site. |

## Building

```bash
dotnet restore Automate.ClickUp.slnx --locked-mode
dotnet build Automate.ClickUp.slnx -c Release --no-restore
dotnet test Automate.ClickUp.slnx -c Release --no-build --no-restore
```

Package versions are managed centrally in `Directory.Packages.props`, and the target framework
in `Directory.Build.props`. Lock files are committed, so add packages through the central file
rather than per-project `PackageReference` versions.

The package version is not stored in any `.csproj` — it lives in
`.release-please-manifest.json` and is injected at pack time.

## Working on the documentation

The site is built with [DocFX](https://dotnet.github.io/docfx/) from the `docs` folder. DocFX is
pinned as a local dotnet tool, so no separate install is needed:

```bash
dotnet tool restore
dotnet docfx docs/docfx.json --serve
```

Then open <http://localhost:8080>.

A pull request that touches `docs/` runs the docs build with `--warningsAsErrors`, so a broken
relative link, a missing table-of-contents target, or a bad anchor fails CI rather than shipping.
Two consequences for authoring:

- **Links between pages inside `docs/` must be relative `.md` paths** — they work on GitHub and
  DocFX rewrites them to `.html`.
- **Links to anything outside `docs/`** — `LICENSE`, `CONTRIBUTING.md`, source files — **must be
  absolute GitHub URLs**, because those files aren't part of the site's content set.

### Documenting a new action

Action docs follow a fixed shape so the section scales without restructuring. When you add an
action to the package:

1. Add `docs/actions/<alias>.md`, using [create-task.md](../actions/create-task.md) as the
   template. Keep its heading structure: **Requirements**, **Inputs**, **Outputs**,
   **Design notes**, **Errors**, **Planned enhancements**.
2. Add one entry under the `Actions` group in `docs/actions/toc.yml`.
3. Add one row to the table in [All actions](../actions/index.md).

Link out to [Binding expressions](../actions/binding-expressions.md) and
[Error handling](../actions/error-handling.md) rather than restating them — those pages exist
so each action doc doesn't have to repeat the same explanations. Likewise, link the connection
requirement to [Add a ClickUp connection](../getting-started/add-a-connection.md) instead of
re-describing it.

Document inputs and outputs as tables with the field labels exactly as they appear in the
backoffice, so someone can match the doc against the UI field by field.

## Internals

- [OpenIddict / ClickUp quirks](openiddict-clickup-quirks.md) — why the OpenIddict registration
  and a few build settings look the way they do.
