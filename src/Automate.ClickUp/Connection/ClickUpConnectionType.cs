using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Umbraco.Automate.Core.Connections;
using Umbraco.Automate.OpenIddict.ConnectionTypes;
using Umbraco.Automate.OpenIddict.Credentials;

namespace Umbraco.Community.Automate.ClickUp.Connection;

/// <summary>
/// Connection type for ClickUp workspaces using OAuth via OpenIddict.
/// </summary>
[ConnectionType("clickup", "ClickUp", Group = "Productivity", Icon = "icon-plugin",
    Description = "Connect to a ClickUp workspace")]
public sealed class ClickUpConnectionType : OAuthConnectionTypeBase<ClickUpConnectionSettings>
{
    private readonly IHttpClientFactory _httpClientFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="ClickUpConnectionType"/> class.
    /// </summary>
    public ClickUpConnectionType(
        ConnectionTypeInfrastructure infrastructure,
        IOAuthCredentialsService credentialsService,
        IHttpClientFactory httpClientFactory)
        : base(infrastructure, credentialsService)
    {
        _httpClientFactory = httpClientFactory;
    }

    /// <inheritdoc />
    public override string ProviderName => "ClickUp";

    /// <summary>
    /// Adds a ClickUp-specific check on top of the base token-resolution check: calls
    /// <c>GET /api/v2/user</c> to confirm the token actually works and returns the
    /// authenticated user's name on success.
    /// </summary>
    public override async Task<ConnectionValidationResult> ValidateAsync(
        object? settings,
        CancellationToken cancellationToken)
    {
        var baseResult = await base.ValidateAsync(settings, cancellationToken);
        if (baseResult.Status != ConnectionValidationStatus.Success)
        {
            return baseResult;
        }

        var credentialsId = ((ClickUpConnectionSettings)settings!).OAuthCredentialsId!.Value;
        var token = await CredentialsService.GetValidAccessTokenAsync(credentialsId, cancellationToken);

        using var client = _httpClientFactory.CreateClient("UmbracoAutomate");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        ClickUpUserResponse? response;
        try
        {
            using var httpResponse = await client.GetAsync("https://api.clickup.com/api/v2/user", cancellationToken);
            response = await httpResponse.Content.ReadFromJsonAsync<ClickUpUserResponse>(cancellationToken);

            if (!httpResponse.IsSuccessStatusCode || response?.User is null)
            {
                return ConnectionValidationResult.Failure("ClickUp rejected the access token.");
            }
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            return ConnectionValidationResult.Failure("Could not reach the ClickUp API.", [ex.Message]);
        }

        var username = response.User.Username ?? "your ClickUp account";
        return ConnectionValidationResult.Success($"Connected as {username}.");
    }

    private sealed class ClickUpUserResponse
    {
        [JsonPropertyName("user")]
        public ClickUpUser? User { get; set; }
    }

    private sealed class ClickUpUser
    {
        [JsonPropertyName("username")]
        public string? Username { get; set; }
    }
}
