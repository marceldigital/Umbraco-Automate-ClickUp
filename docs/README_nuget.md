# Umbraco Automate ClickUp Add-on

[![Downloads](https://img.shields.io/nuget/dt/Umbraco.Community.Automate.ClickUp?color=cc9900)](https://www.nuget.org/packages/Umbraco.Community.Automate.ClickUp/)
[![NuGet](https://img.shields.io/nuget/vpre/Umbraco.Community.Automate.ClickUp?color=0273B3)](https://www.nuget.org/packages/Umbraco.Community.Automate.ClickUp)
[![GitHub license](https://img.shields.io/github/license/marceldigital/Umbraco-Automate-ClickUp?color=8AB803)](https://github.com/marceldigital/Umbraco-Automate-ClickUp/blob/main/LICENSE)

OAuth-based ClickUp provider package for Umbraco Automate.

## Overview

Umbraco.Community.Automate.ClickUp adds a ClickUp provider to Umbraco Automate using OAuth credentials managed through the Umbraco Automate OpenIddict pipeline.

## Key Features

- ClickUp provider foundation aligned with Umbraco Automate provider conventions.
- OAuth-first authentication model for secure credential handling.
- Package structure and configuration pattern modeled after the official Slack provider package.
- **Create Task** action — create tasks in a ClickUp List from any Umbraco Automate workflow. See [docs/actions/create-task.md](https://github.com/marceldigital/Umbraco-Automate-ClickUp/blob/main/docs/actions/create-task.md).

## Installation

Add the package to an existing Umbraco website (v18+) from NuGet:

`dotnet add package Umbraco.Community.Automate.ClickUp`

## Configuration

This package follows the standard Umbraco Automate provider configuration pattern under `Umbraco:Automate:Providers:ClickUp`.

Use your ClickUp OAuth app credentials in `appsettings.json`:

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

## License

Licensed under the [MIT License](https://github.com/marceldigital/Umbraco-Automate-ClickUp/blob/main/LICENSE).