## OpenRedding.Core
This projects serves as the orchestrator of all application services and request processing. Vertical feature slice architecture is preferred with inversion of all non-critical dependencies required.

Heavily relying on MediatR, any additional commands, or queries, will be defined and implemented within this layer. Should an additional infrastructure service be required, define its contract here (or in domain) with the implementation within the infrastructure project.

## Policies
- **No (low-level) dependencies** - the only dependency should be domain (via shared)
- **Add to the appropriate feature slice** - utilize the proper feature, or default to adding to the common slice
- **Defer to infrastructure** - aforementioned, utilize infrastructure services wherever possible


## Workflows
When introducing a new workflow, there should be a pipeline of changes taking place:
1. Designate the interaction as either a query, or command
2. Define an `IRequest<TResult>` implementation within the appropriate feature folder
3. All commands *should* have validators defined from `AbstractValidator<TRequest>` implementations within the appropriate workflow directory
4. Define an `IRequestHandler<TRequest, TResult>` implementation, request any required services from the DI container where appropriate
