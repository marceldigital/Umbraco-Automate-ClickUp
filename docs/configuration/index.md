# Configuration

This package reads a single configuration section, `Umbraco:Automate:Providers:ClickUp`, which
holds the credentials for your [ClickUp OAuth app](../getting-started/clickup-oauth-app.md).

- [**appsettings.json reference**](appsettings.md) — every key in the section, and where to keep the secret.
- [**OAuth scopes**](scopes.md) — why `Scopes` should stay empty.

Per-Workspace settings are not configured in `appsettings.json` — those live on the
[ClickUp connection](../getting-started/add-a-connection.md) in the backoffice. The
configuration section is app-level only: one OAuth app, many connections.
