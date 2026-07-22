using System.Text.Json.Serialization;

namespace Umbraco.Community.Automate.ClickUp.Models;

/// <summary>
/// A ClickUp task, as returned by task endpoints (Create Task, Get Task, Update Task, etc.).
/// Only the fields actions in this package currently consume are mapped — see
/// <see href="https://developer.clickup.com/reference/createtask">ClickUp's Create Task reference</see>
/// for the full response shape.
/// </summary>
internal sealed class ClickUpTaskResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("custom_id")]
    public string? CustomId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("status")]
    public ClickUpStatus? Status { get; set; }

    [JsonPropertyName("date_created")]
    public string? DateCreated { get; set; }

    [JsonPropertyName("creator")]
    public ClickUpCreator? Creator { get; set; }

    [JsonPropertyName("parent")]
    public string? Parent { get; set; }

    [JsonPropertyName("top_level_parent")]
    public string? TopLevelParent { get; set; }

    [JsonPropertyName("priority")]
    public ClickUpPriority? Priority { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("list")]
    public ClickUpList? List { get; set; }

    [JsonPropertyName("folder")]
    public ClickUpHierarchyEntity? Folder { get; set; }

    [JsonPropertyName("project")]
    public ClickUpHierarchyEntity? Project { get; set; }

    [JsonPropertyName("space")]
    public ClickUpHierarchyEntity? Space { get; set; }
}
