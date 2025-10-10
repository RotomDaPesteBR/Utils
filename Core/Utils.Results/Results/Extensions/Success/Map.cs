namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Provides extension methods for the <see cref="Success{TValue}"/> class,
    /// enabling fluent value transformation (mapping).
    /// </summary>
    public static partial class SuccessExtensions
    {
        /// <summary>
        /// Maps the value of a <see cref="Success{TIn}"/> to a new value type, <typeparamref name="TOut"/>,
        /// by applying a synchronous function. The internal <see cref="Success.WithValue"/> mechanism
        /// of the Success object is used to create the new <see cref="Success{TOut}"/>,
        /// preserving auxiliary success details.
        /// </summary>
        /// <typeparam name="TIn">The type of the input success value.</typeparam>
        /// <typeparam name="TOut">The type of the output success value.</typeparam>
        /// <param name="success">The input success object.</param>
        /// <param name="mapper">The synchronous function to transform <typeparamref name="TIn"/> into <typeparamref name="TOut"/>.</param>
        /// <returns>A new <see cref="Success{TOut}"/> containing the mapped value and the original details.</returns>
        public static Success<TOut> Map<TIn, TOut>(
            this Success<TIn> success,
            Func<TIn, TOut> mapper
        )
            // 1. Aplica a função para obter o novo valor (TOut).
            // 2. Chama o método de instância ou o construtor WithValue/Create da classe Success<T> (necessita de implementação).
            //    A sintaxe abaixo é um exemplo de como WithValue agiria internamente.
            =>
            success.WithValue(mapper(success.Value!));
    }
}
