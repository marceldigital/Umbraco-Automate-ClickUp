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

The initial release work focuses on provider setup and package baseline alignment. Trigger and action implementations are planned for subsequent iterations.

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