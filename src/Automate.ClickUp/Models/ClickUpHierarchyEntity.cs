using System.Text.Json.Serialization;

namespace Umbraco.Community.Automate.ClickUp.Models;

/// <summary>
/// A minimal reference to a ClickUp Folder, legacy "project", or Space — these are returned
/// nested in task responses with only an <c>id</c> (Folder/Space may also include a
/// <c>name</c>, but callers here only need the ID for hierarchy context).
/// </summary>
internal sealed class ClickUpHierarchyEntity
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}
