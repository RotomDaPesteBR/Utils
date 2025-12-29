# Usage Guides

This section provides practical guides for using the libraries in the **LightningArc.Utils** ecosystem.

## Core Libraries
Fundamental building blocks for domain logic.

*   **[Utils.Abstractions](Utils.Abstractions.md)**: Common base types and Value Objects (e.g., `Email`).
*   **[Utils.Results](Utils.Results.md)**: The Functional Result Pattern implementation (Error Handling).
*   **[Utils.Json](Utils.Json.md)**: JSON serialization support for custom types.

## Data Access
Abstractions and implementations for database interactions and object mapping.

*   **[Utils.Data.Abstractions](Utils.Data.Abstractions.md)**: Repository, Mapper and Unit of Work interfaces.
*   **[Utils.Data.ADO](Utils.Data.ADO.md)**: ADO.NET / Dapper base repositories and connection factories.
*   **[Utils.Data.EntityFramework](Utils.Data.EntityFramework.md)**: Entity Framework Core base repositories.
*   **[Utils.Mappers](Utils.Mappers.md)**: Adapters for Mapster and AutoMapper.

## Web Extensions
Extensions for ASP.NET Core applications.

*   **[Utils.AspNet.Results](Utils.AspNet.Results.md)**: Integration of the Result pattern with API endpoints (`IResult`).
*   **[Utils.AspNet.CORS](Utils.AspNet.CORS.md)**: CORS policy helpers.
*   **[Utils.AspNet.OpenAPI](Utils.AspNet.OpenAPI.md)**: Swagger/OpenAPI customization.

## Meta-Programming
Tools for Aspect-Oriented Programming (AOP).

*   **[Utils.Metalama](Utils.Metalama.md)**: Extensions and Factories for Metalama aspects.
