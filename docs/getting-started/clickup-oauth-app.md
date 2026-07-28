# Create a ClickUp OAuth app

This package authenticates against ClickUp using OAuth 2.0, so it needs a ClickUp OAuth app.
One app serves every Umbraco environment and every Workspace you connect.

## Create the app in ClickUp

1. In ClickUp, open **Settings → Apps**.
2. Create a new app and give it a name your team will recognise (for example
   `Umbraco Automate`).
3. Register the redirect URL for your Umbraco site — see [Redirect URL](#redirect-url) below.
4. Copy the **Client ID** and **Client Secret**.

You need to be a ClickUp Workspace owner or admin to create an app.

## Redirect URL

Umbraco Automate handles the OAuth callback on one fixed path:

```
https://<your-umbraco-host>/umbraco/automate/oauth/callback
```

Replace `<your-umbraco-host>` with the host of the Umbraco site you're connecting from,
including the port if it runs on a non-default one. For example:

| Environment | Redirect URL |
|---|---|
| Local development | `https://localhost:44369/umbraco/automate/oauth/callback` |
| Staging | `https://staging.example.com/umbraco/automate/oauth/callback` |
| Production | `https://www.example.com/umbraco/automate/oauth/callback` |

The path carries no provider segment, so it's the same URL for every Automate OAuth
provider — a site already connecting another provider uses this one unchanged.

Register one URL per environment on the ClickUp app. ClickUp rejects the authorization
request if the redirect URL doesn't match a registered value character for character,
including the scheme, port, and any trailing slash.

> [!NOTE]
> This path comes from Umbraco Automate's OpenIddict integration, not from this package, so
> a future Automate version could change it. The authoritative value for your site is the
> `redirect_uri` query parameter on the URL ClickUp is called with when you start the
> connection flow from the backoffice.

## Add the credentials to appsettings.json

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

Leave `Scopes` empty — see [OAuth scopes](../configuration/scopes.md) for why.

Keep `ClientSecret` out of source control. Use
[user secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets) locally and
environment variables or a key vault in hosted environments. See the
[appsettings.json reference](../configuration/appsettings.md) for the full section.

## Next

[Add a ClickUp connection](add-a-connection.md) in the backoffice.
