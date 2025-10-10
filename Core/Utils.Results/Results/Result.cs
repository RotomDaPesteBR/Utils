namespace LightningArc.Utils.Results;

/// <summary>
/// Representa o resultado de uma operação que pode ser um sucesso ou uma falha.
/// Esta é a classe base para o padrão Result, usada quando uma operação não retorna um valor específico em caso de sucesso.
/// </summary>
/// <remarks>
/// Um <see cref="Result"/> é imutável e sua natureza (sucesso ou falha) é definida no momento da criação.
/// Em caso de falha, ele encapsula um objeto <see cref="Error"/> que fornece detalhes sobre o ocorrido.
/// </remarks>
public class Result
{
    /// <summary>
    /// Obtém um valor que indica se a operação foi bem-sucedida.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Obtém um valor que indica se a operação falhou.
    /// </summary>
    public bool IsFailure
    {
        get { return !IsSuccess; }
    }

    /// <summary>
    /// Obtém o código de status genérico associado ao resultado.
    /// </summary>
    /// <remarks>
    /// Este é um valor de conveniência que retorna o código do objeto <see cref="Results.Success"/> em caso de sucesso
    /// ou do objeto <see cref="Error"/> em caso de falha.
    /// </remarks>
    public int Code => IsSuccess ? SuccessDetails!.Code : Error!.Code;

    /// <summary>
    /// Obtém o objeto <see cref="Error"/> associado a este resultado.
    /// </summary>
    /// <exception cref="ResultAccessFailedException">Lançada se o resultado for de sucesso (não há erro para acessar).</exception>
    public Error Error =>
        IsFailure
            ? _error!
            : throw new ResultAccessFailedException("Result is successful, no error to access.");

    /// <summary>
    /// Obtém o objeto <see cref="Results.Success"/> associado a este resultado.
    /// </summary>
    /// <exception cref="ResultAccessFailedException">Lançada se o resultado for de falha (não há sucesso para acessar).</exception>
    public Success SuccessDetails =>
        IsSuccess
            ? _success!
            : throw new ResultAccessFailedException(
                "Result is not successful, no success details to access."
            );

    private readonly Error? _error;
    private readonly Success? _success;

    /// <summary>
    /// Construtor protegido para um resultado de sucesso.
    /// </summary>
    /// <param name="success">O objeto <see cref="Results.Success"/> contendo o código e a mensagem.</param>
    protected Result(Success success)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(success, nameof(success));
#else
        if (success is null)
        {
            throw new ArgumentNullException(nameof(success));
        }
#endif

        IsSuccess = true;
        _success = success;
        _error = null;
    }

    /// <summary>
    /// Construtor protegido para um resultado de falha.
    /// </summary>
    /// <param name="error">O objeto <see cref="Error"/> que descreve a falha.</param>
    protected Result(Error error)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(error, nameof(error));
#else
        if (error is null)
        {
            throw new ArgumentNullException(nameof(error));
        }
#endif

        IsSuccess = false;
        _error = error;
        _success = null;
    }

    /// <summary>
    /// Construtor protegido de cópia.
    /// </summary>
    /// <param name="result">O objeto <see cref="Result"/> a ser copiado.</param>
    protected Result(Result result)
    {
        IsSuccess = result.IsSuccess;
        _success = result._success;
        _error = result._error;
    }

    /// <summary>
    /// Cria um resultado de sucesso com um código genérico (Ok) e uma mensagem opcional.
    /// </summary>
    /// <returns>Uma nova instância de <see cref="Result"/> indicando sucesso.</returns>
    public static Result Success() => new(Results.Success.Ok());

    /// <summary>
    /// Cria um resultado de sucesso com o Success especificado
    /// </summary>
    /// <param name="success">O retorno com sucesso.</param>
    /// <returns>Uma nova instância de <see cref="Result"/> indicando sucesso.</returns>
    public static Result Success(Success success) => new(success);

    /// <summary>
    /// Cria um resultado de sucesso com um código genérico (Created) e uma mensagem opcional.
    /// </summary>
    /// <returns>Uma nova instância de <see cref="Result"/> indicando sucesso.</returns>
    public static Result Created() => new(Results.Success.Created());

    /// <summary>
    /// Cria um resultado de sucesso com um código genérico (Accepted) e uma mensagem opcional.
    /// </summary>
    /// <returns>Uma nova instância de <see cref="Result"/> indicando sucesso.</returns>
    public static Result Accepted() => new(Results.Success.Accepted());

    /// <summary>
    /// Cria um resultado de sucesso com um código genérico (NoContent).
    /// </summary>
    /// <returns>Uma nova instância de <see cref="Result"/> indicando sucesso.</returns>
    public static Result NoContent() => new(Results.Success.NoContent());

    // Métodos de fábrica que criam um Result com valor

    /// <summary>
    /// Transforma um <see cref="Result"/> não-genérico (sem valor) em um <see cref="Result{TValue}"/> (com valor).
    /// </summary>
    /// <typeparam name="TValue">O tipo do valor de sucesso.</typeparam>
    /// <param name="value">O valor a ser encapsulado no novo resultado.</param>
    /// <returns>Uma nova instância de <see cref="Result{TValue}"/>, mantendo o status de sucesso/falha do resultado original.
    /// Se o resultado original for de sucesso, ele encapsula o novo valor. Se for de falha, retorna a falha original.</returns>
    public Result<TValue> WithValue<TValue>(TValue value) => new(value, this);

    /// <summary>
    /// Cria um resultado de sucesso com um valor e um código genérico (Ok), usando a mensagem padrão.
    /// </summary>
    /// <typeparam name="TValue">O tipo do valor de sucesso.</typeparam>
    /// <param name="value">O valor a ser encapsulado no resultado.</param>
    /// <returns>Uma nova instância de <see cref="Result{TValue}"/> indicando sucesso.</returns>
    public static Result<TValue> Success<TValue>(TValue value) =>
        new(Results.Success<TValue>.Ok(value));

    /// <summary>
    /// Cria um resultado de sucesso com um valor e um código genérico (Ok), com mensagem customizada.
    /// </summary>
    /// <typeparam name="TValue">O tipo do valor de sucesso.</typeparam>
    /// <param name="value">O valor a ser encapsulado no resultado.</param>
    /// <param name="message">A mensagem customizada de sucesso.</param>
    /// <returns>Uma nova instância de <see cref="Result{TValue}"/> indicando sucesso.</returns>
    public static Result<TValue> Success<TValue>(TValue value, string message) =>
        new(Results.Success<TValue>.Ok(value, message));

    /// <summary>
    /// Cria um resultado de sucesso com um valor e um código genérico (Ok), com mensagem customizada.
    /// </summary>
    /// <typeparam name="TValue">O tipo do valor de sucesso.</typeparam>
    /// <param name="value">O valor a ser encapsulado no resultado.</param>
    /// <param name="success">O retorno com sucesso.</param>
    /// <returns>Uma nova instância de <see cref="Result{TValue}"/> indicando sucesso.</returns>
    public static Result<TValue> Success<TValue>(TValue value, Success success) =>
        new(success.WithValue(value));

    /// <summary>
    /// Cria um resultado de sucesso com o Success especificado.
    /// </summary>
    /// <typeparam name="TValue">O tipo do valor de sucesso.</typeparam>
    /// <param name="success">O retorno com sucesso.</param>
    /// <returns>Uma nova instância de <see cref="Result{TValue}"/> indicando sucesso.</returns>
    public static Result<TValue> Success<TValue>(Success<TValue> success) => new(success);

    // --- Created ---

    /// <summary>
    /// Cria um resultado de sucesso com um valor e um código genérico (Created), usando a mensagem padrão.
    /// </summary>
    /// <typeparam name="TValue">O tipo do valor de sucesso.</typeparam>
    /// <param name="value">O valor a ser encapsulado no resultado.</param>
    /// <returns>Uma nova instância de <see cref="Result{TValue}"/> indicando sucesso e criação.</returns>
    public static Result<TValue> Created<TValue>(TValue value) =>
        new(Results.Success<TValue>.Created(value));

    /// <summary>
    /// Cria um resultado de sucesso com um valor e um código genérico (Created), com mensagem customizada.
    /// </summary>
    /// <typeparam name="TValue">O tipo do valor de sucesso.</typeparam>
    /// <param name="value">O valor a ser encapsulado no resultado.</param>
    /// <param name="message">A mensagem customizada de sucesso.</param>
    /// <returns>Uma nova instância de <see cref="Result{TValue}"/> indicando sucesso e criação.</returns>
    public static Result<TValue> Created<TValue>(TValue value, string message) =>
        new(Results.Success<TValue>.Created(value, message));

    // --- Accepted ---

    /// <summary>
    /// Cria um resultado de sucesso com um valor e um código genérico (Accepted), usando a mensagem padrão.
    /// </summary>
    /// <typeparam name="TValue">O tipo do valor de sucesso.</typeparam>
    /// <param name="value">O valor a ser encapsulado no resultado.</param>
    /// <returns>Uma nova instância de <see cref="Result{TValue}"/> indicando sucesso e aceitação.</returns>
    public static Result<TValue> Accepted<TValue>(TValue value) =>
        new(Results.Success<TValue>.Accepted(value));

    /// <summary>
    /// Cria um resultado de sucesso com um valor e um código genérico (Accepted), com mensagem customizada.
    /// </summary>
    /// <typeparam name="TValue">O tipo do valor de sucesso.</typeparam>
    /// <param name="value">O valor a ser encapsulado no resultado.</param>
    /// <param name="message">A mensagem customizada de sucesso.</param>
    /// <returns>Uma nova instância de <see cref="Result{TValue}"/> indicando sucesso e aceitação.</returns>
    public static Result<TValue> Accepted<TValue>(TValue value, string message) =>
        new(Results.Success<TValue>.Accepted(value, message));

    // --- NoContent ---

    /// <summary>
    /// Cria um resultado de sucesso com um valor e um código genérico (No Content).
    /// </summary>
    /// <typeparam name="TValue">O tipo do valor de sucesso.</typeparam>
    /// <param name="value">O valor a ser encapsulado no resultado.</param>
    /// <returns>Uma nova instância de <see cref="Result{TValue}"/> indicando sucesso e sem conteúdo.</returns>
    public static Result<TValue> NoContent<TValue>(TValue value) =>
        new(Results.Success<TValue>.NoContent(value));

    /// <summary>
    /// Cria um resultado de sucesso com um valor e um código genérico (No Content).
    /// </summary>
    /// <typeparam name="TValue">O tipo do valor de sucesso.</typeparam>
    /// <param name="value">O valor a ser encapsulado no resultado.</param>
    /// <param name="message">A mensagem customizada de sucesso.</param>
    /// <returns>Uma nova instância de <see cref="Result{TValue}"/> indicando sucesso e sem conteúdo.</returns>
    public static Result<TValue> NoContent<TValue>(TValue value, string message) =>
        new(Results.Success<TValue>.NoContent(value, message));

    /// <summary>
    /// Cria um resultado de falha.
    /// </summary>
    /// <param name="error">O objeto <see cref="Error"/> que descreve a falha.</param>
    /// <returns>Uma nova instância de <see cref="Result"/> indicando falha.</returns>
    public static Result Failure(Error error) => new(error);

    /// <summary>
    /// Permite a conversão implícita de um <see cref="Error"/> para um <see cref="Result"/> de falha.
    /// </summary>
    /// <param name="error">O objeto <see cref="Error"/> a ser convertido.</param>
    /// <returns>Um <see cref="Result"/> de falha encapsulando o erro.</returns>
    public static implicit operator Result(Error error) => new(error);

    /// <summary>
    /// Permite a conversão implícita de um <see cref="Results.Success"/> para um <see cref="Result"/> de sucesso.
    /// </summary>
    /// <param name="success">O objeto <see cref="Results.Success"/> a ser convertido.</param>
    /// <returns>Um <see cref="Result"/> de sucesso encapsulando o sucesso.</returns>
    public static implicit operator Result(Success success) => new(success);
}

/// <summary>
/// Representa o resultado de uma operação que pode ser um sucesso (com um valor específico) ou uma falha.
/// </summary>
/// <typeparam name="TValue">O tipo do valor de sucesso que este resultado encapsula.</typeparam>
/// <remarks>
/// Esta classe herda de <see cref="Result"/> e adiciona a capacidade de carregar um valor de sucesso.
/// É a forma preferida de retornar resultados de operações que produzem dados.
/// </remarks>
public class Result<TValue> : Result
{
    /// <summary>
    /// Obtém o objeto <see cref="Results.Success"/> associado a este resultado.
    /// </summary>
    /// <exception cref="ResultAccessFailedException">Lançada se o resultado for de falha (não há sucesso para acessar).</exception>
    public new Success<TValue> SuccessDetails =>
        IsSuccess
            ? _success!
            : throw new ResultAccessFailedException(
                "Result is not successful, no success details to access."
            );

    /// <summary>
    /// Obtém o valor de sucesso encapsulado por este resultado.
    /// </summary>
    /// <exception cref="ResultAccessFailedException">Lançada se o resultado não for de sucesso (não há valor para acessar).</exception>
    public TValue Value =>
        IsSuccess
            ? _success!.Value
            : throw new ResultAccessFailedException("Result is not successful.");
    private readonly Success<TValue>? _success;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Result{TValue}"/> com um valor de sucesso.
    /// </summary>
    /// <param name="value">O valor de sucesso a ser encapsulado.</param>
    /// <param name="success">O objeto <see cref="Success"/> contendo o código e a mensagem.</param>
    internal Result(TValue value, Success success)
        : base(success)
    {
        _success = success.WithValue(value);
    }

    /// <summary>
    /// Construtor protegido para um resultado de sucesso.
    /// </summary>
    /// <param name="success">O objeto <see cref="Results.Success"/> contendo o código e a mensagem.</param>
    internal Result(Success<TValue> success)
        : base(success)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(success, nameof(success));
#else
        if (success is null)
        {
            throw new ArgumentNullException(nameof(success));
        }
#endif

        _success = success;
    }

    /// <summary>
    /// Construtor interno usado para anexar um valor a um resultado existente.
    /// </summary>
    /// <param name="value">O valor a ser encapsulado.</param>
    /// <param name="result">O resultado não-genérico (<see cref="Result"/>) existente.</param>
    internal Result(TValue value, Result result)
        : base(result)
    {
        if (result.IsSuccess)
        {
            _success = result.SuccessDetails.WithValue(value);
        }
    }

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Result{TValue}"/> com um erro de falha.
    /// </summary>
    /// <param name="error">O objeto <see cref="Error"/> que descreve a falha.</param>
    internal Result(Error error)
        : base(error) { }

    /// <summary>
    /// Cria um resultado de falha genérico.
    /// </summary>
    /// <param name="error">O objeto <see cref="Error"/> que descreve a falha.</param>
    /// <returns>Uma nova instância de <see cref="Result{TValue}"/> indicando falha.</returns>
    public static new Result<TValue> Failure(Error error) => new(error);

    /// <summary>
    /// Permite a conversão de um result genérico para um result não-genérico.
    /// </summary>
    /// <param name="result">O valor a ser convertido.</param>
    /// <returns>Um <see cref="Result{TValue}"/> de sucesso encapsulando o valor.</returns>
    public static Result ToResult(Result<TValue> result) =>
        result.IsSuccess ? Result.Success(result.SuccessDetails) : Result.Failure(result.Error);

    /// <summary>
    /// Permite a conversão implícita de um valor para um <see cref="Result{TValue}"/> de sucesso.
    /// O resultado de sucesso terá o código genérico "Ok" (100).
    /// </summary>
    /// <param name="value">O valor a ser convertido.</param>
    /// <returns>Um <see cref="Result{TValue}"/> de sucesso encapsulando o valor.</returns>
    public static implicit operator Result<TValue>(TValue value) =>
        new(value, Results.Success.Ok());

    /// <summary>
    /// Permite a conversão implícita de um <see cref="Error"/> para um <see cref="Result{TValue}"/> de falha.
    /// </summary>
    /// <param name="error">O objeto <see cref="Error"/> a ser convertido.</param>
    /// <returns>Um <see cref="Result{TValue}"/> de falha encapsulando o erro.</returns>
    public static implicit operator Result<TValue>(Error error) => new(error);

    /// <summary>
    /// Permite a conversão implícita de um <see cref="Success{TValue}"/> para um <see cref="Result{TValue}"/> de sucesso.
    /// </summary>
    /// <param name="success">O objeto <see cref="Success{TValue}"/> a ser convertido.</param>
    /// <returns>Um <see cref="Result{TValue}"/> de sucesso.</returns>
    public static implicit operator Result<TValue>(Success<TValue> success) => new(success);
}
