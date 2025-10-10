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
        /// by applying an asynchronous function.
        /// </summary>
        /// <typeparam name="TIn">The type of the input success value.</typeparam>
        /// <typeparam name="TOut">The type of the output success value.</typeparam>
        /// <param name="success">The input success object.</param>
        /// <param name="mapper">The asynchronous function to transform <typeparamref name="TIn"/> into <see cref="Task{TResult}">Task&lt;TOut&gt;</see>.</param>
        /// <returns>A <see cref="Task{TResult}">Task&lt;Success&lt;TOut&gt;&gt;</see> containing the mapped success object.</returns>
        public static async Task<Success<TOut>> MapAsync<TIn, TOut>(
            this Success<TIn> success,
            Func<TIn, Task<TOut>> mapper
        )
        {
            // 1. Aplica a função assíncrona para obter o novo valor (TOut).
            TOut newValue = await mapper(success.Value!).ConfigureAwait(false);

            // 2. Chama o método WithValue (que deve ser síncrono ou ser uma factory que use o WithValue internamente).
            return success.WithValue(newValue);
        }
    }
}
