using Umbraco.Automate.Core.Settings;

namespace Umbraco.Community.Automate.ClickUp.Connection;

/// <summary>
/// Settings for the ClickUp connection type.
/// </summary>
public sealed class ClickUpConnectionSettings
{
    /// <summary>
    /// Gets or sets the OAuth credential ID linking to the stored ClickUp account tokens.
    /// </summary>
    [Field(
        Label = "ClickUp Workspace",
        Description = "Authenticate with a ClickUp account. ClickUp attributes every action "
            + "(tasks, comments, etc.) to whichever account authorizes here, not to this app — "
            + "use a dedicated/shared account rather than a personal one.",
        EditorUiAlias = "Umb.Automate.OAuth",
        EditorConfig = """[{ "alias": "provider", "value": "ClickUp" }]""")]
    public Guid? OAuthCredentialsId { get; set; }
}
