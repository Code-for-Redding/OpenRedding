## OpenRedding.Infrastructure
This projects serves as common solution service implementation layer. All implemented interfaces, common web services, persistence concerns, file system interactions, etc. should be placed in this project.

## Policies
- **Define services within inner layers** - infrastructure services should implement contracts in either core, or domain, projects
- **Wire up dependencies through extensions** - should a new service be added, add the service to the `StartupExtensions` file with the proper service lifetime (default to `scoped` if you're unsure)
- **Apply migrations within persistence** - do not run migrations outside the scope of this project, as all migration and persistence logic should be contained here