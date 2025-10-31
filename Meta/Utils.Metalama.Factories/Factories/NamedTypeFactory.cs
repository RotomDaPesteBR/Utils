using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace LightningArc.Utils.Metalama;

/// <summary>
/// Expõe métodos que retornam instâncias da interface <see cref="INamedType"/>.
/// É um wrapper sobre <see cref="TypeFactory"/>, convertendo o resultado para <see cref="INamedType"/>.
/// </summary>
[CompileTime]
public static class NamedTypeFactory
{
    #region GetType
    /// <summary>
    /// Obtém um <see cref="INamedType"/> dado um <see cref="System.Type"/> de reflexão.
    /// </summary>
    /// <param name="type">O <see cref="System.Type"/> de reflexão.</param>
    /// <returns>
    /// Um <see cref="INamedType"/> que representa o tipo, ou <see langword="null"/> se for um tipo que
    /// não pode ser representado como <see cref="INamedType"/> (como um tipo anônimo ou um ponteiro), 
    /// ou se o tipo não for encontrado na compilação.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Lançada se o <see cref="TypeFactory"/> não estiver disponível no contexto atual.
    /// </exception>
    public static INamedType GetType(Type type)
    {
        return (INamedType)TypeFactory.GetType(type);
    }

    /// <summary>
    /// Obtém um <see cref="INamedType"/> que representa um <see cref="SpecialType"/> dado.
    /// </summary>
    /// <param name="type">O <see cref="SpecialType"/> (tipo especial) a ser recuperado.</param>
    /// <returns>Um <see cref="INamedType"/> que representa o <paramref name="type"/> especial.</returns>
    /// <exception cref="InvalidOperationException">
    /// Lançada se o <see cref="TypeFactory"/> não estiver disponível no contexto atual.
    /// </exception>
    public static INamedType GetType(SpecialType type)
    {
        return TypeFactory.GetType(type);
    }

    /// <summary>
    /// Obtém um <see cref="INamedType"/> baseado em seu nome completo, como usado em reflexão.
    /// </summary>
    /// <remarks>
    /// <para>Para tipos aninhados, isso significa usar '+', por exemplo, para obter <c>System.Environment.SpecialFolder</c>,
    /// use <c>System.Environment+SpecialFolder</c>.</para>
    /// <para>Para definições de tipo genérico, isso requer usar '`', por exemplo, para obter <c>List&lt;T&gt;</c>, use
    /// <c>System.Collections.Generic.List`1</c>.</para>
    /// <para>Tipos genéricos construídos (por exemplo, <c>List&lt;int&gt;</c>) não são suportados. Para estes, use
    /// <c>GenericExtensions.WithTypeArguments</c>.</para>
    /// </remarks>
    /// <param name="typeName">O nome completo do tipo (incluindo namespace e, para tipos aninhados, o caractere '+').</param>
    /// <returns>O <see cref="INamedType"/> que corresponde ao <paramref name="typeName"/>.</returns>
    /// <exception cref="InvalidOperationException">
    /// Lançada se o tipo não for encontrado ou se o <see cref="TypeFactory"/> não estiver disponível no contexto atual.
    /// </exception>
    public static INamedType GetType(string typeName)
    {
        return TypeFactory.GetType(typeName);
    }
    #endregion

    #region GetTypeOrDefault
    /// <summary>
    /// Obtém um <see cref="INamedType"/> baseado em seu nome completo, como usado em reflexão,
    /// retornando um valor padrão se o tipo não for encontrado.
    /// </summary>
    /// <remarks>
    /// <para>As regras de nomenclatura de <paramref name="typeName"/> são as mesmas de <see cref="GetType(string)"/>.</para>
    /// <para>Esta sobrecarga retorna <paramref name="defaultType"/> em vez de lançar uma exceção se o tipo não for encontrado.</para>
    /// </remarks>
    /// <param name="typeName">O nome completo do tipo (incluindo namespace e, para tipos aninhados, o caractere '+').</param>
    /// <param name="defaultType">O <see cref="INamedType"/> a ser retornado se o tipo não for encontrado. O padrão é <see langword="null"/>.</param>
    /// <returns>O <see cref="INamedType"/> que corresponde ao <paramref name="typeName"/>, ou <paramref name="defaultType"/> se não for encontrado.</returns>
    public static INamedType? GetTypeOrDefault(string typeName, INamedType? defaultType = null)
    {
        try
        {
            return TypeFactory.GetType(typeName);
        }
        catch
        {
            // Captura qualquer exceção lançada por TypeFactory.GetType(string) 
            // quando o tipo não é encontrado ou o TypeFactory não está disponível.
            return defaultType;
        }
    }
    #endregion
}