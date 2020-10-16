# Open Redding
### An public data portal for the city of Redding, built for the people, by the people

[![Build Status](https://joeymckenzie.visualstudio.com/Open%20Redding/_apis/build/status/JoeyMckenzie.OpenRedding?branchName=master)](https://joeymckenzie.visualstudio.com/Open%20Redding/_build/latest?definitionId=8&branchName=master)

## What is Open Redding?
Open Redding is a portal for various data pertaining to the city of Redding. The project seeks to aggregate and provide a centralize any and all datasets pertaining to various categories of publicly available data. Examples include: public salary information distributed by Transparent California, zoning data provided by the Redding ArcGIS team, budget and financial information distributed by the city, etc. Our goal is to provide the denizens of our city and online hub to view, analyze, and explore the publicly available datasets in one place to help them stay informed.

## Getting Started
### Project Structure
Open Redding is written entirely in C# using .NET Core and hosted on Azure. The solution structure consists of six different projects:
- **OpenRedding.Api** - ASP.NET Core project for handling web requests
- **OpenRedding.Client** - Blazor WebAssembly frontend UI project
- **OpenRedding.Core** - .NET Core standard library handling all application workflows and the majority of business logic
- **OpenRedding.Domain** - .NET Core standard library containing all POCOs and high level domain objects (core of the [onion](https://www.c-sharpcorner.com/article/onion-architecture-in-asp-net-core-mvc/))
- **OpenRedding.Infrastructure** - .NET Core app handling implemented services, persistence, and general application infrastructure concerns
- **OpenRedding.Shared** - .NET Core standard library for all cross-projects referenced items (constants, parameter validation, etc.)
- **_build** - a [Nuke](https://nuke.build/)]-based project to handle build pipelines mimicking the production pipeline before we deploy release builds 

### Bootstrapping
To get started, fork and clone the repository. For local API development, data is seeded at application startup using SQL Server. If you prefer to use a local version of SQL Server (Express, for example), you'll need to provide the connection string via [user secret](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows), or if you're using `docker-compose`, define a `ConnectionString` environment variable within a `.env` file at the root of the project (do **not** commit this file, it is ignored by default).

To seed application data, simply add a `seed` command line argument at startup, i.e. `dotnet run seed`, when firing up the API project. 

For development of the UI, I recommend using `docker-compose` with the local compose file at the root of the project. If you have made any changes to any non-UI project, build the container with `docker-compose -f "docker-compose.yml" build` followed by a `docker-compose -f "docker-compose.yml" up` to fire up both a local container instance of the API with a SQL Server instance as well.

Once you have confirmed your containers have been built and are running, simply fire up the client project (should already be pointing to the API for client requests).

## Contributing
As the project is in an early alpha stage, all work (currently) is being pushed directly to `master` until a clean stable build is available. Going forward, all feature development will be done on a `dev` branch, with corresponding `release` branches to follow. See individual project `README.md`s for style guides and project architectures. 