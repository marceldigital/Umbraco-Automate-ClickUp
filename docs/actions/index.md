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

## Adding a new action

Action docs follow a fixed shape so the section scales without restructuring. To document a
new action:

1. Add `docs/actions/<alias>.md`, using [create-task.md](create-task.md) as the template. Keep
   its heading structure: **Requirements**, **Inputs**, **Outputs**, **Design notes**,
   **Errors**, **Planned enhancements**.
2. Add one entry under the `Actions` group in `docs/actions/toc.yml`.
3. Add one row to the table above.

Link out to [Binding expressions](binding-expressions.md) and
[Error handling](error-handling.md) rather than restating them — those pages exist so each
action doc doesn't have to repeat the same explanations. Likewise, link the connection
requirement to [Add a ClickUp connection](../getting-started/add-a-connection.md) instead of
re-describing it.

Document inputs and outputs as tables with the field labels exactly as they appear in the
backoffice, so someone can match the doc against the UI field by field.
