# Create Task

Creates a task in a ClickUp List. Backed by ClickUp's [Create Task API](https://developer.clickup.com/reference/createtask) (`POST /api/v2/list/{list_id}/task`).

- **Alias:** `clickup.createTask`
- **Group:** ClickUp
- **Requires:** a [ClickUp connection](../README_nuget.md)

## Requirements

This action requires a ClickUp connection configured on the automation step. As noted in the package README, ClickUp has no app-level identity for OAuth apps — every task this action creates is attributed in ClickUp's activity history to whichever ClickUp account authorized the connection, not to "Umbraco Automate". Use a dedicated/shared ClickUp account for automation rather than an individual's personal account.

**Don't see this action in the step picker?** It only appears once the current Workspace has a ClickUp connection configured — see [Usage](../README_nuget.md#usage) in the package README.

## Inputs

| Field | Type | Required | Description |
|---|---|---|---|
| List ID | Text | Yes | The ClickUp List ID to create the task in. Found in the List's URL or List settings. |
| Task Name | Text | Yes | The title of the task. |
| Description | Markdown editor | No | The task description, with a live preview. Supports Markdown formatting — see [Design notes](#design-notes). |
| Assignees | Text | No | Comma-separated ClickUp user IDs to assign to the task (e.g. `12345,67890`). |
| Tags | Text | No | Comma-separated tag names to apply to the task. |
| Status | Text | No | Must match an existing status name on the target List. Leave blank to use the List's default status. |
| Priority | Number | No | `1` = Urgent, `2` = High, `3` = Normal, `4` = Low. Leave blank for no priority. |
| Due Date | Text | No | An ISO-8601 date/datetime (e.g. `2026-08-01` or `2026-08-01T14:30:00`) or a binding expression (e.g. `${trigger.output.publishDate}`). See [Design notes](#design-notes). |
| Start Date | Text | No | Same format as Due Date. |
| Time Estimate (minutes) | Number | No | Estimated effort in minutes. Converted to ClickUp's millisecond `time_estimate` before sending. |
| Parent Task ID | Text | No | An existing ClickUp task ID — creates this task as a subtask of it. |
| Linked Task ID | Text | No | An existing ClickUp task ID to link this new task to as a dependency. |
| Notify All | Toggle | No | Whether ClickUp notifies assignees, watchers, and the task creator about the new task. |

Fields intentionally not exposed in this version: due/start time-of-day toggles (inferred automatically — see below), Sprint Points, Group Assignees, Archived, Check Required Custom Fields, Custom Fields, Custom Item ID.

## Outputs

| Field | Type | Description |
|---|---|---|
| Task ID | Text | The created ClickUp task's ID — usable by later steps (e.g. to update or comment on it). |
| Name | Text | The task's name as stored by ClickUp. |
| Url | Text | Direct link to the task in ClickUp. |
| Status Name | Text | The resulting status name — the List's default if none was specified on input. |
| Status Type | Text | The status's category (`open`, `custom`, `done`, or `closed`) — stable across Lists that use different custom status names, so it's more reliable for workflow branching than Status Name alone. |
| Priority | Text | The resulting priority name (e.g. `normal`), or empty if no priority is set. |
| Custom ID | Text | ClickUp's human-readable custom task ID, if the workspace has custom IDs enabled; otherwise empty. |
| Parent Task ID | Text | The immediate parent task ID when this task was created as a subtask; empty for top-level tasks. |
| Top Level Parent Task ID | Text | The root ancestor's task ID walking all the way up a subtask chain, straight from ClickUp — no need to walk Parent Task ID manually. Empty for top-level tasks. |
| Date Created | Date | The task's creation timestamp per ClickUp. |
| Creator ID | Number | The ClickUp user ID of the account this task was created under. |
| Creator Username | Text | The ClickUp username of the account this task was created under — a per-task reminder of the attribution caveat above. |
| List Name | Text | The target List's display name — confirms where the task landed, especially useful when List ID was resolved dynamically via a binding. |
| Space ID | Text | The containing Space's ID. |
| Folder ID | Text | The containing Folder's ID. Empty if the List isn't inside a Folder. |
| Project ID | Text | The containing Folder's ID under ClickUp's legacy `project` key (in practice, the same value as Folder ID). |

## Design notes

- **Description is always sent as Markdown content.** Whatever you enter in Description — plain text or Markdown — is sent to ClickUp's `markdown_content` field, never the separate plain `description` field. Plain text and Markdown both render correctly through this one field, so there's no need to choose between a "plain" and "rich" description field.
- **Due/Start Date accept dynamic values.** Both fields are plain text rather than a date-picker, specifically so a workflow can source them from something dynamic at runtime — for example, setting Due Date to a triggering content item's scheduled publish date via a binding expression — rather than only a fixed value chosen when the automation was authored.
- **Due/Start "time of day" is inferred, not a separate setting.** If the date you provide includes a specific time (not midnight), ClickUp treats it as time-specific automatically; otherwise it's treated as a date-only value. There's no separate toggle to configure.

## Errors

| Category | When |
|---|---|
| Validation | List ID or Task Name missing; Due Date/Start Date isn't a valid date; ClickUp rejects the request as malformed (e.g. an invalid List ID). |
| Authentication | No ClickUp connection configured, or its access token is expired/revoked. |
| RateLimiting | ClickUp responds with HTTP 429. |
| ServiceUnavailable | ClickUp responds with a 5xx error, or the request fails to reach ClickUp at all. |
| InvalidResponse | Any other unexpected non-success response. |

## Planned enhancements

- Replace the free-text List ID field with a cascading Space → Folder → List picker (matching the UX of other ClickUp integrations), instead of requiring the numeric List ID to be copied in manually.
- Support for ClickUp Custom Fields.
