# appsettings.json reference

```json
{
  "Umbraco": {
    "Automate": {
      "Providers": {
        "ClickUp": {
          "ClientId": "...",
          "ClientSecret": "...",
          "Scopes": []
        }
      }
    }
  }
}
```

## Keys

| Key | Type | Required | Description |
|---|---|---|---|
| `ClientId` | string | Yes | The Client ID of your ClickUp OAuth app. |
| `ClientSecret` | string | Yes | The Client Secret of your ClickUp OAuth app. Keep this out of source control. |
| `Scopes` | string array | No | Leave empty. See [OAuth scopes](scopes.md). |

These are the standard OAuth provider fields Umbraco Automate defines for every OAuth provider —
ClickUp adds no provider-specific keys.

## IntelliSense

The package ships a JSON schema (`appsettings-schema.Umbraco.Automate.ClickUp.json`) and wires
it up through a `buildTransitive` props file, so a consuming site gets completion and validation
for this section in Visual Studio, Rider, and VS Code automatically. No manual `$schema`
reference is needed.

## Keeping the secret out of source control

`ClientSecret` is a credential. Put a placeholder — or nothing at all — in `appsettings.json`,
and supply the real value per environment:

**Locally**, use [user secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets):

```bash
dotnet user-secrets set "Umbraco:Automate:Providers:ClickUp:ClientSecret" "your-secret"
```

**In hosted environments**, use an environment variable. Configuration keys map onto
environment variables with `__` replacing each `:`

```text
Umbraco__Automate__Providers__ClickUp__ClientSecret
```

Azure App Service application settings, container environment variables, and Key Vault
references all resolve through this name.

## Applying a change

The section is read when the OAuth provider is registered at startup, so changing `ClientId` or
`ClientSecret` requires a site restart. Existing connections keep working across a restart —
they hold their own tokens and are not re-validated until used or re-saved. Rotating the OAuth
app's secret in ClickUp, however, invalidates existing tokens, so connections will need
re-authorizing.
