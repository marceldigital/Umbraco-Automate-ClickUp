using System.Text.Json.Serialization;

namespace Umbraco.Community.Automate.ClickUp.Models;

/// <summary>
/// A task's priority, as returned nested in ClickUp task responses.
/// </summary>
internal sealed class ClickUpPriority
{
    [JsonPropertyName("priority")]
    public string? Priority { get; set; }
}
