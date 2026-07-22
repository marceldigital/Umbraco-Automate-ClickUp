using System.Text.Json.Serialization;

namespace Umbraco.Community.Automate.ClickUp.Models;

/// <summary>
/// The ClickUp account a task (or other entity) is attributed to, as returned nested
/// in ClickUp responses.
/// </summary>
internal sealed class ClickUpCreator
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("username")]
    public string? Username { get; set; }
}
