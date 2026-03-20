using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace LightningArc.Utils.Metalama;

/// <summary>
/// A compile-time type factory for creating generic instances
/// of common collections such as <see cref="IEnumerable{T}"/> and <see cref="List{T}"/>.
/// </summary>
[CompileTime]
public static class CollectionTypeFactory
{
    #region Types
    private static readonly Type _iEnumerableType = typeof(IEnumerable<>);
    private static readonly INamedType _iEnumerableIType = NamedTypeFactory.GetType(_iEnumerableType);
    #endregion

    #region ITypes
    private static readonly Type _listType = typeof(List<>);
    private static readonly INamedType _listIType = NamedTypeFactory.GetType(_listType);
    #endregion

    #region GetIEnumerableType
    /// <summary>
    /// Creates a generic instance of <see cref="IEnumerable{T}"/> with the specified element type.
    /// </summary>
    /// <param name="type">The element type (<c>T</c>) of the <see cref="IEnumerable{T}"/>.</param>
    /// <returns>An <see cref="INamedType"/> representing <c>IEnumerable&lt;type&gt;</c>.</returns>

    [CompileTime] 
    public static INamedType GetIEnumerableType(Type type) => _iEnumerableIType.MakeGenericInstance(type);

    /// <summary>
    /// Creates a generic instance of <see cref="IEnumerable{T}"/> with the specified element type.
    /// </summary>
    /// <param name="type">The element type (<c>T</c>) of the <see cref="IEnumerable{T}"/>.</param>
    /// <returns>An <see cref="INamedType"/> representing <c>IEnumerable&lt;type&gt;</c>.</returns>

    [CompileTime] 
    public static INamedType GetIEnumerableType(IType type) => _iEnumerableIType.MakeGenericInstance(type);
    #endregion

    #region GetListType
    /// <summary>
    /// Creates a generic instance of <see cref="List{T}"/> with the specified element type.
    /// </summary>
    /// <param name="type">The element type (<c>T</c>) of the <see cref="List{T}"/>.</param>
    /// <returns>An <see cref="INamedType"/> representing <c>List&lt;type&gt;</c>.</returns>
    [CompileTime]
    public static INamedType GetListType(Type type) => _listIType.MakeGenericInstance(type);

    /// <summary>
    /// Creates a generic instance of <see cref="List{T}"/> with the specified element type.
    /// </summary>
    /// <param name="type">The element type (<c>T</c>) of the <see cref="List{T}"/>.</param>
    /// <returns>An <see cref="INamedType"/> representing <c>List&lt;type&gt;</c>.</returns>

    [CompileTime]
    public static INamedType GetListType(IType type) => _listIType.MakeGenericInstance(type);
    #endregion
}
