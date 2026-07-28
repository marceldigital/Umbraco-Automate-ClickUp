# ClickUp actions are missing from the step picker

**Symptom:** the package is installed and the site has restarted, but adding a step to an
automation shows no **ClickUp** group and no ClickUp actions.

## The usual cause

ClickUp actions only appear once the current Workspace has a
[ClickUp connection](../getting-started/add-a-connection.md) configured. This is deliberate —
Automate hides actions whose connection type isn't available — but it looks identical to the
package not having loaded at all.

**Fix:** add a ClickUp connection to that Workspace, then reopen the step picker.

Note that connections are **per Workspace**. Actions appearing in one Workspace and not another
is expected behaviour, not a bug: each Workspace needs its own connection.

## If a connection exists and actions still don't appear

Work through these in order:

1. **Confirm the connection is on the Workspace you're editing the automation in.** This is the
   same cause as above wearing a disguise, and it accounts for most of the remaining cases.
2. **Confirm the connection saved successfully.** A connection that failed validation won't make
   the actions available. Re-open it and check it reports
   **"Connected as {username}."** — the other two messages are covered under
   [validation messages](../getting-started/add-a-connection.md#validation-messages).
3. **Confirm the package is actually loaded.** Check that
   `Umbraco.Community.Automate.ClickUp.dll` is in the site's `bin` folder and that no startup
   errors were logged. The package registers itself through an `IComposer`, so a composer
   failure would show in the startup log.
4. **Confirm the configuration section is present.** Missing or malformed
   `Umbraco:Automate:Providers:ClickUp` credentials prevent the OAuth provider from
   registering, which in turn prevents a connection from being created. See the
   [appsettings.json reference](../configuration/appsettings.md).
5. **Restart the site.** The connection type and OAuth provider are registered at startup. If
   `appsettings.json` was edited while the site was running, the change hasn't been picked up.

If none of that resolves it, please
[open an issue](https://github.com/marceldigital/Umbraco-Automate-ClickUp/issues) with your
Umbraco and Umbraco Automate versions and any relevant startup log entries.
