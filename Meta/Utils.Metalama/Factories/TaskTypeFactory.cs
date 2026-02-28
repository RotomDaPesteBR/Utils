using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace LightningArc.Utils.Metalama;

/// <summary>
/// A compile-time type factory for creating generic instances
/// of <see cref="Task{TResult}"/> and <see cref="ValueTask{TResult}"/>.
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
    /// Gets the non-generic <see cref="Task"/> type.
    /// </summary>
    /// <returns>An <see cref="INamedType"/> representing the <see cref="Task"/> type (equivalent to <c>Task</c>).</returns>
    [CompileTime]
    public static INamedType GetTaskType() => _taskIType;

    /// <summary>
    /// Creates a generic instance of <see cref="Task{TResult}"/> with the specified result type.
    /// </summary>
    /// <param name="type">The result type (<c>TResult</c>) of the <see cref="Task{TResult}"/>.</param>
    /// <returns>An <see cref="INamedType"/> representing <c>Task&lt;type&gt;</c>.</returns>
    [CompileTime]
    public static INamedType GetTaskType(Type type) => _genericTaskIType.MakeGenericInstance(type);

    /// <summary>
    /// Creates a generic instance of <see cref="Task{TResult}"/> with the specified result type.
    /// </summary>
    /// <param name="type">The result type (<c>TResult</c>) of the <see cref="Task{TResult}"/>.</param>
    /// <returns>An <see cref="INamedType"/> representing <c>Task&lt;type&gt;</c>.</returns>
    [CompileTime]
    public static INamedType GetTaskType(IType type) => _genericTaskIType.MakeGenericInstance(type);
    #endregion

    #region GetValueTaskType
    /// <summary>
    /// Gets the non-generic <see cref="ValueTask"/> type.
    /// </summary>
    /// <returns>An <see cref="INamedType"/> representing the <see cref="ValueTask"/> type (equivalent to <c>ValueTask</c>).</returns>
    [CompileTime]
    public static INamedType GetValueTaskType() => _valueTaskIType;

    /// <summary>
    /// Creates a generic instance of <see cref="ValueTask{TResult}"/> with the specified result type.
    /// </summary>
    /// <param name="type">The result type (<c>TResult</c>) of the <see cref="ValueTask{TResult}"/>.</param>
    /// <returns>An <see cref="INamedType"/> representing <c>ValueTask&lt;type&gt;</c>.</returns>
    [CompileTime]
    public static INamedType GetValueTaskType(Type type) => _genericValueTaskIType.MakeGenericInstance(type);

    /// <summary>
    /// Creates a generic instance of <see cref="ValueTask{TResult}"/> with the specified result type.
    /// </summary>
    /// <param name="type">The result type (<c>TResult</c>) of the <see cref="ValueTask{TResult}"/>.</param>
    /// <returns>An <see cref="INamedType"/> representing <c>ValueTask&lt;type&gt;</c>.</returns>
    [CompileTime]
    public static INamedType GetValueTaskType(IType type) => _genericValueTaskIType.MakeGenericInstance(type);
    #endregion
}