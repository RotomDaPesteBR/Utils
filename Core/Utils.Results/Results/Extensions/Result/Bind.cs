// File: ResultExtensions.Bind.cs

namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Provides extension methods for the <see cref="Result{TValue}"/> class,
    /// enabling the composition of functional operations.
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Chains the current result with a new operation that might fail, transforming the success value
        /// into a new <see cref="Result{TOut}"/>. This is the **Bind** (or FlatMap) operation.
        /// If the input result is a failure, the error is propagated without executing the function.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">O tipo de valor de saída.</typeparam>
        /// <param name="result">The input result.</param>
        /// <param name="func">The function to apply to the successful value, which returns a new <see cref="Result{TOut}"/>.</param>
        /// <returns>The new <see cref="Result{TOut}"/> produced by <paramref name="func"/>, or the original error.</returns>
        public static Result<TOut> Bind<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, Result<TOut>> func
        )
        {
            if (result.IsSuccess)
            {
                // result.Value is non-null when IsSuccess is true.
                return func(result.Value!);
            }

            // The implicit conversion from Error to Result<TOut> applies here.
            return result.Error;
        }
    }
}