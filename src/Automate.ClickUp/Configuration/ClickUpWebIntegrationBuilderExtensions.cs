using OpenIddict.Abstractions;
using OpenIddict.Client;
using Umbraco.Community.Automate.ClickUp.Configuration;

// Declared in the same namespace as OpenIddict's generated provider extensions
// (Microsoft.Extensions.DependencyInjection) purely for call-site discoverability
// alongside .AddSlack(), .AddGitHub(), etc. — not because this hooks into any
// OpenIddict extension point.
namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Registers ClickUp as an OAuth2 provider on <see cref="OpenIddictClientWebIntegrationBuilder"/>.
/// </summary>
/// <remarks>
/// <para>
/// ClickUp is not one of OpenIddict's built-in WebIntegration providers, so this
/// method is hand-written rather than generated, registering a generic client
/// registration with ClickUp's known endpoints directly.
/// </para>
/// <para>
/// <b>Fragility note:</b> this relies on <see cref="OpenIddictClientWebIntegrationBuilder"/>
/// being <c>public</c> with an accessible <c>Services</c> property — there is no
/// documented, officially supported extension point for third-party providers.
/// If a future OpenIddict release changes that type's accessibility, this method
/// breaks and needs to be re-pointed at whatever mechanism replaces it.
/// </para>
/// </remarks>
public static class ClickUpWebIntegrationBuilderExtensions
{
    public static OpenIddictClientWebIntegrationBuilder AddClickUp(
        this OpenIddictClientWebIntegrationBuilder builder,
        Action<ClickUpOptions>? configuration = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        var options = new ClickUpOptions();
        configuration?.Invoke(options);

        builder.Services.Configure<OpenIddictClientOptions>(clientOptions =>
        {
            var registration = new OpenIddictClientRegistration
            {
                Issuer = new Uri("https://api.clickup.com/", UriKind.Absolute),
                ProviderName = "ClickUp",
                ProviderDisplayName = "ClickUp",
                ClientType = OpenIddictConstants.ClientTypes.Confidential,
                ClientId = options.ClientId,
                ClientSecret = options.ClientSecret,
                RedirectUri = options.RedirectUri,
                Configuration = new OpenIddictConfiguration
                {
                    AuthorizationEndpoint = new Uri("https://app.clickup.com/api", UriKind.Absolute),
                    TokenEndpoint = new Uri("https://api.clickup.com/api/v2/oauth/token", UriKind.Absolute),
                    GrantTypesSupported = { OpenIddictConstants.GrantTypes.AuthorizationCode },
                    ResponseTypesSupported = { OpenIddictConstants.ResponseTypes.Code },
                    ResponseModesSupported = { OpenIddictConstants.ResponseModes.Query },
                    TokenEndpointAuthMethodsSupported = { OpenIddictConstants.ClientAuthenticationMethods.ClientSecretPost },
                },
                GrantTypes = { OpenIddictConstants.GrantTypes.AuthorizationCode },
                ResponseTypes = { OpenIddictConstants.ResponseTypes.Code },
                ClientAuthenticationMethods = { OpenIddictConstants.ClientAuthenticationMethods.ClientSecretPost },
            };

            clientOptions.Registrations.Add(registration);
        });

        return builder;
    }
}
