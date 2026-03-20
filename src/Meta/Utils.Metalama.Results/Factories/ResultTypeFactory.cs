using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using LightningArc.Utils.Results;

namespace LightningArc.Utils.Metalama;

/// <summary>
/// A compile-time type factory for creating instances of 
/// <c>Result</c>, <c>Result&lt;T&gt;</c> types and their corresponding <c>Task</c> 
/// versions (such as <c>Task&lt;Result&gt;</c>).
/// </summary>
[CompileTime]
public static class ResultTypeFactory
{
    #region Types
    private static readonly Type _resultType = typeof(Result);
    private static readonly INamedType _resultIType = NamedTypeFactory.GetType(_resultType);
    #endregion

    #region ITypes
    private static readonly Type _genericResultType = typeof(Result<>);
    private static readonly INamedType _genericResultIType = NamedTypeFactory.GetType(_genericResultType);
    #endregion

    #region GetType
    /// <summary>
    /// Gets the non-generic <c>Result</c> type.
    /// </summary>
    /// <returns>An <see cref="INamedType"/> representing the <c>Result</c> type.</returns>
    [CompileTime]
    public static new INamedType GetType() => _resultIType;

    /// <summary>
    /// Creates a generic instance of the <c>Result&lt;T&gt;</c> type with the specified success type.
    /// </summary>
    /// <param name="type">The success type (<c>T</c>) contained in <c>Result&lt;T&gt;</c>.</param>
    /// <returns>An <see cref="INamedType"/> representing <c>Result&lt;type&gt;</c>.</returns>
    [CompileTime]
    public static INamedType GetType(Type type) => _genericResultIType.MakeGenericInstance(type);

    /// <summary>
    /// Creates a generic instance of the <c>Result&lt;T&gt;</c> type with the specified success type.
    /// </summary>
    /// <param name="type">The success type (<c>T</c>) contained in <c>Result&lt;T&gt;</c>.</param>
    /// <returns>An <see cref="INamedType"/> representing <c>Result&lt;type&gt;</c>.</returns>
    [CompileTime]
    public static INamedType GetType(IType type) => _genericResultIType.MakeGenericInstance(type);
    #endregion

    #region GetTaskType
    /// <summary>
    /// Gets the <see cref="Task{TResult}"/> type where the generic result is the non-generic <c>Result</c>.
    /// </summary>
    /// <returns>An <see cref="INamedType"/> representing the <c>Task&lt;Result&gt;</c> type.</returns>
    [CompileTime]
    public static INamedType GetTaskType() => TaskTypeFactory.GetTaskType(_resultIType);

    /// <summary>
    /// Gets the <see cref="Task{TResult}"/> type where the generic result is the specified <c>Result&lt;T&gt;</c>.
    /// </summary>
    /// <param name="type">The success type (<c>T</c>) contained in <c>Result&lt;T&gt;</c>.</param>
    /// <returns>An <see cref="INamedType"/> representing the <c>Task&lt;Result&lt;type&gt;&gt;</c> type.</returns>
    [CompileTime]
    public static INamedType GetTaskType(Type type) => TaskTypeFactory.GetTaskType(GetType(type));

    /// <summary>
    /// Gets the <see cref="Task{TResult}"/> type where the generic result is the specified <c>Result&lt;T&gt;</c>.
    /// </summary>
    /// <param name="type">The success type (<c>T</c>) contained in <c>Result&lt;T&gt;</c>.</param>
    /// <returns>An <see cref="INamedType"/> representing the <c>Task&lt;Result&lt;type&gt;&gt;</c> type.</returns>
    [CompileTime]
    public static INamedType GetTaskType(IType type) => TaskTypeFactory.GetTaskType(GetType(type));
    #endregion
}