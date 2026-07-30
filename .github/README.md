<!-- Keep this short. Substantive docs live in /docs and publish to the site linked below. -->

# Umbraco Automate ClickUp Add-on

[![Docs](https://img.shields.io/badge/docs-GitHub_Pages-2F6FA8)](https://marceldigital.github.io/Umbraco-Automate-ClickUp/)
[![Downloads](https://img.shields.io/nuget/dt/Umbraco.Community.Automate.ClickUp?color=cc9900)](https://www.nuget.org/packages/Umbraco.Community.Automate.ClickUp/)
[![NuGet](https://img.shields.io/nuget/vpre/Umbraco.Community.Automate.ClickUp?color=0273B3)](https://www.nuget.org/packages/Umbraco.Community.Automate.ClickUp)
[![GitHub license](https://img.shields.io/github/license/marceldigital/Umbraco-Automate-ClickUp?color=8AB803)](../LICENSE)

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

## Contributing

Contributions are most welcome. See the [Contributing Guidelines](CONTRIBUTING.md), and
[Contributing](https://marceldigital.github.io/Umbraco-Automate-ClickUp/contributing/) on the
docs site for the repository layout, how to preview the docs, and the OpenIddict internals.

## License

Licensed under the [MIT License](../LICENSE).
