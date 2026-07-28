# OAuth scopes

Leave `Scopes` as an empty array:

```json
"Scopes": []
```

## Why

`Scopes` exists because it's part of the shared OAuth provider configuration that Umbraco
Automate defines for every OAuth provider. ClickUp doesn't need it.

ClickUp's OAuth implementation grants an app access to the Workspaces the authorizing user
selects during the authorization flow, rather than to a set of named permission scopes requested
by the app. There is no scope string to negotiate, and unlike Slack — which splits user scopes
from bot scopes and needs both configured — ClickUp has no non-standard scope arrangement to
express here.

## Controlling what the integration can reach

Because access follows the authorizing account rather than a scope list, permissions are
controlled on the ClickUp side instead:

- **Which Workspaces** — chosen during authorization. Only the Workspaces approved at that point
  are reachable.
- **Which Spaces, Folders, and Lists** — whatever the authorizing ClickUp account can see. The
  integration cannot exceed that account's own permissions.

This is the practical argument for connecting with a purpose-made automation account: its
ClickUp permissions become the integration's permissions, so you can scope access by scoping
that account. See
[Tasks are attributed to a person](../troubleshooting/connection-identity.md).
