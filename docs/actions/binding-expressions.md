# Binding expressions

Most action inputs can take a value computed at runtime instead of one typed in when the
automation was authored. That's what makes an action useful in a workflow rather than a fixed
template.

A binding expression references the output of an earlier step:

```text
${trigger.output.publishDate}
```

Bindings are an Umbraco Automate feature, not a ClickUp one — anywhere Automate supports them,
these actions do too.

## Why some fields are plain text

Several inputs that look like they should be a picker are deliberately plain text fields.
**Due Date** and **Start Date** on [Create task](create-task.md) are the clearest example: they
accept an ISO-8601 value such as `2026-08-01` or `2026-08-01T14:30:00`, *or* a binding
expression.

A date-picker control would only ever produce a fixed date chosen at authoring time. Plain text
lets a workflow source the value from whatever triggered it — setting a ClickUp task's due date
from the publish date of the content item that started the automation, for instance. The
trade-off is that the value isn't validated until the step runs, at which point an unparseable
date fails with a `Validation` error (see [Error handling](error-handling.md)).

The same reasoning applies to ID fields such as **List ID** and **Parent Task ID** — a workflow
can resolve them dynamically rather than hard-coding one List.

## Time of day is inferred

For date inputs, whether the value is treated as time-specific is derived from the value
itself, not from a separate setting. If the date you supply includes a time other than
midnight, ClickUp treats it as time-specific; otherwise it's a date-only value. There is no
"include time" toggle to configure, which means a binding that yields a full timestamp behaves
correctly without any extra step configuration.

## Comma-separated inputs

Fields that accept several values — **Assignees**, **Tags** — take a comma-separated string
(`12345,67890`). A binding that produces an already-joined string works directly; if an earlier
step produces a collection, join it before passing it in.
