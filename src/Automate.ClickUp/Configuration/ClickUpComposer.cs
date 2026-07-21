using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace Umbraco.Community.Automate.ClickUp.Configuration;

/// <summary>
/// Registers ClickUp as an OpenIddict OAuth provider.
/// </summary>
public sealed class ClickUpComposer : IComposer
{
    /// <inheritdoc />
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddOpenIddict()
            .AddClient(options => options.UseWebProviders().AddClickUp());
    }
}
