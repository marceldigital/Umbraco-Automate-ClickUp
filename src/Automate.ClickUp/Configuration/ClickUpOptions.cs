namespace Umbraco.Community.Automate.ClickUp.Configuration;

/// <summary>
/// Options for configuring the ClickUp OAuth provider registration.
/// </summary>
public sealed class ClickUpOptions
{
    /// <summary>
    /// Gets or sets the ClickUp OAuth app client ID.
    /// </summary>
    public string? ClientId { get; set; }

    /// <summary>
    /// Gets or sets the ClickUp OAuth app client secret.
    /// </summary>
    public string? ClientSecret { get; set; }

    /// <summary>
    /// Gets or sets the redirect URI registered with the ClickUp OAuth app.
    /// </summary>
    public Uri? RedirectUri { get; set; }
}
