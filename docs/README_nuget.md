<!-- Packed into the nupkg as <PackageReadmeFile> and rendered on nuget.org, where relative
     links do NOT resolve — every link here must be absolute. Keep it short; substantive docs
     live in /docs. Excluded from the DocFX content set in docs/docfx.json. -->

# Umbraco Automate ClickUp Add-on

[![Downloads](https://img.shields.io/nuget/dt/Umbraco.Community.Automate.ClickUp?color=cc9900)](https://www.nuget.org/packages/Umbraco.Community.Automate.ClickUp/)
[![NuGet](https://img.shields.io/nuget/vpre/Umbraco.Community.Automate.ClickUp?color=0273B3)](https://www.nuget.org/packages/Umbraco.Community.Automate.ClickUp)
[![GitHub license](https://img.shields.io/github/license/marceldigital/Umbraco-Automate-ClickUp?color=8AB803)](https://github.com/marceldigital/Umbraco-Automate-ClickUp/blob/main/LICENSE)

OAuth-based ClickUp provider for Umbraco Automate. Adds a ClickUp connection type and ClickUp
actions — starting with **Create Task** — to any Umbraco Automate workflow.

## Documentation

### **[marceldigital.github.io/Umbraco-Automate-ClickUp](https://marceldigital.github.io/Umbraco-Automate-ClickUp/)**

| Section | Covers |
|---|---|
| [Getting started](https://marceldigital.github.io/Umbraco-Automate-ClickUp/getting-started/) | Install the package, create a ClickUp OAuth app, add a connection |
| [Configuration](https://marceldigital.github.io/Umbraco-Automate-ClickUp/configuration/) | The `appsettings.json` section, and why `Scopes` stays empty |
| [Actions](https://marceldigital.github.io/Umbraco-Automate-ClickUp/actions/) | Every action with its inputs and outputs, binding expressions, error handling |
| [Troubleshooting](https://marceldigital.github.io/Umbraco-Automate-ClickUp/troubleshooting/) | Missing actions, task attribution, what each error category means |

## Installation

Requires Umbraco 18+ with Umbraco Automate.

```bash
dotnet add package Umbraco.Community.Automate.ClickUp
```

Then [create a ClickUp OAuth app](https://marceldigital.github.io/Umbraco-Automate-ClickUp/getting-started/clickup-oauth-app.html)
and [add a connection](https://marceldigital.github.io/Umbraco-Automate-ClickUp/getting-started/add-a-connection.html)
— actions don't appear in the step picker until a Workspace has one.

## License

Licensed under the [MIT License](https://github.com/marceldigital/Umbraco-Automate-ClickUp/blob/main/LICENSE).
