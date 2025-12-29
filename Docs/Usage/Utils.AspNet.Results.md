# Utils.AspNet.Results Usage Guide

The **Utils.AspNet.Results** library integrates the **Utils.Results** pattern with ASP.NET Core, automatically translating domain results into HTTP responses.

## Setup

In your `Program.cs`:

```csharp
using LightningArc.Utils.AspNet;

var builder = WebApplication.CreateBuilder(args);

// Register the services
builder.Services.AddEndpointResults(
    defaultCulture: "en-US",
    configureMappings: (success, errors) =>
    {
        // Optional: Map specific domain errors to HTTP codes
        errors.Map<MyNotFoundError>(HttpStatusCode.NotFound);
    }
);

var app = builder.Build();

// Optional: Generate error documentation on startup
if (app.Environment.IsDevelopment())
{
    app.OutputErrorsList();
}

app.Run();
```

## Usage in Endpoints

### Minimal APIs

Return `EndpointResult<T>` instead of `IResult` or `Results.Ok()`.

```csharp
app.MapGet("/users/{id}", (int id, UserService service) =>
{
    // Implicit conversion from Result<User> to EndpointResult<User>
    // Handles 200 OK (with data) or 404/400/500 (Problem Details) automatically.
    return service.GetUser(id); 
});

app.MapPost("/users", (UserDto dto, UserService service) =>
{
    // Implicit conversion from Result (No content)
    // Could return 201 Created or 400 Bad Request
    return service.CreateUser(dto);
});
```

### Controllers

Similar to Minimal APIs, change the return type of your actions.

```csharp
[HttpGet("{id}")]
public EndpointResult<User> Get(int id)
{
    return _service.GetUser(id);
}
```

## Automatic Mapping

| Domain Result | HTTP Response |
| :--- | :--- |
| `Result.Success(val)` | **200 OK** (Body: `val`) |
| `Result.Created(val)` | **201 Created** (Body: `val`) |
| `Result.NoContent()` | **204 No Content** |
| `Error.NotFound` | **404 Not Found** (Problem Details) |
| `Error.Validation` | **400 Bad Request** (Problem Details with fields) |
| `Error.Unexpected` | **500 Internal Server Error** |

---

## Implementation Details

For deep dives into the source code, see:
*   [Utils.AspNet.Results Implementation](../Implementation/Web/Utils.AspNet.Results/README.md)
*   [EndpointResult.cs](../Implementation/Web/Utils.AspNet.Results/Results/EndpointResult.md)
*   [DependencyInjection.cs](../Implementation/Web/Utils.AspNet.Results/DependencyInjection.md)