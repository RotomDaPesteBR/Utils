using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace LightningArc.Utils.Metalama;

/// <summary>
/// Uma fábrica de tipos em tempo de compilação para criar instâncias genéricas
/// de <see cref="Task{TResult}"/> e <see cref="ValueTask{TResult}"/>.
/// </summary>
[CompileTime]
public static class TaskTypeFactory
{
    #region Types
    private static readonly Type _taskType = typeof(Task);
    private static readonly Type _genericTaskType = typeof(Task<>);
    private static readonly Type _valueTaskType = typeof(ValueTask);
    private static readonly Type _genericValueTaskType = typeof(ValueTask<>);
    #endregion

    #region ITypes
    private static readonly INamedType _taskIType = NamedTypeFactory.GetType(_taskType);
    private static readonly INamedType _genericTaskIType = NamedTypeFactory.GetType(_genericTaskType);
    private static readonly INamedType _valueTaskIType = NamedTypeFactory.GetType(_valueTaskType);
    private static readonly INamedType _genericValueTaskIType = NamedTypeFactory.GetType(_genericValueTaskType);
    #endregion

    #region GetTaskType
    /// <summary>
    /// Obtém o tipo de <see cref="Task"/> não genérico.
    /// </summary>
    /// <returns>Um <see cref="INamedType"/> que representa o tipo <see cref="Task"/> (equivalente a <c>Task</c>).</returns>
    public static INamedType GetTaskType() => _taskIType;

    /// <summary>
    /// Cria uma instância genérica de <see cref="Task{TResult}"/> com o tipo de resultado especificado.
    /// </summary>
    /// <param name="type">O tipo de resultado (<c>TResult</c>) da <see cref="Task{TResult}"/>.</param>
    /// <returns>Um <see cref="INamedType"/> que representa <c>Task&lt;type&gt;</c>.</returns>
    public static INamedType GetTaskType(Type type) => _genericTaskIType.MakeGenericInstance(type);

    /// <summary>
    /// Cria uma instância genérica de <see cref="Task{TResult}"/> com o tipo de resultado especificado.
    /// </summary>
    /// <param name="type">O tipo de resultado (<c>TResult</c>) da <see cref="Task{TResult}"/>.</param>
    /// <returns>Um <see cref="INamedType"/> que representa <c>Task&lt;type&gt;</c>.</returns>
    public static INamedType GetTaskType(IType type) => _genericTaskIType.MakeGenericInstance(type);
    #endregion

    #region GetValueTaskType
    /// <summary>
    /// Obtém o tipo de <see cref="ValueTask"/> não genérico.
    /// </summary>
    /// <returns>Um <see cref="INamedType"/> que representa o tipo <see cref="ValueTask"/> (equivalente a <c>ValueTask</c>).</returns>
    public static INamedType GetValueTaskType() => _valueTaskIType;

    /// <summary>
    /// Cria uma instância genérica de <see cref="ValueTask{TResult}"/> com o tipo de resultado especificado.
    /// </summary>
    /// <param name="type">O tipo de resultado (<c>TResult</c>) da <see cref="ValueTask{TResult}"/>.</param>
    /// <returns>Um <see cref="INamedType"/> que representa <c>ValueTask&lt;type&gt;</c>.</returns>
    public static INamedType GetValueTaskType(Type type) => _genericValueTaskIType.MakeGenericInstance(type);

    /// <summary>
    /// Cria uma instância genérica de <see cref="ValueTask{TResult}"/> com o tipo de resultado especificado.
    /// </summary>
    /// <param name="type">O tipo de resultado (<c>TResult</c>) da <see cref="ValueTask{TResult}"/>.</param>
    /// <returns>Um <see cref="INamedType"/> que representa <c>ValueTask&lt;type&gt;</c>.</returns>
    public static INamedType GetValueTaskType(IType type) => _genericValueTaskIType.MakeGenericInstance(type);
    #endregion
}
