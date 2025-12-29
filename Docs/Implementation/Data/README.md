# Data Layer Implementation

The Data layer provides patterns and tools for database access.

## Projects

*   **[Utils.Data.Abstractions](Utils.Data.Abstractions/README.md)**: Core interfaces (Unit of Work, Repository, Mapper) and base classes.
*   **ADO/**: Projects related to ADO.NET and Dapper.
    *   `Utils.Data.ADO`: Base implementations.
    *   `Utils.Data.ADO.Oracle`: Oracle driver integration.
    *   `Utils.Data.ADO.SqlServer`: SQL Server driver integration.
*   **EF/**: Projects related to Entity Framework Core.
    *   `Utils.Data.EntityFramework`: Generic base for EF.
*   **Mappers/**: Object mapping adapters.
    *   `Utils.Mappers.AutoMapper`: Adapter for AutoMapper library.
    *   `Utils.Mappers.Mapster`: Adapter for Mapster library.
