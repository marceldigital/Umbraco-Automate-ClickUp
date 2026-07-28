# All actions

Every ClickUp action is filed under the **ClickUp** group in an automation's step picker, and
every one requires a [ClickUp connection](../getting-started/add-a-connection.md) on the
Workspace.

| Action | Alias | What it does |
|---|---|---|
| [Create task](create-task.md) | `clickup.createTask` | Creates a task in a ClickUp List, returning the new task's ID, URL, status, and hierarchy. |

Actions don't appear in the step picker at all until the Workspace has a connection — see
[ClickUp actions are missing from the step picker](../troubleshooting/action-not-in-step-picker.md).

## Concepts

Two behaviours are shared by every action rather than documented per-action:

- [**Binding expressions**](binding-expressions.md) — how to feed runtime values from earlier
  workflow steps into an action's input fields.
- [**Error handling**](error-handling.md) — the error categories actions return, what triggers
  each one, and how to branch a workflow on them.
