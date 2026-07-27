# Getting started

Setting up ClickUp actions in Umbraco Automate takes three steps, in this order:

1. [**Install the package**](installation.md) — add `Umbraco.Community.Automate.ClickUp` to an Umbraco 18+ site running Umbraco Automate.
2. [**Create a ClickUp OAuth app**](clickup-oauth-app.md) — register an app in ClickUp and put its Client ID and Client Secret in `appsettings.json`.
3. [**Add a ClickUp connection**](add-a-connection.md) — authorize the app against a ClickUp Workspace from the Umbraco backoffice.

Step 3 is the one people miss. ClickUp actions do not appear in an automation's step picker
until the Workspace has a ClickUp connection configured — see
[ClickUp actions are missing from the step picker](../troubleshooting/action-not-in-step-picker.md)
if you get that far and the actions aren't there.

Once a connection exists, head to [Actions](../actions/index.md).
