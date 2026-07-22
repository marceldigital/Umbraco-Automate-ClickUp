using System.Text.Json.Serialization;

namespace Umbraco.Community.Automate.ClickUp.Models;

/// <summary>
/// Request body for ClickUp's Create Task endpoint (<c>POST /api/v2/list/{list_id}/task</c>).
/// Field-for-field specific to that endpoint — unlike the response models in this namespace,
/// not assumed reusable by other task endpoints (e.g. Update Task) without checking their
/// actual accepted shape first.
/// </summary>
internal sealed class ClickUpCreateTaskRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("markdown_content")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? MarkdownContent { get; set; }

    [JsonPropertyName("assignees")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int[]? Assignees { get; set; }

    [JsonPropertyName("tags")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? Tags { get; set; }

    [JsonPropertyName("status")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Status { get; set; }

    [JsonPropertyName("priority")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Priority { get; set; }

    [JsonPropertyName("due_date")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? DueDate { get; set; }

    [JsonPropertyName("due_date_time")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? DueDateTime { get; set; }

    [JsonPropertyName("start_date")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? StartDate { get; set; }

    [JsonPropertyName("start_date_time")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? StartDateTime { get; set; }

    [JsonPropertyName("time_estimate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? TimeEstimate { get; set; }

    [JsonPropertyName("parent")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Parent { get; set; }

    [JsonPropertyName("links_to")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? LinksTo { get; set; }

    [JsonPropertyName("notify_all")]
    public bool NotifyAll { get; set; }
}
