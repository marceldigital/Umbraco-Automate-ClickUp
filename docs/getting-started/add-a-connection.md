# Add a ClickUp connection

A **connection** links one Umbraco Automate Workspace to one ClickUp account. Actions read
their credentials from the connection configured on the automation step, so at least one
connection must exist before any ClickUp action becomes available.

## Add the connection

1. In the Umbraco backoffice, open **Automate** and select the Workspace you want to automate from.
2. Add a new connection. ClickUp is listed under the **Productivity** group, named **ClickUp**
   — "Connect to a ClickUp workspace".
3. Authorize the connection. You'll be redirected to ClickUp to approve the OAuth app, then
   returned to Umbraco.
4. Save. The connection validates itself immediately by calling ClickUp's
   `GET /api/v2/user` endpoint.

## Validation messages

Saving the connection reports one of the following:

| Message | Meaning |
|---|---|
| **Connected as {username}.** | Success. The token works, and this is the ClickUp account every action will act as. |
| **ClickUp rejected the access token.** | ClickUp returned a non-success response. The token is expired, revoked, or the app's credentials are wrong. |
| **Could not reach the ClickUp API.** | The request never got a response — check outbound network access and any proxy or firewall between the site and `api.clickup.com`. |

## Which account should authorize it?

The username reported back matters more than it looks. ClickUp has no app-level or bot
identity for OAuth apps, so every task this integration creates is attributed in ClickUp's
activity history to the account that authorized the connection.

**Use a dedicated or shared ClickUp account for automation**, not an individual's personal
account — otherwise the connection breaks when that person leaves the Workspace. See
[Tasks are attributed to a person](../troubleshooting/connection-identity.md) for the full
explanation.

## Next

The Workspace now has a ClickUp connection, so ClickUp actions appear in the step picker for
its automations. See [All actions](../actions/index.md).
