using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace LightningArc.Utils.Metalama;

/// <summary>
/// Uma fábrica de tipos em tempo de compilação para criar instâncias genéricas
/// de coleções comuns como <see cref="IEnumerable{T}"/> e <see cref="List{T}"/>.
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
    /// Cria uma instância genérica de <see cref="IEnumerable{T}"/> com o tipo de elemento especificado.
    /// </summary>
    /// <param name="type">O tipo de elemento (<c>T</c>) da <see cref="IEnumerable{T}"/>.</param>
    /// <returns>Um <see cref="INamedType"/> que representa <c>IEnumerable&lt;type&gt;</c>.</returns>

    [CompileTime] 
    public static INamedType GetIEnumerableType(Type type) => _iEnumerableIType.MakeGenericInstance(type);

    /// <summary>
    /// Cria uma instância genérica de <see cref="IEnumerable{T}"/> com o tipo de elemento especificado.
    /// </summary>
    /// <param name="type">O tipo de elemento (<c>T</c>) da <see cref="IEnumerable{T}"/>.</param>
    /// <returns>Um <see cref="INamedType"/> que representa <c>IEnumerable&lt;type&gt;</c>.</returns>

    [CompileTime] 
    public static INamedType GetIEnumerableType(IType type) => _iEnumerableIType.MakeGenericInstance(type);
    #endregion

    #region GetListType
    /// <summary>
    /// Cria uma instância genérica de <see cref="List{T}"/> com o tipo de elemento especificado.
    /// </summary>
    /// <param name="type">O tipo de elemento (<c>T</c>) da <see cref="List{T}"/>.</param>
    /// <returns>Um <see cref="INamedType"/> que representa <c>List&lt;type&gt;</c>.</returns>
    [CompileTime]
    public static INamedType GetListType(Type type) => _listIType.MakeGenericInstance(type);

    /// <summary>
    /// Cria uma instância genérica de <see cref="List{T}"/> com o tipo de elemento especificado.
    /// </summary>
    /// <param name="type">O tipo de elemento (<c>T</c>) da <see cref="List{T}"/>.</param>
    /// <returns>Um <see cref="INamedType"/> que representa <c>List&lt;type&gt;</c>.</returns>

    [CompileTime]
    public static INamedType GetListType(IType type) => _listIType.MakeGenericInstance(type);
    #endregion
}
