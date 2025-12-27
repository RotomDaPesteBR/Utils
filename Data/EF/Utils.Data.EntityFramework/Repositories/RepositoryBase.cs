using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using LightningArc.Utils.Data.Abstractions.Mappers;

namespace LightningArc.Utils.Data.EntityFramework.Repositories;

/// <summary>
/// Provides the basic infrastructure for data access operations using Entity Framework Core.
/// </summary>
/// <typeparam name="TContext">The type of the <see cref="DbContext"/>.</typeparam>
public abstract class RepositoryBase<TContext>
    where TContext : DbContext
{
    /// <summary>
    /// The database context instance.
    /// </summary>
    protected readonly TContext Context;

    /// <summary>
    /// The logger instance for recording diagnostic and error information.
    /// </summary>
    protected readonly ILogger? Logger;

    /// <summary>
    /// The mapper used for transforming data between models and entities.
    /// </summary>
    protected readonly IMapper? Mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="RepositoryBase{TContext}"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="mapper">An optional mapper for data transformation.</param>
    /// <param name="logger">An optional logger instance.</param>
    protected RepositoryBase(TContext context, IMapper? mapper = null, ILogger? logger = null)
    {
        Context = context;
        Mapper = mapper;
        Logger = logger;
    }
}

/// <summary>
/// Generic base for Entity Framework Core repositories, supporting mapping between models and entities.
/// </summary>
/// <typeparam name="TContext">The type of the <see cref="DbContext"/>.</typeparam>
/// <typeparam name="TEntity">The type of the database entity.</typeparam>
public abstract class RepositoryBase<TContext, TEntity> : RepositoryBase<TContext>
    where TContext : DbContext
    where TEntity : class
{
    /// <summary>
    /// The <see cref="DbSet{TEntity}"/> for the current entity.
    /// </summary>
    protected readonly DbSet<TEntity> DbSet;

    /// <summary>
    /// Initializes a new instance of the <see cref="RepositoryBase{TContext, TEntity}"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="mapper">An optional mapper for data transformation.</param>
    /// <param name="logger">An optional logger instance.</param>
    protected RepositoryBase(TContext context, IMapper? mapper = null, ILogger? logger = null)
        : base(context, mapper, logger)
    {
        DbSet = context.Set<TEntity>();
    }
}

/// <summary>
/// Generic base for Entity Framework Core repositories, supporting mapping between database entities and result models (e.g., DTOs).
/// </summary>
/// <typeparam name="TContext">The type of the <see cref="DbContext"/>.</typeparam>
/// <typeparam name="TEntity">The type of the database entity (source).</typeparam>
/// <typeparam name="TResult">The type of the result model (destination).</typeparam>
public abstract class RepositoryBase<TContext, TEntity, TResult> : RepositoryBase<TContext, TEntity>
    where TContext : DbContext
    where TEntity : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RepositoryBase{TContext, TEntity, TResult}"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="mapper">An optional mapper for data transformation.</param>
    /// <param name="logger">An optional logger instance.</param>
    protected RepositoryBase(TContext context, IMapper? mapper = null, ILogger? logger = null)
        : base(context, mapper, logger)
    {
    }
}
