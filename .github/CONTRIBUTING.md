# Contributing Guidelines

Contributions to this package are most welcome! 

There is a test site in the solution to make working with this repository easier.
It is configured to do an unattended install, check `appSettings.json` for the login details.

## Commit Messages

This repository uses [Release Please](https://github.com/googleapis/release-please) to automate versioning and changelogs, which relies on [Conventional Commits](https://www.conventionalcommits.org/). Please format commit messages (or at minimum, PR titles, since squash-merges use the PR title as the commit message) as:

```
<type>[optional scope]: <description>

[optional body]

[optional footer(s)]
```

Common types:

- `feat:` — a new feature (bumps the minor version)
- `fix:` — a bug fix (bumps the patch version)
- `feat!:` / `fix!:` or a footer of `BREAKING CHANGE: <description>` — a breaking change (bumps the major version)
- `docs:`, `chore:`, `refactor:`, `test:`, `ci:` — no version bump, but still shows up in the changelog under its own section

Examples:

```
feat: add support for syncing ClickUp task comments
fix: correct OAuth token refresh when scope is empty
feat!: rename `ClickUpProvider` options to match Automate's naming convention

BREAKING CHANGE: consumers must update their appsettings ClickUp section key from `ClickUp` to `ClickUpProvider`.
```

Commits that don't follow this format are not required for docs-only or trivial changes, but anything intended to land in the changelog or affect the version number needs to.