## OpenRedding.Api
This projects serves as the web request interface and entry point into the application. As an ASP.NET Core project, its sole responsibility is to wire up application dependencies and offload requests to the core for processing.

Taking the thin controller approach, any additional feature enhancements to this project should not introduce complex logic within any controller. Simply log the request, and forward to the core for request processing. Using MediatR, all requests should send either a query, or command, to be received in the core where the majority of feature development should be housed.

## Policies
- **Keep controllers thin** - no business logic should written in this layer
- **Wire up dependencies accordingly** - if adding a NuGet reference, consider its use case and whether it should be better off placed in either the core, or infrastructure, projects
- **Wrap middleware with extensions** - do not add any `app.Use()` middleware dependencies, place them in a corresponding request delegate receiving middleware file and wrap with an extension (see the exception handler for examples)