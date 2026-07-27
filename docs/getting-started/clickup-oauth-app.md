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

> [!IMPORTANT]
> The exact redirect URL is supplied by Umbraco Automate's OpenIddict integration rather than
> by this package, and is not yet documented here. Capture it from your own site before
> registering the app: start the connection flow from the backoffice and read the
> `redirect_uri` query parameter off the URL ClickUp is called with, or check the OpenIddict
> client registration in your site's startup logs.

Once you have it, register that exact URL in the ClickUp app. ClickUp rejects the
authorization request if the redirect URL doesn't match a registered value character for
character, including the scheme, port, and any trailing slash. Each environment
(local, staging, production) needs its own URL registered on the app.

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
