using System.Text.Json.Serialization;

namespace Umbraco.Community.Automate.ClickUp.Models;

/// <summary>
/// A task's status, as returned nested in ClickUp task responses.
/// </summary>
internal sealed class ClickUpStatus
{
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// The status's category (e.g. "open", "custom", "done", "closed") — stable across Lists
    /// that use different custom status names, unlike <see cref="Status"/> itself.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }
}
