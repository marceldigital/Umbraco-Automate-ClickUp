using System.Net.Http.Headers;
using Umbraco.Automate.OpenIddict.Credentials;
using Umbraco.Community.Automate.ClickUp.Connection;

namespace Umbraco.Community.Automate.ClickUp;

/// <summary>
/// Creates a ClickUp-ready <see cref="HttpClient"/> — shared across every action that calls the
/// ClickUp API, so resolving the connection's access token, getting the shared named client, and
/// setting the auth header/base address only need to be written once.
/// </summary>
internal static class HttpClientFactoryExtensions
{
    private const string ClickUpApiBaseUrl = "https://api.clickup.com/api/v2/";

    /// <summary>
    /// Resolves <paramref name="connectionSettings"/>'s access token via <paramref name="credentialsService"/>
    /// and returns an <see cref="HttpClient"/> (from the shared <c>"UmbracoAutomate"</c> named client)
    /// with its <see cref="HttpClient.BaseAddress"/> set to ClickUp's API root and a bearer
    /// <c>Authorization</c> header already attached — callers only need a path relative to
    /// <c>https://api.clickup.com/api/v2/</c> (no leading slash). Pass
    /// <c>context.Connection?.GetSettings&lt;ClickUpConnectionSettings&gt;()</c> as
    /// <paramref name="connectionSettings"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown when <paramref name="connectionSettings"/> is <see langword="null"/>, the connection
    /// isn't authenticated, or its access token is expired/revoked. Callers should catch this and
    /// map it to <see cref="Umbraco.Automate.Core.Actions.StepRunErrorCategory.Authentication"/>.
    /// </exception>
    public static async Task<HttpClient> CreateClickUpClientAsync(
        this IHttpClientFactory httpClientFactory,
        ClickUpConnectionSettings? connectionSettings,
        IOAuthCredentialsService credentialsService,
        CancellationToken cancellationToken)
    {
        if (connectionSettings is null)
        {
            throw new InvalidOperationException("A ClickUp connection is required.");
        }

        if (connectionSettings.OAuthCredentialsId is not { } credentialId || credentialId == Guid.Empty)
        {
            throw new InvalidOperationException("ClickUp workspace is not authenticated.");
        }

        var accessToken = await credentialsService.GetValidAccessTokenAsync(credentialId, cancellationToken)
            ?? throw new InvalidOperationException("ClickUp access token is expired or revoked. Please re-authenticate.");

        var client = httpClientFactory.CreateClient("UmbracoAutomate");
        client.BaseAddress = new Uri(ClickUpApiBaseUrl);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        return client;
    }
}
