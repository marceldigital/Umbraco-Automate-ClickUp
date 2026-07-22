using Umbraco.Automate.Core.Settings;

namespace Umbraco.Community.Automate.ClickUp.Actions;

/// <summary>
/// Settings for the <see cref="CreateTaskAction"/>.
/// </summary>
public sealed class CreateTaskSettings
{
    /// <summary>
    /// Gets or sets the ClickUp List ID to create the task in.
    /// </summary>
    [Field(
        Label = "List ID",
        Description = "The ClickUp List ID to create the task in. Found in the List's URL or List settings.",
        SupportsBindings = true)]
    public string ListId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the task's title.
    /// </summary>
    [Field(
        Label = "Task Name",
        Description = "The title of the task.",
        SortOrder = 1,
        SupportsBindings = true)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the task description. Always sent to ClickUp as markdown content, so
    /// plain text and Markdown both render correctly without a separate "rich text" field.
    /// </summary>
    [Field(
        Label = "Description",
        Description = "The task description. Supports Markdown formatting.",
        EditorUiAlias = "Umb.PropertyEditorUi.MarkdownEditor",
        EditorConfig = """[{ "alias": "preview", "value": true }]""",
        SortOrder = 2,
        SupportsBindings = true)]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets a comma-separated list of ClickUp user IDs to assign to the task.
    /// </summary>
    [Field(
        Label = "Assignees",
        Description = "Comma-separated ClickUp user IDs to assign to the task (e.g. 12345,67890).",
        SortOrder = 3,
        SupportsBindings = true)]
    public string? Assignees { get; set; }

    /// <summary>
    /// Gets or sets a comma-separated list of tag names to apply to the task.
    /// </summary>
    [Field(
        Label = "Tags",
        Description = "Comma-separated tag names to apply to the task.",
        SortOrder = 4,
        SupportsBindings = true)]
    public string? Tags { get; set; }

    /// <summary>
    /// Gets or sets the ClickUp status name to set on the task.
    /// </summary>
    [Field(
        Label = "Status",
        Description = "The ClickUp status name to set (must match an existing status on the target "
            + "List, e.g. \"to do\"). Leave blank to use the List's default status.",
        SortOrder = 5,
        SupportsBindings = true)]
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets the task priority (1 = Urgent, 2 = High, 3 = Normal, 4 = Low).
    /// </summary>
    [Field(
        Label = "Priority",
        Description = "Task priority: 1 = Urgent, 2 = High, 3 = Normal, 4 = Low. Leave blank for no priority.",
        SortOrder = 6)]
    public int? Priority { get; set; }

    /// <summary>
    /// Gets or sets the task's due date. Accepts an ISO-8601 date/datetime string or a
    /// <c>${ }</c> binding expression — kept as a plain bindable string rather than a
    /// structured date type so it can be sourced dynamically at runtime (e.g. from a
    /// triggering content item's scheduled publish date).
    /// </summary>
    [Field(
        Label = "Due Date",
        Description = "The task's due date. Accepts an ISO-8601 date/datetime (e.g. 2026-08-01 or "
            + "2026-08-01T14:30:00) or a binding expression. If a specific time of day is set (not "
            + "midnight), ClickUp will treat the due date as time-specific.",
        SortOrder = 7,
        SupportsBindings = true)]
    public string? DueDate { get; set; }

    /// <summary>
    /// Gets or sets the task's start date. Same format and binding support as <see cref="DueDate"/>.
    /// </summary>
    [Field(
        Label = "Start Date",
        Description = "The task's start date. Accepts an ISO-8601 date/datetime (e.g. 2026-08-01 or "
            + "2026-08-01T14:30:00) or a binding expression. If a specific time of day is set (not "
            + "midnight), ClickUp will treat the start date as time-specific.",
        SortOrder = 8,
        SupportsBindings = true)]
    public string? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the estimated effort for the task, in minutes.
    /// </summary>
    [Field(
        Label = "Time Estimate (minutes)",
        Description = "Estimated time to complete the task, in minutes. Leave blank for no estimate.",
        SortOrder = 9)]
    public int? TimeEstimateMinutes { get; set; }

    /// <summary>
    /// Gets or sets an existing ClickUp task ID to create this task as a subtask of.
    /// </summary>
    [Field(
        Label = "Parent Task ID",
        Description = "An existing ClickUp task ID to create this task as a subtask of.",
        SortOrder = 10,
        SupportsBindings = true)]
    public string? ParentTaskId { get; set; }

    /// <summary>
    /// Gets or sets an existing ClickUp task ID to link this new task to as a dependency.
    /// </summary>
    [Field(
        Label = "Linked Task ID",
        Description = "An existing ClickUp task ID to link this new task to as a dependency.",
        SortOrder = 11,
        SupportsBindings = true)]
    public string? LinksToTaskId { get; set; }

    /// <summary>
    /// Gets or sets whether ClickUp should notify assignees, watchers, and the task creator.
    /// </summary>
    [Field(
        Label = "Notify All",
        Description = "Whether to notify assignees, watchers, and the task creator about this new task.",
        EditorUiAlias = "Umb.PropertyEditorUi.Toggle",
        SortOrder = 12)]
    public bool NotifyAll { get; set; }
}
