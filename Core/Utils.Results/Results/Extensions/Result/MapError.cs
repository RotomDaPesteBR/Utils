namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Fornece métodos de extensão para a classe <see cref="Result{TValue}"/>,
    /// permitindo a composição de operações funcionais.
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Executa uma função no erro de um <see cref="Result{TValue}"/> se a operação falhou,
        /// permitindo transformar o erro original em um novo erro ou retornar um valor default.
        /// </summary>
        public static Result<TValue> MapError<TValue>(
            this Result<TValue> result,
            Func<Error, Error> func
        )
        {
            if (result.IsFailure)
            {
                return func(result.Error);
            }
            return result;
        }
    }
}
