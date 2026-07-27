# Tasks are attributed to a person, not the integration

**Symptom:** tasks created by an automation show up in ClickUp's activity history as created by
a named colleague, rather than by "Umbraco Automate" or a bot.

This is expected, and it can't be changed from this package.

## Why

ClickUp's API has no app-level or bot identity for OAuth apps. Every action this integration
performs — creating tasks, adding comments — is attributed to whichever ClickUp account
authorized the connection. There is no "act as the app" mode to switch on.

This is a ClickUp platform limitation rather than something this package works around, and it
applies equally to a personal API token. The
[Create task](../actions/create-task.md) action surfaces the account on every run through its
`Creator Username` output, so you can see which identity is being used without digging through
ClickUp's activity log.

## What to do about it

**Connect using a dedicated or shared ClickUp account created for automation**, rather than an
individual's personal account. This has two benefits beyond a tidier activity history:

- **The connection survives staff changes.** If the authorizing person's account is deactivated
  or removed from the Workspace, the connection stops working — every subsequent run fails with
  an `Authentication` error (see [Error handling](../actions/error-handling.md)). A
  purpose-made account isn't tied to anyone's employment.
- **Access is scopeable.** Because the integration inherits the authorizing account's
  permissions, you can limit what automations can reach by limiting that account's access to
  Spaces, Folders, and Lists. See [OAuth scopes](../configuration/scopes.md).

## Changing the account on an existing connection

Re-authorize the [connection](../getting-started/add-a-connection.md) while signed into ClickUp
as the account you want to use. Attribution applies from that point forward — tasks already
created keep the original creator in ClickUp's history, since that's immutable.
