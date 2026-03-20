using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace LightningArc.Utils.Metalama;

/// <summary>
/// Exposes methods that return instances of the <see cref="INamedType"/> interface.
/// It is a wrapper over <see cref="TypeFactory"/>, converting the result to <see cref="INamedType"/>.
/// </summary>
[CompileTime]
public static class NamedTypeFactory
{
    #region GetType
    /// <summary>
    /// Gets an <see cref="INamedType" /> given a reflection <see cref="Type" />.
    /// </summary>
    /// <param name="type">The reflection <see cref="Type" />.</param>
    /// <returns>
    /// An <see cref="INamedType" /> that represents the type, or <see langword="null" /> if it's a type that
    /// cannot be represented as <see cref="INamedType" /> (such as an anonymous type or a pointer),
    /// or if the type is not found during compilation.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the <see cref="TypeFactory" /> is not available in the current context.
    /// </exception>
    [CompileTime]
    public static INamedType GetType(Type type) => (INamedType)TypeFactory.GetType(type);

    /// <summary>
    /// Gets an <see cref="INamedType" /> that represents a given <see cref="SpecialType" />.
    /// </summary>
    /// <param name="type">The <see cref="SpecialType" /> (special type) to be retrieved.</param>
    /// <returns>An <see cref="INamedType" /> representing the special <paramref name="type" />.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the <see cref="TypeFactory" /> is not available in the current context.
    /// </exception>
    [CompileTime]
    public static INamedType GetType(SpecialType type) => TypeFactory.GetType(type);

    /// <summary>
    /// Gets an <see cref="INamedType" /> based on its full name, as used in reflection.
    /// </summary>
    /// <remarks>
    /// <para>For nested types, this means using '+', for example, to get <c>System.Environment.SpecialFolder</c>,
    /// use <c>System.Environment+SpecialFolder</c>.</para>
    /// <para>For generic type definitions, this requires using '`', for example, to get <c>List&lt;T&gt;</c>, use
    /// <c>System.Collections.Generic.List`1</c>.</para>
    /// <para>Constructed generic types (e.g., <c>List&lt;int&gt;</c>) are not supported. For these, use
    /// <see cref="GenericExtensions.WithTypeArguments(INamedType, IType[])"/>.</para>
    /// </remarks>
    /// <param name="typeName">The full name of the type (including namespace and, for nested types, the '+' character).</param>
    /// <returns>The <see cref="INamedType" /> that corresponds to the <paramref name="typeName" />.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the type is not found or if the <see cref="TypeFactory" /> is not available in the current context.
    /// </exception>
    [CompileTime]
    public static INamedType GetType(string typeName) => TypeFactory.GetType(typeName);
    #endregion

    #region GetTypeOrDefault
    /// <summary>
    /// Gets an <see cref="INamedType"/> based on its full name, as used in reflection,
    /// returning a default value if the type is not found.
    /// </summary>
    /// <remarks>
    /// <para>The naming rules for <paramref name="typeName"/> are the same as those for <see cref="GetType(string)"/>.</para>
    /// <para>This overload returns <paramref name="defaultType"/> instead of throwing an exception if the type is not found.</para>
    /// </remarks>
    /// <param name="typeName">The full name of the type (including namespace and, for nested types, the '+' character).</param>
    /// <param name="defaultType">The <see cref="INamedType"/> to be returned if the type is not found. The default is <see langword="null"/>.</param>
    /// <returns>The <see cref="INamedType"/> that corresponds to the <paramref name="typeName"/>, or <paramref name="defaultType"/> if not found.</returns>

    [CompileTime]
    public static INamedType? GetTypeOrDefault(string typeName, INamedType? defaultType = null)
    {
        try
        {
            return TypeFactory.GetType(typeName);
        }
        catch
        {
            return defaultType;
        }
    }
    #endregion
}
