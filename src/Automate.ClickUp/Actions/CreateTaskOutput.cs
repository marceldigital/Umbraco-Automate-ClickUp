namespace Umbraco.Community.Automate.ClickUp.Actions;

/// <summary>
/// Output produced by the <see cref="CreateTaskAction"/>.
/// </summary>
public sealed class CreateTaskOutput
{
    /// <summary>
    /// Gets the created ClickUp task's ID.
    /// </summary>
    public string? TaskId { get; init; }

    /// <summary>
    /// Gets the task's name as stored by ClickUp.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Gets the direct link to the task in ClickUp.
    /// </summary>
    public string? Url { get; init; }

    /// <summary>
    /// Gets the resulting status name (the List's default if none was specified on input).
    /// </summary>
    public string? StatusName { get; init; }

    /// <summary>
    /// Gets the resulting status's category (e.g. "open", "custom", "done", "closed") — stable
    /// across Lists that use different custom status names.
    /// </summary>
    public string? StatusType { get; init; }

    /// <summary>
    /// Gets the resulting priority name (e.g. "normal"), or <see langword="null"/> if no priority is set.
    /// </summary>
    public string? Priority { get; init; }

    /// <summary>
    /// Gets ClickUp's human-readable custom task ID, if the workspace has custom IDs enabled;
    /// <see langword="null"/> otherwise.
    /// </summary>
    public string? CustomId { get; init; }

    /// <summary>
    /// Gets the immediate parent task ID when this task was created as a subtask;
    /// <see langword="null"/> for top-level tasks.
    /// </summary>
    public string? ParentTaskId { get; init; }

    /// <summary>
    /// Gets the root ancestor's task ID walking all the way up a subtask chain;
    /// <see langword="null"/> for top-level tasks.
    /// </summary>
    public string? TopLevelParentTaskId { get; init; }

    /// <summary>
    /// Gets the task's creation timestamp per ClickUp.
    /// </summary>
    public DateTimeOffset? DateCreated { get; init; }

    /// <summary>
    /// Gets the ClickUp user ID of the account this task was created under.
    /// </summary>
    public int? CreatorId { get; init; }

    /// <summary>
    /// Gets the ClickUp username of the account this task was created under.
    /// </summary>
    public string? CreatorUsername { get; init; }

    /// <summary>
    /// Gets the target List's display name.
    /// </summary>
    public string? ListName { get; init; }

    /// <summary>
    /// Gets the containing Space's ID.
    /// </summary>
    public string? SpaceId { get; init; }

    /// <summary>
    /// Gets the containing Folder's ID, or <see langword="null"/> if the List isn't inside a Folder.
    /// </summary>
    public string? FolderId { get; init; }

    /// <summary>
    /// Gets the containing Folder's ID under ClickUp's legacy "project" key.
    /// </summary>
    public string? ProjectId { get; init; }
}
