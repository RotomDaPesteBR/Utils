# CorsExtensions.cs Implementation Details

**File Path:** `Web/Utils.AspNet.CORS/CORS/CorsExtensions.cs`
**Namespace:** `LightningArc.Utils.AspNet.CORS`

## Overview
This class provides top-level extension methods to register and apply CORS policies globally in the application.

## Code Analysis

### `AddCorsPolicies`
*   **Purpose**: Centralizes the registration of all library-provided CORS policies.
*   **Current Action**: Calls `AddAllowAllPolicy()`.

### `UseCorsPolicies`
*   **Purpose**: Applies the default library policy to the ASP.NET Core request pipeline.
*   **Current Action**: Calls `app.UseCors("AllowAll")`.
*   **Target**: Extends `IApplicationBuilder`, making it compatible with both `WebApplication` and traditional `Startup` classes.
