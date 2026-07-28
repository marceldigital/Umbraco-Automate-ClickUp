# Security Policy

## Supported versions

This package is pre-1.0. Only the most recent release on
[NuGet](https://www.nuget.org/packages/Umbraco.Community.Automate.ClickUp) is supported —
fixes ship as a new minor or patch rather than being backported to earlier versions.

## Reporting a vulnerability

**Please don't open a public issue for a security problem.**

Use GitHub's private vulnerability reporting instead: go to the
[Security tab](https://github.com/marceldigital/Umbraco-Automate-ClickUp/security) and choose
**Report a vulnerability**. That opens a private advisory visible only to you and the
maintainers.

Helpful things to include:

- Which version of the package, Umbraco, and Umbraco Automate you're on.
- What an attacker can do — read another Workspace's data, act as another user, recover an
  OAuth token, and so on.
- Steps to reproduce, and a workflow or action configuration if the issue is specific to one.

We'll acknowledge your report and let you know whether we can reproduce it. If it's
confirmed, we'll agree a disclosure timeline with you and credit you in the advisory and
changelog unless you'd rather stay anonymous.

## Scope

This policy covers the `Umbraco.Community.Automate.ClickUp` package — the ClickUp connection
type, its OAuth credential handling, and its actions.

It does not cover the dependencies this package sits on top of. Report those upstream, since
we can't fix them here:

| Where the problem is | Report to |
|---|---|
| Umbraco CMS or Umbraco Automate | [Umbraco's security policy](https://umbraco.com/about-us/trust-center/security/) |
| The ClickUp API or ClickUp itself | [ClickUp](https://clickup.com/security) |
| OpenIddict | [OpenIddict](https://github.com/openiddict/openiddict-core/security/policy) |

One thing that is **not** a vulnerability, because it's a documented ClickUp platform
limitation rather than a flaw in this package: ClickUp has no app-level identity for OAuth
apps, so every action is attributed to the account that authorised the connection. See
[Connection identity](https://github.com/marceldigital/Umbraco-Automate-ClickUp/blob/main/docs/troubleshooting/connection-identity.md).
