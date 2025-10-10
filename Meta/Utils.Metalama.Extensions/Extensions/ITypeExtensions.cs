using System.Security.Claims;
using LightningArc.Utils.Results;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace LightningArc.Utils.Metalama;

/// <summary>
/// Fornece métodos de extensão para o tipo <see cref="IType"/> do Metalama,
/// focados na inspeção de tipos de retorno de métodos, especialmente no contexto de
/// operações assíncronas (<see cref="Task"/>) e tipos de resultado baseados em <see cref="Result"/>.
/// </summary>
/// <remarks>
/// Esta classe é utilizada em Aspectos do Metalama para determinar a natureza e a serialização
/// de parâmetros e tipos de retorno em tempo de compilação.
/// </remarks>
[CompileTime]
public static class ITypeExtensions
{
    /// <summary>
    /// Fornece uma lista com Tipos não serializáveis <see cref="IType"/>,
    /// usado no método de extensão <see cref="IsSerializable(IType, Type[])"/>
    /// </summary>
    public static readonly Type[] NotSerializableTypes =
    [
        typeof(CancellationToken),
        typeof(Stream),
        typeof(ClaimsPrincipal),
    ];

    /// <summary>
    /// Determina se o tipo atual é um <see cref="Task"/> ou <see cref="Task{TResult}"/>.
    /// </summary>
    /// <param name="type">O tipo a ser verificado.</param>
    /// <returns><c>true</c> se o tipo for um Task; caso contrário, <c>false</c>.</returns>
    public static bool IsTask(this IType type)
    {
        bool isTask = type.IsConvertibleTo(typeof(Task));

        return isTask;
    }

    /// <summary>
    /// Obtém o tipo de retorno real (o tipo contido) se o tipo de entrada for um <see cref="Task{TResult}"/>.
    /// Caso contrário, retorna o próprio tipo de entrada.
    /// </summary>
    /// <remarks>
    /// Este método é crucial para abstrair a complexidade de métodos assíncronos,
    /// permitindo a inspeção do tipo de dado que realmente será retornado após a conclusão da Task.
    /// </remarks>
    /// <param name="type">O tipo de retorno do método.</param>
    /// <returns>O tipo de resultado da Task (TResult) se for um Task&lt;TResult&gt;; caso contrário, o tipo original.</returns>
    public static IType UnwrapType(this IType type) =>
        type.IsTask() ? type.GetAsyncInfo().ResultType : type;

    /// <summary>
    /// Determina se o tipo de retorno *real* (após desempacotar um possível Task) é conversível para o tipo base <see cref="Result"/>.
    /// </summary>
    /// <param name="type">O tipo de retorno do método (pode ser Task).</param>
    /// <returns><c>true</c> se o tipo for um <see cref="Result"/> não genérico ou um <see cref="Result{TValue}"/>; caso contrário, <c>false</c>.</returns>
    public static bool IsResult(this IType type) =>
        type.UnwrapType().IsConvertibleTo(typeof(Result));

    /// <summary>
    /// Determina se o tipo de retorno *real* (após desempacotar um possível Task) é uma definição de tipo genérico <see cref="Result{TValue}"/>.
    /// </summary>
    /// <param name="type">O tipo de retorno do método (pode ser Task).</param>
    /// <returns><c>true</c> se o tipo for um <see cref="Result{TValue}"/>; caso contrário, <c>false</c>.</returns>
    public static bool IsValueResult(this IType type) =>
        type.UnwrapType().IsConvertibleTo(typeof(Result<>), ConversionKind.TypeDefinition);

    /// <summary>
    /// Determina se o tipo de retorno é um tipo que geralmente é considerado serializável ou que deve ser
    /// inspecionado por aspectos.
    /// </summary>
    /// <remarks>
    /// Verifica se o tipo não está na lista de tipos que são injetados pelo framework
    /// (e.g., <see cref="CancellationToken"/>,
    /// pois esses geralmente não devem ser serializados ou inspecionados para lógica de negócios.
    /// </remarks>
    /// <param name="type">O tipo a ser verificado.</param>
    /// <param name="notSerializableTypes">Uma lista de tipos a serem reconhecidos como não serializáveis.
    /// Por padrão usa a propriedade estática <see cref="NotSerializableTypes"/></param>
    /// <returns><c>true</c> se o tipo não estiver na lista de tipos ignorados; caso contrário, <c>false</c>.</returns>
    public static bool IsSerializable(this IType type, Type[]? notSerializableTypes = null) =>
        !(notSerializableTypes is null ? NotSerializableTypes : notSerializableTypes).Any(t =>
            type.IsConvertibleTo(t)
        );
}
