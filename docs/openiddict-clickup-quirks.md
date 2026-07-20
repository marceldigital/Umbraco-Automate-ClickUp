# OpenIddict / ClickUp integration quirks

Notes on non-obvious configuration required to get ClickUp's OAuth2 API working
through OpenIddict's generic client, found while building and live-testing the
connection type. Kept here instead of as inline comments so the reasoning isn't
lost, without cluttering the registration code itself.

ClickUp has no discovery document (no `.well-known/openid-configuration`), so
OpenIddict has nothing to infer server capabilities from — every one of the
following had to be declared explicitly on the hand-built registration in
`Configuration/ClickUpWebIntegrationBuilderExtensions.cs`.

## `GrantTypesSupported` / `ResponseTypesSupported` / `ResponseModesSupported`

Without these set on the registration's `OpenIddictConfiguration`, the first
attempt to connect fails at the authorization redirect with:

> `InvalidOperationException: No supported response type could be found in the
> server configuration...`

OpenIddict's `AttachGrantTypeAndResponseType` handler negotiates the OAuth flow
by intersecting what the client registration declares (`GrantTypes`/`ResponseTypes`)
against what the server configuration declares supported. An empty
`GrantTypesSupported` is leniently treated as "assume supported" — but there's no
equivalent fallback for `ResponseTypesSupported`, so an empty list can never
match anything. `ResponseModesSupported` isn't strictly required for the
authorization-code + query flow (a global default fills in), but is set
explicitly for the same reason rather than relying on that default.

## `ClientAuthenticationMethods` / `TokenEndpointAuthMethodsSupported`

Without these, the token exchange reaches ClickUp but is rejected with:

> `{"err":"Client secret does not match","ECODE":"OAUTH_011"}`

When a server's supported-methods list is empty, OpenIddict's negotiation falls
back to `client_secret_basic` ("as it MUST be implemented by all OAuth 2.0
servers"). ClickUp's token endpoint only reads `client_id`/`client_secret` as
POST body parameters — it doesn't parse the `Authorization: Basic` header — so
the secret silently never arrives. Forcing `client_secret_post` on both the
registration and the configuration's supported-methods list fixes this.

## `ClientType`

Without this, the token exchange still reaches ClickUp with a `client_id` but no
`client_secret` at all — same downstream error as above, even with the two
fixes above in place. This one is subtler: it's a race between two separate
`IPostConfigureOptions<OpenIddictClientOptions>` steps that both run once, in
registration order, whenever the options are first resolved:

- OpenIddict's own `OpenIddictClientConfiguration` infers `registration.ClientType`
  — but only if it isn't already set — as `Confidential` if `ClientSecret` is
  non-empty, else `Public`.
- Umbraco.Automate's `OpenIddictClientCredentialsConfigurator` is the thing that
  patches the *real* `ClientSecret` in from `Umbraco:Automate:Providers:ClickUp`
  config. Our own registration always starts with `ClientSecret = null`.

If OpenIddict's inference runs before Umbraco's patching, it permanently infers
`ClientType = Public` from the still-empty secret. `AttachTokenEndpointClientAuthenticationMethod`
short-circuits to `none` for public clients — before it ever looks at
`ClientAuthenticationMethods`/`TokenEndpointAuthMethodsSupported` — so the secret
is dropped from every request regardless of the fixes above.

Setting `ClientType = Confidential` explicitly at registration-creation time
skips OpenIddict's inference entirely, so the outcome no longer depends on
which post-configure step happens to run first.

## `CopyLocalLockFileAssemblies` on `Automate.ClickUp.csproj`

Without this, `dotnet build` fails during `GenerateAppsettingsSchema` with:

> `Could not load file or assembly 'Umbraco.Automate.OpenIddict.Core'...`

The `JsonSchemaGenerate` MSBuild task (from `Umbraco.JsonSchema.Extensions`)
reflects over the freshly-built `$(TargetPath)` to read `UmbracoAutomateClickUpSchema`,
loading it into a plugin `AssemblyLoadContext` that resolves dependencies via an
`AssemblyDependencyResolver` scoped to files sitting next to that dll. Class
library builds don't otherwise copy transitive NuGet dependencies into their own
output folder (only apps do) — `CopyLocalLockFileAssemblies` forces that copy so
the resolver can find them during our own build.
