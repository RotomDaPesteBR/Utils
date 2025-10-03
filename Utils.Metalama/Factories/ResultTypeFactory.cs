using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using LightningArc.Utils.Results;

namespace LightningArc.Utils.Metalama;

/// <summary>
/// Uma fábrica de tipos em tempo de compilação para criar instâncias de 
/// tipos <c>Result</c>, <c>Result&lt;T&gt;</c> e suas versões <c>Task</c> 
/// correspondentes (como <c>Task&lt;Result&gt;</c>).
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
    /// Obtém o tipo <c>Result</c> não genérico.
    /// </summary>
    /// <returns>Um <see cref="INamedType"/> que representa o tipo <c>Result</c>.</returns>
    public static new INamedType GetType() => _resultIType;

    /// <summary>
    /// Cria uma instância genérica do tipo <c>Result&lt;T&gt;</c> com o tipo de sucesso especificado.
    /// </summary>
    /// <param name="type">O tipo de sucesso (<c>T</c>) contido em <c>Result&lt;T&gt;</c>.</param>
    /// <returns>Um <see cref="INamedType"/> que representa <c>Result&lt;type&gt;</c>.</returns>
    public static INamedType GetType(Type type) => _genericResultIType.MakeGenericInstance(type);

    /// <summary>
    /// Cria uma instância genérica do tipo <c>Result&lt;T&gt;</c> com o tipo de sucesso especificado.
    /// </summary>
    /// <param name="type">O tipo de sucesso (<c>T</c>) contido em <c>Result&lt;T&gt;</c>.</param>
    /// <returns>Um <see cref="INamedType"/> que representa <c>Result&lt;type&gt;</c>.</returns>
    public static INamedType GetType(IType type) => _genericResultIType.MakeGenericInstance(type);
    #endregion

    #region GetTaskType
    /// <summary>
    /// Obtém o tipo <see cref="Task{TResult}"/> onde o resultado genérico é o <c>Result</c> não genérico.
    /// </summary>
    /// <returns>Um <see cref="INamedType"/> que representa o tipo <c>Task&lt;Result&gt;</c>.</returns>
    public static INamedType GetTaskType() => TaskTypeFactory.GetTaskType(_resultIType);

    /// <summary>
    /// Obtém o tipo <see cref="Task{TResult}"/> onde o resultado genérico é o <c>Result&lt;T&gt;</c> especificado.
    /// </summary>
    /// <param name="type">O tipo de sucesso (<c>T</c>) contido no <c>Result&lt;T&gt;</c>.</param>
    /// <returns>Um <see cref="INamedType"/> que representa o tipo <c>Task&lt;Result&lt;type&gt;&gt;</c>.</returns>
    public static INamedType GetTaskType(Type type) => TaskTypeFactory.GetTaskType(GetType(type));

    /// <summary>
    /// Obtém o tipo <see cref="Task{TResult}"/> onde o resultado genérico é o <c>Result&lt;T&gt;</c> especificado.
    /// </summary>
    /// <param name="type">O tipo de sucesso (<c>T</c>) contido no <c>Result&lt;T&gt;</c>.</param>
    /// <returns>Um <see cref="INamedType"/> que representa o tipo <c>Task&lt;Result&lt;type&gt;&gt;</c>.</returns>
    public static INamedType GetTaskType(IType type) => TaskTypeFactory.GetTaskType(GetType(type));
    #endregion
}
