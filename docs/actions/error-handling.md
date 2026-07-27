# Error handling

Every ClickUp action reports failures using one of Umbraco Automate's standard
`StepRunErrorCategory` values. The category is what a workflow can branch on, so it's more
useful than the error message — messages come from ClickUp and can change, categories don't.

## Categories

| Category | What triggers it |
|---|---|
| `Validation` | A required input is missing or malformed *before* the request is sent, or ClickUp rejects the request as invalid. |
| `Authentication` | No usable ClickUp connection, or ClickUp refused the token. |
| `RateLimiting` | ClickUp returned HTTP 429. |
| `ServiceUnavailable` | ClickUp returned 5xx, or the request never reached ClickUp at all. |
| `InvalidResponse` | ClickUp responded with something unexpected that doesn't fit the above. |

## How HTTP responses map to categories

When ClickUp does respond, the status code determines the category:

| ClickUp response | Category |
|---|---|
| `401 Unauthorized`, `403 Forbidden` | `Authentication` |
| `429 Too Many Requests` | `RateLimiting` |
| `400 Bad Request`, `404 Not Found` | `Validation` |
| Any `5xx` | `ServiceUnavailable` |
| Anything else non-success | `InvalidResponse` |

`404` maps to `Validation` rather than a not-found category on purpose: in practice it almost
always means an ID supplied as input doesn't exist — a wrong List ID or parent task ID — which
is a problem with the step's configuration, not a transient fault.

## Failures before the request

Some failures never reach ClickUp:

| Situation | Category |
|---|---|
| A required input is empty | `Validation` |
| A date input isn't a valid date | `Validation` |
| The connection's access token can't be resolved or refreshed | `Authentication` |
| The HTTP request fails outright — DNS, TLS, timeout, no route | `ServiceUnavailable` |
| ClickUp returns success but a response body that can't be parsed | `InvalidResponse` |

## Branching a workflow on failures

The distinction that matters when designing a workflow is whether retrying could ever help:

- **`RateLimiting` and `ServiceUnavailable` are transient.** Retrying later is reasonable —
  back off rather than retrying immediately, particularly for `RateLimiting`.
- **`Validation` is not transient.** The step configuration or the incoming data is wrong;
  retrying produces the same result. Surface it to a human.
- **`Authentication` needs an operator.** Someone has to re-authorize the connection, so
  notify rather than retry. This is also what you'll see if the ClickUp account that authorized
  the connection was deactivated — see
  [Tasks are attributed to a person](../troubleshooting/connection-identity.md).
- **`InvalidResponse` is worth investigating.** It usually means ClickUp's API behaved in a way
  this package doesn't anticipate. Please
  [open an issue](https://github.com/marceldigital/Umbraco-Automate-ClickUp/issues) if you hit
  one reproducibly.

Per-action error tables list which of these a specific action can return — see
[Create task](create-task.md#errors).
