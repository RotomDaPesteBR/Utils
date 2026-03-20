using System.Security.Claims;
using LightningArc.Utils.Results;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace LightningArc.Utils.Metalama;

/// <summary>
/// Provides extension methods for the Metalama <see cref="IType"/> type,
/// focused on inspecting method return types, especially in the context of
/// asynchronous operations (<see cref="Task"/>) and result types based on <see cref="Result"/>.
/// </summary>
/// <remarks>
/// This class is used in Metalama Aspects to determine the nature and serialization
/// of parameters and return types at compile-time.
/// </remarks>
[CompileTime]
public static class ITypeExtensions
{
    /// <summary>
    /// Provides a list of non-serializable <see cref="IType"/> types,
    /// used in the <see cref="IsSerializable(IType, Type[])"/> extension method.
    /// </summary>
    [CompileTime]
    public static readonly Type[] NotSerializableTypes =
    [
        typeof(CancellationToken),
        typeof(Stream),
        typeof(ClaimsPrincipal),
    ];

    /// <summary>
    /// Determines if the current type is a <see cref="Task"/> or <see cref="Task{TResult}"/>.
    /// </summary>
    /// <param name="type">The type to be checked.</param>
    /// <returns><c>true</c> if the type is a Task; otherwise, <c>false</c>.</returns>
    [CompileTime]
    public static bool IsTask(this IType type)
    {
        bool isTask = type.IsConvertibleTo(typeof(Task));

        return isTask;
    }

    /// <summary>
    /// Gets the actual return type (the contained type) if the input type is a <see cref="Task{TResult}"/>.
    /// Otherwise, returns the input type itself.
    /// </summary>
    /// <param name="type">The type to be checked.</param>
    /// <remarks>
    /// This method is crucial for abstracting the complexity of asynchronous methods,
    /// allowing inspection of the data type that will actually be returned after the Task completes.
    /// </remarks>
    /// <returns>The Task result type (TResult) if it is a Task&lt;TResult&gt;; otherwise, the original type.</returns>
    [CompileTime]
    public static IType UnwrapType(this IType type) =>
        type.IsTask() ? type.GetAsyncInfo().ResultType : type;

    /// <summary>
    /// Determines if the *actual* return type (after unwrapping a possible Task) is convertible to the base <see cref="Result"/> type.
    /// </summary>
    /// <param name="type">The type to be checked.</param>
    /// <returns><c>true</c> if the type is a non-generic <see cref="Result"/> or a <see cref="Result{TValue}"/>; otherwise, <c>false</c>.</returns>
    [CompileTime]
    public static bool IsResult(this IType type) =>
        type.UnwrapType().IsConvertibleTo(typeof(Result));

    /// <summary>
    /// Determines if the *actual* return type (after unwrapping a possible Task) is a generic <see cref="Result{TValue}"/> type definition.
    /// </summary>
    /// <param name="type">The type to be checked.</param>
    /// <returns><c>true</c> if the type is a <see cref="Result{TValue}"/>; otherwise, <c>false</c>.</returns>
    [CompileTime]
    public static bool IsValueResult(this IType type) =>
        type.UnwrapType().IsConvertibleTo(typeof(Result<>), ConversionKind.TypeDefinition);

    /// <summary>
    /// Determines if the return type is a type that is generally considered serializable or that should be
    /// inspected by aspects.
    /// </summary>
    /// <remarks>
    /// Checks if the type is not in the list of types that are injected by the framework
    /// (e.g., <see cref="CancellationToken"/>),
    /// as these generally should not be serialized or inspected for business logic.
    /// </remarks>
    /// <param name="notSerializableTypes">A list of types to be recognized as non-serializable.
    /// By default, uses the <see cref="NotSerializableTypes"/> static property.</param>
    /// <param name="type">The type to be checked.</param>
    /// <returns><c>true</c> if the type is not in the list of ignored types; otherwise, <c>false</c>.</returns>
    [CompileTime]
    public static bool IsSerializable(this IType type, Type[]? notSerializableTypes = null) =>
        !(notSerializableTypes ?? NotSerializableTypes).Any(t => type.IsConvertibleTo(t));
}
