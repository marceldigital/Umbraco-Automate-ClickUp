using Umbraco.Automate.OpenIddict.Providers;

// Schema wrapper consumed by the JsonSchemaGenerate MSBuild task at build time.
// Describes the appsettings.json shape below Umbraco:Automate:Providers:ClickUp so
// tooling can give editors IntelliSense against appsettings-schema.Umbraco.Automate.ClickUp.json.
internal sealed class UmbracoAutomateClickUpSchema
{
    public required UmbracoDefinition Umbraco { get; set; }

    public sealed class UmbracoDefinition
    {
        public required UmbracoAutomateDefinition Automate { get; set; }
    }

    public sealed class UmbracoAutomateDefinition
    {
        public required ProvidersDefinition Providers { get; set; }
    }

    public sealed class ProvidersDefinition
    {
        /// <summary>
        /// ClickUp OAuth app credentials.
        /// </summary>
        public required ClickUpProviderDefinition ClickUp { get; set; }
    }

    /// <summary>
    /// ClickUp provider section (<c>Umbraco:Automate:Providers:ClickUp</c>). Inherits
    /// the shared OAuth fields (<c>ClientId</c>, <c>ClientSecret</c>, <c>Scopes</c>)
    /// from <see cref="OAuthProviderConfiguration"/>. No ClickUp-specific fields are
    /// needed — unlike Slack, there's no non-standard scope split to configure.
    /// </summary>
    public sealed class ClickUpProviderDefinition : OAuthProviderConfiguration;
}
