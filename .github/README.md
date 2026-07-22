# Umbraco Automate ClickUp Add-on

[![Downloads](https://img.shields.io/nuget/dt/Umbraco.Community.Automate.ClickUp?color=cc9900)](https://www.nuget.org/packages/Umbraco.Community.Automate.ClickUp/)
[![NuGet](https://img.shields.io/nuget/vpre/Umbraco.Community.Automate.ClickUp?color=0273B3)](https://www.nuget.org/packages/Umbraco.Community.Automate.ClickUp)
[![GitHub license](https://img.shields.io/github/license/marceldigital/Umbraco-Automate-ClickUp?color=8AB803)](../LICENSE)

OAuth-based ClickUp provider package for Umbraco Automate.

## Overview

This repository contains the Umbraco.Community.Automate.ClickUp package. The extension follows the same project and documentation conventions as the official Umbraco Automate provider packages, with an OAuth-only ClickUp connection model.

## Key Features

- ClickUp provider package baseline aligned with Umbraco Automate conventions.
- OAuth credential flow designed for Umbraco Automate OpenIddict integration.
- **Create Task** action — create tasks in a ClickUp List from any Umbraco Automate workflow. See [docs/actions/create-task.md](../docs/actions/create-task.md).

## Installation

Add the package to an existing Umbraco website (v18+) from NuGet:

`dotnet add package Umbraco.Community.Automate.ClickUp`

## Configuration

Configure ClickUp OAuth credentials under `Umbraco:Automate:Providers:ClickUp` in `appsettings.json`:

```json
{
	"Umbraco": {
		"Automate": {
			"Providers": {
				"ClickUp": {
					"ClientId": "...",
					"ClientSecret": "...",
					"Scopes": []
				}
			}
		}
	}
}
```

The initial release work focused on provider setup and package baseline alignment. Action implementations are landing next — the Create Task action ships first, with more to follow.

## Usage

1. Install the package and configure your OAuth app credentials as above.
2. In the Umbraco backoffice, add a **ClickUp connection** to the Workspace you want to automate from.
3. ClickUp actions (e.g. Create Task) only appear in an automation's step picker once that Workspace has a ClickUp connection configured — if you don't see them yet, this is almost always why.

## Connection identity

ClickUp's API has no app-level or bot identity for OAuth apps — every action this
integration performs (creating tasks, adding comments, etc.) is attributed in
ClickUp's activity history to whichever ClickUp account authorized the connection,
not to "Umbraco Automate". This is a ClickUp platform limitation, not something
this package can work around; it applies equally to a personal API token.

**Recommendation:** connect this integration using a dedicated or shared ClickUp
account created for automation purposes, rather than an individual's personal
account. This keeps the activity history meaningful and avoids the connection
breaking if that person's account is later deactivated or removed from the
workspace.

## Contributing

Contributions to this package are most welcome! Please read the [Contributing Guidelines](CONTRIBUTING.md).

## License

Licensed under the [MIT License](../LICENSE).