using LightningArc.Utils.Results;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace LightningArc.Utils.Metalama;

/// <summary>
/// Provides extension methods for the Metalama <see cref="IType"/> type,
/// specifically focused on inspecting <see cref="Result"/> types.
/// </summary>
[CompileTime]
public static class ResultTypeExtensions
{
    /// <summary>
    /// Determines if the *actual* return type (after unwrapping a possible Task) is convertible to the base <see cref="Result"/> type.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns><c>true</c> if the type is a non-generic <see cref="Result"/> or a <see cref="Result{TValue}"/>; otherwise, <c>false</c>.</returns>
    [CompileTime]
    public static bool IsResult(this IType type) =>
        type.UnwrapType().IsConvertibleTo(typeof(Result));

    /// <summary>
    /// Determines if the *actual* return type (after unwrapping a possible Task) is a generic type definition <see cref="Result{TValue}"/>.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns><c>true</c> if the type is a <see cref="Result{TValue}"/>; otherwise, <c>false</c>.</returns>
    [CompileTime]
    public static bool IsValueResult(this IType type) =>
        type.UnwrapType().IsConvertibleTo(typeof(Result<>), ConversionKind.TypeDefinition);
}