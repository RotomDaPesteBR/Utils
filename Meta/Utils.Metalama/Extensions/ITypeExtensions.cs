using LightningArc.Utils.Metalama;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Engine.Extensibility;
using System.Security.Claims;

// [assembly: ExportExtension(typeof(ITypeExtensions), ExtensionKinds.None)]

namespace LightningArc.Utils.Metalama;

/// <summary>
/// Provides extension methods for the Metalama <see cref="IType"/> type,
/// focused on method return type inspection and serialization.
/// </summary>
/// <remarks>
/// This class is used in Metalama Aspects to determine the nature and serialization
/// capability of parameters and return types at compile time.
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
    /// <param name="type">The type to check.</param>
    /// <returns><c>true</c> if the type is a Task; otherwise, <c>false</c>.</returns>
    [CompileTime]
    public static bool IsTask(this IType type)
    {
        bool isTask = type.IsConvertibleTo(typeof(Task));

        return isTask;
    }

    /// <summary>
    /// Gets the actual return type (the wrapped type) if the input type is a <see cref="Task{TResult}"/>.
    /// Otherwise, returns the input type itself.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <remarks>
    /// This method is crucial for abstracting the complexity of asynchronous methods,
    /// allowing inspection of the data type that will actually be returned after the Task completes.
    /// </remarks>
    /// <returns>The Task's result type (TResult) if it is a Task&lt;TResult&gt;; otherwise, the original type.</returns>
    [CompileTime]
    public static IType UnwrapType(this IType type) =>
        type.IsTask() ? type.GetAsyncInfo().ResultType : type;

    /// <summary>
    /// Determines if the return type is a type generally considered serializable or one that should be
    /// inspected by aspects.
    /// </summary>
    /// <remarks>
    /// Checks if the type is not in the list of types injected by the framework
    /// (e.g., <see cref="CancellationToken"/>),
    /// as these should generally not be serialized or inspected for business logic.
    /// </remarks>
    /// <param name="notSerializableTypes">A list of types to be recognized as non-serializable.
    /// Defaults to using the static property <see cref="NotSerializableTypes"/>.</param>
    /// <param name="type">The type to check.</param>
    /// <returns><c>true</c> if the type is not in the ignored types list; otherwise, <c>false</c>.</returns>
    [CompileTime]
    public static bool IsSerializable(this IType type, Type[]? notSerializableTypes = null) =>
        !(notSerializableTypes ?? NotSerializableTypes).Any(t => type.IsConvertibleTo(t));
}
