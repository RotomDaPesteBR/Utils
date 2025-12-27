using System.Data.Common;
using System.Reflection;
using Microsoft.Extensions.Logging;
using LightningArc.Utils.Data.Abstractions.Mappers;
using LightningArc.Utils.Data.ADO.Repositories;

namespace LightningArc.Utils.Data.ADO.Factories;

/// <summary>
/// A concrete, generic implementation of IRepositoryFactory for ADO.NET/Dapper repositories.
/// </summary>
public sealed class RepositoryFactory(
    IConnectionFactory connectionFactory,
    IMapper? mapper = null,
    ILoggerFactory? loggerFactory = null
) : IRepositoryFactory
{
    private readonly IConnectionFactory _connectionFactory = connectionFactory;
    private readonly IMapper? _mapper = mapper;
    private readonly ILoggerFactory? _loggerFactory = loggerFactory;

    /// <inheritdoc />
    public TRepository Create<TRepository>()
        where TRepository : IDbRepository<TRepository>
    {
#if NET7_0_OR_GREATER
        return TRepository.Create(
            _connectionFactory,
            _mapper,
            _loggerFactory?.CreateLogger<TRepository>()
        );
#else
        return InvokeCreate<TRepository>(new object?[] { _connectionFactory, _mapper, _loggerFactory?.CreateLogger<TRepository>() });
#endif
    }

    /// <inheritdoc />
    public TRepository Create<TRepository>(IConnectionFactory connectionFactory)
        where TRepository : IDbRepository<TRepository>
    {
#if NET7_0_OR_GREATER
        return TRepository.Create(
            connectionFactory,
            _mapper,
            _loggerFactory?.CreateLogger<TRepository>()
        );
#else
        return InvokeCreate<TRepository>(new object?[] { connectionFactory, _mapper, _loggerFactory?.CreateLogger<TRepository>() });
#endif
    }

    /// <inheritdoc />
    public TRepository Create<TRepository>(DbConnection connection, DbTransaction transaction)
        where TRepository : IDbRepository<TRepository>
    {
#if NET7_0_OR_GREATER
        return TRepository.Create(
            connection,
            transaction,
            _mapper,
            _loggerFactory?.CreateLogger<TRepository>()
        );
#else
        return InvokeCreate<TRepository>(new object?[] { connection, transaction, _mapper, _loggerFactory?.CreateLogger<TRepository>() });
#endif
    }

#if !NET7_0_OR_GREATER
    private TRepository InvokeCreate<TRepository>(object?[] args)
    {
        var type = typeof(TRepository);
        var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Static)
                          .Where(m => m.Name == "Create");

        foreach (var method in methods)
        {
            var parameters = method.GetParameters();
            if (parameters.Length != args.Length) continue;

            bool match = true;
            for (int i = 0; i < parameters.Length; i++)
            {
                if (args[i] != null && !parameters[i].ParameterType.IsAssignableFrom(args[i]!.GetType()))
                {
                    match = false;
                    break;
                }
            }

            if (match) return (TRepository)method.Invoke(null, args)!;
        }
        
        throw new InvalidOperationException($"Could not find a suitable static 'Create' method on type {type.Name}.");
    }
#endif
}
