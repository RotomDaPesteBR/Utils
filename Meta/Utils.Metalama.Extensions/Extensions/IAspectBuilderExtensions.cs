using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.DeclarationBuilders;

namespace LightningArc.Utils.Metalama;

/// <summary>
/// Fornece métodos de extensão para simplificar a adição (introdução) de métodos a tipos
/// dentro do contexto da construção de aspectos do Metalama.
/// </summary>
/// <remarks>
/// Estes métodos são úteis para aspectos que precisam gerar código Boilerplate, como
/// a introdução de métodos de interface ou a criação de stubs assíncronos.
/// </remarks>
[CompileTime]
public static class IAspectBuilderExtensions
{
    /// <summary>
    /// Especifica o tipo de retorno assíncrono a ser usado ao introduzir um método.
    /// </summary>
    [CompileTime]
    public enum AsyncType
    {
        /// <summary>
        /// O método de retorno assíncrono deve usar <see cref="Task"/> ou <see cref="Task{TResult}"/>.
        /// </summary>
        Task,

        /// <summary>
        /// O método de retorno assíncrono deve usar <see cref="ValueTask"/> ou <see cref="ValueTask{TResult}"/>.
        /// </summary>
        ValueTask,
    }

    /// <summary>
    /// Introduz um novo método síncrono no tipo atualmente sendo construído pelo Aspecto.
    /// </summary>
    /// <param name="templateName">O nome do template de código que será usado para o corpo do método.</param>
    /// <param name="methodName">O nome que o novo método deve ter.</param>
    /// <param name="returnType">O tipo de retorno do novo método.</param>
    /// <param name="parameters">Uma Ação opcional para configurar os parâmetros do método.</param>
    /// <param name="args">Um objeto anônimo que passa os valores atribuídos aos parâmetros que possuem <see cref="CompileTimeAttribute" />, para os parâmetros de mesmo nome do método introduzido.</param>
    /// <param name="methodBuilder">Uma Ação opcional para configurações avançadas do método.</param>
    /// <param name="builder">O construtor do aspecto para o tipo (INamedType).</param>
    /// <returns>O construtor do aspecto (builder) para permitir o encadeamento de chamadas.</returns>
    [CompileTime]
    public static IAspectBuilder<INamedType> AddMethod(
        this IAspectBuilder<INamedType> builder,
        string templateName,
        string methodName,
        IType returnType,
        Action<IParameterBuilderList>? parameters = null,
        object? args = null,
        Action<IMethodBuilder>? methodBuilder = null
    )
    {
        builder.IntroduceMethod(
            template: templateName,
            scope: IntroductionScope.Default,
            whenExists: OverrideStrategy.Ignore,
            buildMethod: m =>
            {
                m.Name = methodName;
                m.ReturnType = returnType;
                parameters?.Invoke(m.Parameters);

                methodBuilder?.Invoke(m);
            },
            args: args
        );

        return builder;
    }

    /// <summary>
    /// Introduz um novo método assíncrono no tipo, garantindo que o nome termine em "Async"
    /// e o tipo de retorno seja envolvido em <see cref="Task"/> ou <see cref="ValueTask"/>.
    /// </summary>
    /// <param name="templateName">O nome do template de código que será usado para o corpo do método.</param>
    /// <param name="methodName">O nome base do método (a parte antes de "Async").</param>
    /// <param name="returnType">O tipo de valor real a ser retornado (TResult), antes de ser envolvido em Task/ValueTask.</param>
    /// <param name="parameters">Uma Ação opcional para configurar os parâmetros do método.</param>
    /// <param name="args">Um objeto anônimo que passa os valores atribuídos aos parâmetros que possuem <see cref="CompileTimeAttribute" />,
    /// para os parâmetros de mesmo nome do método introduzido.</param>
    /// <param name="methodBuilder">Uma Ação opcional para configurações avançadas do método.</param>
    /// <param name="asyncType">O tipo de wrapper assíncrono a ser usado: <see cref="AsyncType.Task"/> (padrão) ou <see cref="AsyncType.ValueTask"/>.</param>
    /// <param name="builder">O construtor do aspecto para o tipo (INamedType).</param>
    /// <returns>O construtor do aspecto (builder) para permitir o encadeamento de chamadas.</returns>
    [CompileTime]
    public static IAspectBuilder<INamedType> AddAsyncMethod(
        this IAspectBuilder<INamedType> builder,
        string templateName,
        string methodName,
        IType returnType,
        Action<IParameterBuilderList>? parameters = null,
        object? args = null,
        Action<IMethodBuilder>? methodBuilder = null,
        AsyncType asyncType = AsyncType.Task
    )
    {
        IType asyncReturnType = asyncType switch
        {
            AsyncType.Task => TaskTypeFactory.GetTaskType(returnType),
            AsyncType.ValueTask => TaskTypeFactory.GetValueTaskType(returnType),
            _ => TaskTypeFactory.GetTaskType(returnType),
        };

        builder.AddMethod(
            templateName: templateName,
            methodName: $"{methodName}Async",
            returnType: asyncReturnType,
            parameters: parameters,
            args: args,
            methodBuilder: methodBuilder
        );

        return builder;
    }
}
