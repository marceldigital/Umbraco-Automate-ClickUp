# Install the package

## Requirements

| Requirement | Version |
|---|---|
| Umbraco CMS | 18.0 or later |
| Umbraco Automate | 18.0 or later |
| .NET | 10.0 |

Umbraco Automate must already be installed and licensed on the site. This package is a
*provider* for Automate — it adds a ClickUp connection type and ClickUp actions, but it does
not include Automate itself.

## Install

```bash
dotnet add package Umbraco.Community.Automate.ClickUp
```

No `Startup.cs` or composer changes are needed. The package registers ClickUp as an OpenIddict
OAuth provider automatically through an `IComposer`, so the connection type and actions are
available as soon as the site restarts.

## Next

The package is installed but not yet usable — it needs OAuth app credentials before a
connection can be made. Continue to [Create a ClickUp OAuth app](clickup-oauth-app.md).
