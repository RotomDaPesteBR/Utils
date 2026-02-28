using System.Runtime.CompilerServices;

namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Representa o resultado assíncrono de uma operação que pode ser um sucesso (com um valor específico) ou uma falha.
    /// Encapsula uma Task que contém um <see cref="Result{TValue}"/> e é diretamente awaitable.
    /// </summary>
    /// <typeparam name="TValue">O tipo do valor de sucesso que este resultado encapsula.</typeparam>
    /// <remarks>
    /// Inicializa uma nova instância da classe <see cref="TaskResult{TValue}"/>.
    /// </remarks>
    /// <param name="task">A <see cref="Task{TResult}"/> que contém o <see cref="Result{TValue}"/>.</param>
    /// <exception cref="ArgumentNullException">Lançada se a tarefa for nula.</exception>
    public class TaskResult<TValue>(Task<Result<TValue>> task)
    {
        private readonly Task<Result<TValue>> _task = task ?? throw new ArgumentNullException(nameof(task));

        // ******** START: AWAITABLE PATTERN IMPLEMENTATION ********

        /// <summary>
        /// Obtém o awaiter para esta <see cref="TaskResult{TValue}"/>, permitindo que ela seja diretamente aguardada.
        /// </summary>
        /// <returns>Um <see cref="TaskResultAwaiter"/> para esta instância.</returns>
        public TaskResultAwaiter GetAwaiter() => new(_task);

        /// <summary>
        /// O tipo Awaiter para <see cref="TaskResult{TValue}"/>.
        /// </summary>
        /// <remarks>
        /// Inicializa uma nova instância da estrutura <see cref="TaskResultAwaiter"/>.
        /// </remarks>
        /// <param name="task">A <see cref="Task{TResult}"/> encapsulada.</param>
        public readonly struct TaskResultAwaiter(Task<Result<TValue>> task) : INotifyCompletion, ICriticalNotifyCompletion
        {
            private readonly Task<Result<TValue>> _task = task;

            /// <summary>
            /// Obtém um valor que indica se a tarefa associada está concluída.
            /// </summary>
            public bool IsCompleted => _task.IsCompleted;

            /// <summary>
            /// Obtém o resultado da tarefa após a conclusão.
            /// </summary>
            /// <returns>O <see cref="Result{TValue}"/> encapsulado pela tarefa.</returns>
            public Result<TValue> GetResult() =>
                // Isso lançará a exceção original da Task se ela falhar,
                // ou retornará o Result<TValue> se for bem-sucedida.
                _task.Result;

            /// <summary>
            /// Agenda a ação de continuação quando a tarefa for concluída.
            /// </summary>
            /// <param name="continuation">A ação a ser executada.</param>
            public void OnCompleted(Action continuation) =>
                _task.GetAwaiter().OnCompleted(continuation);

            /// <summary>
            /// Agenda a ação de continuação quando a tarefa for concluída de forma crítica.
            /// </summary>
            /// <param name="continuation">A ação a ser executada.</param>
            public void UnsafeOnCompleted(Action continuation) =>
                _task.GetAwaiter().UnsafeOnCompleted(continuation);
        }

        // ******** END: AWAITABLE PATTERN IMPLEMENTATION ********

        /// <summary>
        /// Permite a conversão implícita de uma <see cref="Task{TResult}"/> de <see cref="Result{TValue}"/> para um <see cref="TaskResult{TValue}"/>.
        /// </summary>
        /// <param name="task">A <see cref="Task{TResult}"/> de <see cref="Result{TValue}"/> a ser convertida.</param>
        /// <returns>Um <see cref="TaskResult{TValue}"/> encapsulando a tarefa.</returns>
        public static implicit operator TaskResult<TValue>(Task<Result<TValue>> task) =>
            new(task);
    }

    /// <summary>
    /// Fornece métodos estáticos de fábrica para criar <see cref="TaskResult{TValue}"/> e <see cref="TaskResult{TValue}"/>.
    /// </summary>
    public static class TaskResult
    {
        /// <summary>
        /// Cria um <see cref="TaskResult{TValue}"/> de sucesso.
        /// </summary>
        /// <typeparam name="TValue">O tipo do valor de sucesso.</typeparam>
        /// <param name="value">O valor a ser encapsulado no resultado.</param>
        /// <returns>Uma nova instância de <see cref="TaskResult{TValue}"/> indicando sucesso.</returns>
        public static TaskResult<TValue> Success<TValue>(TValue value) =>
            new(Task.FromResult(Result.Success(value)));

        /// <summary>
        /// Cria um <see cref="TaskResult{TValue}"/> de falha.
        /// </summary>
        /// <typeparam name="TValue">O tipo do valor de sucesso.</typeparam>
        /// <param name="error">O objeto <see cref="Error"/> que descreve a falha.</param>
        /// <returns>Uma nova instância de <see cref="TaskResult{TValue}"/> indicando falha.</returns>
        public static TaskResult<TValue> Failure<TValue>(Error error) =>
            new(Task.FromResult(Result<TValue>.Failure(error)));

        /// <summary>
        /// Converte uma <see cref="Task{TResult}"/> de <see cref="Result"/> em um TaskResult&lt;object&gt; para permitir encadeamento assíncrono.
        /// Este método é para casos onde o <see cref="Result"/> não possui um valor genérico (<see cref="Result"/> base).
        /// </summary>
        /// <param name="task">A <see cref="Task{TResult}"/> a ser convertida.</param>
        /// <returns>Um TaskResult&lt;object&gt; encapsulando a tarefa.</returns>
        public static TaskResult<object> FromTask(Task<Result> task) => new(
                task.ContinueWith(t =>
                {
                    if (t.IsFaulted && t.Exception != null)
                    {
                        // Propaga a exceção original da Task
                        throw t.Exception.InnerException ?? t.Exception;
                    }
                    else if (t.IsCanceled)
                    {
                        // Lida com o cancelamento da Task
                        throw new TaskCanceledException(t);
                    }
                    else if (t.Result.IsSuccess)
                    {
                        // Retorna um Result<object> de sucesso
                        return Result.Success(new object());
                    }
                    else
                    {
                        // Retorna um Result<object> de falha com o erro original
                        return Result<object>.Failure(t.Result.Error);
                    }
                })
            );

        /// <summary>
        /// Cria um TaskResult&lt;object&gt; de sucesso para operações que não retornam um valor específico.
        /// </summary>
        /// <returns>Uma nova instância de TaskResult&lt;object&gt; indicando sucesso.</returns>
        public static TaskResult<object> Success() =>
            new(Task.FromResult(Result.Success(new object())));

        /// <summary>
        /// Cria um TaskResult&lt;object&gt; de falha para operações que não retornam um valor específico.
        /// </summary>
        /// <param name="error">O objeto <see cref="Error"/> que descreve a falha.</param>
        /// <returns>Uma nova instância de TaskResult&lt;object&gt; indicando falha.</returns>
        public static TaskResult<object> Failure(Error error) =>
            new(Task.FromResult(Result<object>.Failure(error)));
    }
}
