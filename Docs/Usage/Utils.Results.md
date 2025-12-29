# Utils.Results Usage Guide

The **Utils.Results** library implements the **Result Pattern**, a functional approach to error handling that replaces exceptions for control flow.

## Core Concepts

*   **Result**: Represents the outcome of an operation (Success or Failure).
*   **Result\<T>**: Represents a success with a value, or a failure.
*   **Error**: A structured object describing why an operation failed.

## Returning Results

### Basic Success
Return a value directly. The library implicitly converts it to a Success Result.
```csharp
public Result<int> Calculate(int a, int b)
{
    return a + b; // Implicit conversion to Result<int> (Success)
}
```

### Basic Failure
Return an `Error` object. The library implicitly converts it to a Failure Result.
```csharp
public Result<User> GetUser(int id)
{
    if (id < 0)
    {
        return Errors.General.InvalidId; // Implicit conversion
    }
    // ...
}
```

### Specific Success Types
You can be explicit about the type of success (useful for REST APIs).
```csharp
public Result<User> CreateUser(User user)
{
    // ... logic ...
    return Result.Created(user); // Returns HTTP 201 equivalent
}
```

## Handling Results

### Checking Status
```csharp
var result = service.DoWork();

if (result.IsFailure)
{
    // Handle error
    Console.WriteLine(result.Error.Message);
    return;
}

// Access value safely
var value = result.Value;
```

### Pattern Matching (`Match`)
Use `Match` to handle both outcomes cleanly in a single expression.

```csharp
string message = result.Match(
    success: value => $"Success: {value}",
    failure: error => $"Error: {error.Message}"
);
```

### Chaining (`Bind`)
Use `Bind` to chain operations where the next step depends on the success of the previous one. This is known as "Railroad Oriented Programming".

```csharp
Result<Order> result = GetUser(userId)
    .Bind(user => ValidateUser(user))
    .Bind(user => CreateOrder(user));
    
// If any step fails, the chain stops and returns the error.
```

### Async Support (`TaskResult`)
The library provides `TaskResult<T>` to simplify async workflows. You can await it directly.

```csharp
public async TaskResult<User> GetUserAsync(int id)
{
    // ... async logic ...
    return new User();
}

// Usage
var result = await GetUserAsync(1);
```

---

## Implementation Details

For deep dives into the source code, see:
*   [Utils.Results Implementation](../Implementation/Core/Utils.Results/README.md)
*   [Result.cs](../Implementation/Core/Utils.Results/Results/Result.md)
*   [Error.cs](../Implementation/Core/Utils.Results/Results/Error.md)
*   [Success.cs](../Implementation/Core/Utils.Results/Results/Success.md)