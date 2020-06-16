## OpenRedding.Client
This projects serves as the frontend UI for the application written using Blazor WebAssembly. As the public face of the project, it utilizes Bootstrap for styling and Fluxor as the redux provider.

Heavily relying on Fluxor, all feature development should adhere to redux standards with appropriate actions and workflows placed under the proper state features.

## Policies
- **No injected component services** - defer to issuing actions and appropriate state changes
- **No flow is too small for redux** - when implementing a new workflow, start by defining any relevant actions and state reducers
- **Utilize flex when possible** - while reliant on Bootstrap, defer to flex box utilities rather than inline styles
- **Abstract common components** - if you find yourself re-writing common component markup, consider externalizing said markup for common reuse


## Workflows
When introducing a new workflow, there should be a pipeline of changes taking place (redux):
1. Define any relevant actions, including success/failure actions
2. Define state reducers and any state persistence on computed change
3. Define effects for appropriate actions, including success/failure actions
4. Defer to services within effects, rather than business logic within the effect itself
