using Metalama.Framework.Aspects;
using Metalama.Framework.Code.DeclarationBuilders;

namespace LightningArc.Utils.Metalama;

/// <summary>
/// Provides extension methods for <see cref="IMethodBaseBuilder"/>.
/// </summary>
[CompileTime]
public static class IMethodBaseBuilderExtensions
{
    /// <summary>
    /// Adds a new parameter to the method based on a <see cref="ParameterDefinition"/>.
    /// </summary>
    /// <param name="methodBaseBuilder">The <see cref="IMethodBaseBuilder"/> to add the parameter to.</param>
    /// <param name="parameterDefinition">The definition of the parameter to add.</param>
    /// <returns>An <see cref="IParameterBuilder"/> for the new parameter.</returns>
    [CompileTime]
    public static IParameterBuilder AddParameter(
        this IMethodBaseBuilder methodBaseBuilder,
        ParameterDefinition parameterDefinition
    )
    {
        return methodBaseBuilder.AddParameter(
            parameterDefinition.Name,
            parameterDefinition.Type,
            parameterDefinition.RefKind,
            parameterDefinition.DefaultValue
        );
    }

    /// <summary>
    /// Adds a range of new parameters to the method based on a collection of <see cref="ParameterDefinition"/> objects.
    /// </summary>
    /// <param name="methodBaseBuilder">The <see cref="IMethodBaseBuilder"/> to add the parameters to.</param>
    /// <param name="parameterDefinitions">The definitions of the parameters to add.</param>
    /// <returns>A collection of <see cref="IParameterBuilder"/> objects for the new parameters.</returns>
    [CompileTime]
    public static IEnumerable<IParameterBuilder> AddParameterRange(
        this IMethodBaseBuilder methodBaseBuilder,
        IEnumerable<ParameterDefinition> parameterDefinitions
    )
    {
        List<IParameterBuilder> parameterBuilders = [];

        foreach (var parameterDefinition in parameterDefinitions)
        {
            var builder = methodBaseBuilder.AddParameter(parameterDefinition);

            parameterBuilders.Add(builder);
        }

        return parameterBuilders;
    }
}
