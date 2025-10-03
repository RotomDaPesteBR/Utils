namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Fornece métodos de extensão para a classe <see cref="Result{TValue}"/>,
    /// permitindo a composição de operações funcionais.
    /// </summary>
    public static partial class ResultExtensions
    {
        #region Result
        /// <summary>
        /// Mapeia um resultado não genérico (sem valor) aplicando funções assíncronas,
        /// retornando um Task{Result} (sem valor).
        /// </summary>
        /// <param name="result">O resultado de entrada (sem valor).</param>
        /// <param name="success">A função assíncrona a ser aplicada ao sucesso, retornando um Task{Result}.</param>
        /// <param name="failure">A função assíncrona a ser aplicada ao erro, retornando um Task{Result}.</param>
        /// <returns>Um Task contendo o novo Result.</returns>
        public static async Task<Result> MatchAsync(
            this Result result,
            Func<Task<Result>> success,
            Func<Error, Task<Result>> failure
        )
        {
            return result.IsSuccess
                ? await success().ConfigureAwait(false)
                : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Mapeia um resultado não genérico (sem valor) usando uma função de sucesso síncrona,
        /// mas com falha assíncrona. Retorna um Task{Result} (sem valor).
        /// </summary>
        /// <param name="result">O resultado de entrada (sem valor).</param>
        /// <param name="success">A função síncrona a ser aplicada ao sucesso, retornando um Result.</param>
        /// <param name="failure">A função assíncrona a ser aplicada ao erro, retornando um Task{Result}.</param>
        /// <returns>Um Task contendo o novo Result.</returns>
        public static async Task<Result> MatchAsync(
            this Result result,
            Func<Result> success,
            Func<Error, Task<Result>> failure
        )
        {
            return result.IsSuccess ? success() : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Aguarda um Task{Result} e mapeia o resultado usando funções assíncronas,
        /// retornando um Task{Result} (sem valor).
        /// </summary>
        /// <param name="resultTask">A Task que contém o resultado de entrada.</param>
        /// <param name="success">A função assíncrona a ser aplicada ao sucesso, retornando um Task{Result}.</param>
        /// <param name="failure">A função assíncrona a ser aplicada ao erro, retornando um Task{Result}.</param>
        /// <returns>Um Task contendo o novo Result.</returns>
        public static async Task<Result> MatchAsync(
            this Task<Result> resultTask,
            Func<Task<Result>> success,
            Func<Error, Task<Result>> failure
        )
        {
            var result = await resultTask.ConfigureAwait(false);

            return result.IsSuccess
                ? await success().ConfigureAwait(false)
                : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Aguarda um Task{Result} e mapeia o resultado usando funções síncronas para sucesso,
        /// com falha assíncrona. Retorna um Task{Result} (sem valor).
        /// </summary>
        /// <param name="resultTask">A Task que contém o resultado de entrada.</param>
        /// <param name="success">A função síncrona a ser aplicada ao sucesso, retornando um Result.</param>
        /// <param name="failure">A função assíncrona a ser aplicada ao erro, retornando um Task{Result}.</param>
        /// <returns>Um Task contendo o novo Result.</returns>
        public static async Task<Result> MatchAsync(
            this Task<Result> resultTask,
            Func<Result> success,
            Func<Error, Task<Result>> failure
        )
        {
            var result = await resultTask.ConfigureAwait(false);

            return result.IsSuccess ? success() : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Aguarda um Task{Result{TIn}} e mapeia o resultado. A função de sucesso recebe o Result{TIn}
        /// (para reenvio ou inspeção), enquanto a falha recebe o Error.
        /// </summary>
        public static async Task<Result<TOut>> MatchAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<Result<TIn>, Task<Result<TOut>>> success, // Recebe Result<TIn> e retorna Task<Result<TOut>>
            Func<Error, Task<Result<TOut>>> failure
        )
        {
            var result = await resultTask.ConfigureAwait(false);

            // No sucesso: a função deve mapear TIn para TOut (ou reencaminhar a falha se TIn=TOut).
            return result.IsSuccess
                ? await success(result).ConfigureAwait(false)
                : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Mapeia um resultado síncrono Result{TIn} usando um delegado que recebe o Result original.
        /// </summary>
        public static async Task<Result<TOut>> MatchAsync<TIn, TOut>(
            this Result<TIn> result,
            Func<Result<TIn>, Task<Result<TOut>>> success, // Recebe Result<TIn>
            Func<Error, Task<Result<TOut>>> failure
        )
        {
            return result.IsSuccess
                ? await success(result).ConfigureAwait(false)
                : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Aguarda um Task{Result} e mapeia o resultado. A função de sucesso recebe o Result original
        /// (para reenvio), e a falha recebe o Error, ambos retornando Task{Result}.
        /// </summary>
        public static async Task<Result> MatchAsync(
            this Task<Result> resultTask,
            Func<Result, Task<Result>> success, // Recebe Result e retorna Task<Result>
            Func<Error, Task<Result>> failure
        )
        {
            var result = await resultTask.ConfigureAwait(false);

            return result.IsSuccess
                ? await success(result).ConfigureAwait(false) // Passa o Result original para reenvio
                : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Mapeia um resultado síncrono Result usando um delegado que recebe o Result original.
        /// </summary>
        public static async Task<Result> MatchAsync(
            this Result result,
            Func<Result, Task<Result>> success, // Recebe Result
            Func<Error, Task<Result>> failure
        )
        {
            return result.IsSuccess
                ? await success(result).ConfigureAwait(false)
                : await failure(result.Error).ConfigureAwait(false);
        }
        #endregion

        #region Result<TValue>
        /// <summary>
        /// Mapeia o resultado aplicando uma das duas funções: uma para sucesso e outra para falha.
        /// </summary>
        /// <typeparam name="TIn">O tipo de valor de entrada.</typeparam>
        /// <typeparam name="TOut">O tipo de valor de saída.</typeparam>
        /// <param name="result">O resultado de entrada.</param>
        /// <param name="success">A função a ser aplicada ao valor de sucesso.</param>
        /// <param name="failure">A função a ser aplicada ao erro.</param>
        /// <returns>O valor TOut mapeado a partir do valor de sucesso ou do erro.</returns>
        public static async Task<Result<TOut>> MatchAsync<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, Task<TOut>> success,
            Func<Error, Task<Result<TOut>>> failure
        )
        {
            return result.IsSuccess
                ? Result.Success(
                    await success(result.Value).ConfigureAwait(false),
                    result.SuccessDetails
                )
                : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Mapeia o resultado aplicando uma das duas funções: uma para sucesso e outra para falha.
        /// </summary>
        /// <typeparam name="TIn">O tipo de valor de entrada.</typeparam>
        /// <typeparam name="TOut">O tipo de valor de saída.</typeparam>
        /// <param name="result">O resultado de entrada.</param>
        /// <param name="success">A função a ser aplicada ao valor de sucesso.</param>
        /// <param name="failure">A função a ser aplicada ao erro.</param>
        /// <returns>O valor TOut mapeado a partir do valor de sucesso ou do erro.</returns>
        public static async Task<Result<TOut>> MatchAsync<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, Task<Result<TOut>>> success,
            Func<Error, Task<Result<TOut>>> failure
        )
        {
            return result.IsSuccess
                ? await success(result.Value).ConfigureAwait(false)
                : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Aguarda um <see cref="Task{TResult}"/> contendo um <see cref="Result{TIn}"/> e mapeia o resultado
        /// usando funções assíncronas para sucesso e falha. O resultado de sucesso é encapsulado via conversão implícita.
        /// </summary>
        /// <typeparam name="TIn">O tipo de valor de entrada.</typeparam>
        /// <typeparam name="TOut">O tipo de valor de saída.</typeparam>
        /// <param name="resultTask">A Task que contém o resultado de entrada.</param>
        /// <param name="success">A função assíncrona a ser aplicada ao valor de sucesso, que retorna <see cref="Task{TOut}"/>.</param>
        /// <param name="failure">A função assíncrona a ser aplicada ao erro, que retorna o <see cref="Task{TResult}"/> de falha.</param>
        /// <returns>Um <see cref="Task{TResult}"/> contendo o novo <see cref="Result{TOut}"/>.</returns>
        public static async Task<Result<TOut>> MatchAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, Task<TOut>> success,
            Func<Error, Task<Result<TOut>>> failure
        )
        {
            var result = await resultTask.ConfigureAwait(false);

            return result.IsSuccess
                ? Result.Success(
                    await success(result.Value).ConfigureAwait(false),
                    result.SuccessDetails
                )
                : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Aguarda um <see cref="Task{TResult}"/> contendo um <see cref="Result{TIn}"/> e mapeia o resultado
        /// usando funções síncronas para sucesso e falha.
        /// </summary>
        /// <typeparam name="TIn">O tipo de valor de entrada.</typeparam>
        /// <typeparam name="TOut">O tipo de valor de saída.</typeparam>
        /// <param name="resultTask">A Task que contém o resultado de entrada.</param>
        /// <param name="success">A função síncrona a ser aplicada ao valor de sucesso, que retorna <typeparamref name="TOut"/>.</param>
        /// <param name="failure">A função síncrona a ser aplicada ao erro, que retorna o <see cref="Result{TOut}"/> de falha.</param>
        /// <returns>Um <see cref="Task{TResult}"/> contendo o novo <see cref="Result{TOut}"/>.</returns>
        public static async Task<Result<TOut>> MatchAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, TOut> success,
            Func<Error, Result<TOut>> failure
        )
        {
            var result = await resultTask.ConfigureAwait(false);

            return result.IsSuccess
                ? Result.Success(success(result.Value), result.SuccessDetails)
                : failure(result.Error);
        }

        /// <summary>
        /// Aguarda um <see cref="Task{TResult}"/> contendo um <see cref="Result{TIn}"/> e mapeia o resultado
        /// usando funções assíncronas para sucesso e falha. O resultado de sucesso é encapsulado via conversão implícita.
        /// </summary>
        /// <typeparam name="TIn">O tipo de valor de entrada.</typeparam>
        /// <typeparam name="TOut">O tipo de valor de saída.</typeparam>
        /// <param name="resultTask">A Task que contém o resultado de entrada.</param>
        /// <param name="success">A função assíncrona a ser aplicada ao valor de sucesso, que retorna <see cref="Task{TOut}"/>.</param>
        /// <param name="failure">A função assíncrona a ser aplicada ao erro, que retorna o <see cref="Task{TResult}"/> de falha.</param>
        /// <returns>Um <see cref="Task{TResult}"/> contendo o novo <see cref="Result{TOut}"/>.</returns>
        public static async Task<Result<TOut>> MatchAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, Task<Result<TOut>>> success,
            Func<Error, Task<Result<TOut>>> failure
        )
        {
            var result = await resultTask.ConfigureAwait(false);

            return result.IsSuccess
                ? await success(result.Value).ConfigureAwait(false)
                : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Aguarda um <see cref="Task{TResult}"/> contendo um <see cref="Result{TIn}"/> e mapeia o resultado
        /// usando funções síncronas para sucesso e falha.
        /// </summary>
        /// <typeparam name="TIn">O tipo de valor de entrada.</typeparam>
        /// <typeparam name="TOut">O tipo de valor de saída.</typeparam>
        /// <param name="resultTask">A Task que contém o resultado de entrada.</param>
        /// <param name="success">A função síncrona a ser aplicada ao valor de sucesso, que retorna <typeparamref name="TOut"/>.</param>
        /// <param name="failure">A função síncrona a ser aplicada ao erro, que retorna o <see cref="Result{TOut}"/> de falha.</param>
        /// <returns>Um <see cref="Task{TResult}"/> contendo o novo <see cref="Result{TOut}"/>.</returns>
        public static async Task<Result<TOut>> MatchAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, Result<TOut>> success,
            Func<Error, Result<TOut>> failure
        )
        {
            var result = await resultTask.ConfigureAwait(false);

            return result.IsSuccess ? success(result.Value) : failure(result.Error);
        }

        /// <summary>
        /// Mapeia o resultado síncrono (Result{TIn}) usando uma função de sucesso assíncrona
        /// e uma função de falha síncrona. Preserva o SuccessDetails.
        /// </summary>
        public static async Task<Result<TOut>> MatchAsync<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, Task<TOut>> success,
            Func<Error, Result<TOut>> failure
        )
        {
            return result.IsSuccess
                ? Result.Success(
                    await success(result.Value!).ConfigureAwait(false),
                    result.SuccessDetails
                )
                : failure(result.Error);
        }

        /// <summary>
        /// Mapeia o resultado síncrono (Result{TIn}) usando uma função de sucesso assíncrona
        /// que retorna um Result e uma função de falha síncrona.
        /// </summary>
        public static async Task<Result<TOut>> MatchAsync<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, Task<Result<TOut>>> success,
            Func<Error, Result<TOut>> failure
        )
        {
            return result.IsSuccess
                ? await success(result.Value).ConfigureAwait(false)
                : failure(result.Error);
        }

        /// <summary>
        /// Aguarda o resultado assíncrono de entrada e mapeia usando uma função de sucesso
        /// síncrona (retorna Result) e uma função de falha assíncrona.
        /// </summary>
        public static async Task<Result<TOut>> MatchAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, Result<TOut>> success,
            Func<Error, Task<Result<TOut>>> failure
        )
        {
            var result = await resultTask.ConfigureAwait(false);

            return result.IsSuccess
                ? success(result.Value)
                : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Aguarda um Task{Result} e mapeia o resultado usando funções assíncronas.
        /// </summary>
        /// <typeparam name="TOut">O tipo de valor de saída (se sucesso).</typeparam>
        /// <param name="resultTask">A Task que contém o resultado de entrada.</param>
        /// <param name="success">A função assíncrona a ser aplicada ao sucesso.</param>
        /// <param name="failure">A função assíncrona a ser aplicada ao erro.</param>
        /// <returns>Um Task contendo o novo Result{TOut}.</returns>
        public static async Task<Result<TOut>> MatchAsync<TOut>(
            this Task<Result> resultTask,
            Func<Task<TOut>> success,
            Func<Error, Task<Result<TOut>>> failure
        )
        {
            var result = await resultTask.ConfigureAwait(false);

            return result.IsSuccess
                ? Result.Success(await success().ConfigureAwait(false), result.SuccessDetails)
                : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Aguarda um Task{Result} e mapeia o resultado usando funções síncronas para sucesso.
        /// </summary>
        /// <typeparam name="TOut">O tipo de valor de saída (se sucesso).</typeparam>
        /// <param name="resultTask">A Task que contém o resultado de entrada.</param>
        /// <param name="success">A função síncrona a ser aplicada ao sucesso, que retorna TOut.</param>
        /// <param name="failure">A função assíncrona a ser aplicada ao erro.</param>
        /// <returns>Um Task contendo o novo Result{TOut}.</returns>
        public static async Task<Result<TOut>> MatchAsync<TOut>(
            this Task<Result> resultTask,
            Func<TOut> success,
            Func<Error, Task<Result<TOut>>> failure
        )
        {
            var result = await resultTask.ConfigureAwait(false);

            // Usa a conversão implícita do TOut para Result<TOut>
            return result.IsSuccess
                ? Result.Success(success(), result.SuccessDetails)
                : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Aguarda um Task{Result} e mapeia o resultado usando funções assíncronas, onde o sucesso retorna um Result.
        /// </summary>
        /// <typeparam name="TOut">O tipo de valor de saída (se sucesso).</typeparam>
        /// <param name="resultTask">A Task que contém o resultado de entrada.</param>
        /// <param name="success">A função assíncrona a ser aplicada ao sucesso, que retorna um Result{TOut}.</param>
        /// <param name="failure">A função assíncrona a ser aplicada ao erro.</param>
        /// <returns>Um Task contendo o novo Result{TOut}.</returns>
        public static async Task<Result<TOut>> MatchAsync<TOut>(
            this Task<Result> resultTask,
            Func<Task<Result<TOut>>> success,
            Func<Error, Task<Result<TOut>>> failure
        )
        {
            var result = await resultTask.ConfigureAwait(false);

            return result.IsSuccess
                ? await success().ConfigureAwait(false)
                : await failure(result.Error).ConfigureAwait(false);
        }
        #endregion
    }
}
