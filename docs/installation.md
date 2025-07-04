---
layout: default
title: Installation
nav_order: 3
---

# Installation

## Required Dependencies
To use SpatiaLite, you will need to add additional NuGet package dependencies to your project:
- For Windows: `Microsoft.Data.SQLite` and `mod_spatialite`.
- For Linux and macOS: `SQLitePCLRaw.bundle_sqlite3`.

If you are deploying your application on Linux or macOS, it is recommended to install the SpatiaLite library on your operating system:
- Debian/Ubuntu:
  ```bash
  apt-get install libsqlite3-mod-spatialite
  ```
- macOS:
  ```bash
  brew install libspatialite
  ```

## NuGet Packages
The following packages are available:

| Package | Description | Link |
| --- | --- | --- |
| OgcApi.Net | OGC API - Features implementation without specific data providers | <a href="https://www.nuget.org/packages/OgcApi.Net/ "><img src="https://img.shields.io/nuget/v/OgcApi.Net " alt="NuGet"></a> |
| OgcApi.Net.SqlServer | SQL Server features data provider implementation | <a href="https://www.nuget.org/packages/OgcApi.Net.SqlServer/ "><img src="https://img.shields.io/nuget/v/OgcApi.Net.SqlServer " alt="NuGet"></a> |
| OgcApi.Net.PostGis | PostgreSQL/PostGis features data provider implementation | <a href="https://www.nuget.org/packages/OgcApi.Net.PostGis/ "><img src="https://img.shields.io/nuget/v/OgcApi.Net.PostGis " alt="NuGet"></a> |
| OgcApi.Net.SpatiaLite | SQLite/SpatiaLite features data provider implementation | <a href="https://www.nuget.org/packages/OgcApi.Net.SpatiaLite/ "><img src="https://img.shields.io/nuget/v/OgcApi.Net.SpatiaLite " alt="NuGet"></a> |
| OgcApi.Net.MbTiles | MbTiles tiles provider implementation | <a href="https://www.nuget.org/packages/OgcApi.Net.MbTiles/ "><img src="https://img.shields.io/nuget/v/OgcApi.Net.MbTiles " alt="NuGet"></a> |
| OgcApi.Net.Schemas | Schemas standard implementation | <a href="https://www.nuget.org/packages/OgcApi.Net.Schemas/ "><img src="https://img.shields.io/nuget/v/OgcApi.Net.Schemas " alt="NuGet"></a> |

For API configuration, see [API Configuration](configuration.md).