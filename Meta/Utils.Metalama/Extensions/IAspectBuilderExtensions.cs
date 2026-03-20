using LightningArc.Utils.Metalama;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.DeclarationBuilders;
using Metalama.Framework.Engine.Extensibility;

// [assembly: ExportExtension(typeof(IAspectBuilderExtensions), ExtensionKinds.None)]

namespace LightningArc.Utils.Metalama;

/// <summary>
/// Provides extension methods to simplify adding (introducing) methods to types
/// within the context of building Metalama aspects.
/// </summary>
/// <remarks>
/// These methods are useful for aspects that need to generate boilerplate code, such as
/// introducing interface methods or creating asynchronous stubs.
/// </remarks>
[CompileTime]
public static class IAspectBuilderExtensions
{
    /// <summary>
    /// Specifies the asynchronous return type to be used when introducing a method.
    /// </summary>
    public enum AsyncType
    {
        /// <summary>
        /// The asynchronous return method should use <see cref="Task"/> or <see cref="Task{TResult}"/>.
        /// </summary>
        Task,

        /// <summary>
        /// The asynchronous return method should use <see cref="ValueTask"/> or <see cref="ValueTask{TResult}"/>.
        /// </summary>
        ValueTask,
    }

    /// <summary>
    /// Introduces a new synchronous method into the type currently being built by the Aspect.
    /// </summary>
    /// <param name="templateName">The name of the code template that will be used for the method body.</param>
    /// <param name="methodName">The name the new method should have.</param>
    /// <param name="returnType">The return type of the new method.</param>
    /// <param name="parameters">An optional Action to configure the method parameters.</param>
    /// <param name="args">An anonymous object that passes the values assigned to parameters that have the <see cref="CompileTimeAttribute" />, for parameters of the same name in the introduced method.</param>
    /// <param name="methodBuilder">An optional Action for advanced method configurations.</param>
    /// <param name="builder">The aspect builder for the type (INamedType).</param>
    /// <returns>The aspect builder to allow for call chaining.</returns>
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
    /// Introduces a new asynchronous method into the type, ensuring that the name ends in "Async"
    /// and the return type is wrapped in <see cref="Task"/> or <see cref="ValueTask"/>.
    /// </summary>
    /// <param name="templateName">The name of the code template that will be used for the method body.</param>
    /// <param name="methodName">The base name of the method (the part before "Async").</param>
    /// <param name="returnType">The actual value type to be returned (TResult), before being wrapped in Task/ValueTask.</param>
    /// <param name="parameters">An optional Action to configure the method parameters.</param>
    /// <param name="args">An anonymous object that passes the values assigned to parameters that have the <see cref="CompileTimeAttribute" />,
    /// for parameters of the same name in the introduced method.</param>
    /// <param name="methodBuilder">An optional Action for advanced method configurations.</param>
    /// <param name="asyncType">The type of asynchronous wrapper to be used: <see cref="AsyncType.Task"/> (default) or <see cref="AsyncType.ValueTask"/>.</param>
    /// <param name="builder">The aspect builder for the type (INamedType).</param>
    /// <returns>The aspect builder to allow for call chaining.</returns>
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
