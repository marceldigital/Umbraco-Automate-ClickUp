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
- Structure prepared for upcoming connection, trigger, and action implementations.

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

The current setup phase focuses on provider/package foundations. Functional ClickUp automation features are implemented in follow-up iterations.

## Contributing

Contributions to this package are most welcome! Please read the [Contributing Guidelines](CONTRIBUTING.md).

## License

Licensed under the [MIT License](../LICENSE).