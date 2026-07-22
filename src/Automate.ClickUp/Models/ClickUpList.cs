using System.Text.Json.Serialization;

namespace Umbraco.Community.Automate.ClickUp.Models;

/// <summary>
/// The ClickUp List a task belongs to, as returned nested in ClickUp task responses.
/// </summary>
internal sealed class ClickUpList
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
