<!--
This repo squash-merges PRs, so the PR TITLE becomes the commit message on
main — and release-please parses that commit to decide the version bump and
changelog entry. It must follow Conventional Commits:

    <type>[optional scope]: <description>

Types: feat, fix, perf, revert, docs, chore, refactor, test, ci, build, style
Breaking change: use `feat!:` / `fix!:`, or add a `BREAKING CHANGE:` footer
in this description. See .github/CONTRIBUTING.md for full details.

pr-title.yml checks this automatically — if it fails, edit the PR title
(not just this description) and it will re-run.
-->

## Summary

<!-- What changed, and why. Link a related issue if there is one. -->

Closes #

## Type of change

- [ ] `feat` — new functionality (minor version bump)
- [ ] `fix` — bug fix (patch version bump)
- [ ] breaking change — `feat!`/`fix!` or a `BREAKING CHANGE:` footer above (major version bump)
- [ ] `docs` / `chore` / `refactor` / `test` / `ci` / `build` / `style` — no version bump

## How was this verified?

<!-- e.g. ran the OAuth flow against Automate.ClickUp.TestSite, added/ran tests, etc. -->

## Checklist

- [ ] PR title follows Conventional Commits (see comment above — `pr-title.yml` will check this)
- [ ] Self-reviewed the diff
- [ ] Updated `docs/README_nuget.md` or other docs if user-facing behavior changed
- [ ] No secrets, credentials, or API keys included in the diff
