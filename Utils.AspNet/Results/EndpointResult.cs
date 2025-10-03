namespace LightningArc.Utils.Results.AspNet;


/// <summary>
/// Representa um resultado de endpoint que mapeia um <see cref="Result"/>
/// para uma resposta HTTP apropriada.
/// </summary>
/// <remarks>
/// Esta classe atua como um adaptador entre a lógica de negócio que retorna um <see cref="Result"/>
/// e a camada de apresentação (API) que precisa produzir uma resposta HTTP.
/// Ela implementa <see cref="IResult"/> do ASP.NET Core para ser diretamente retornável de endpoints.
/// </remarks>
public sealed class EndpointResult : IResult
{
    private readonly IResult _result;

    /// <summary>
    /// Construtor privado para criar uma nova instância de <see cref="EndpointResult"/>.
    /// </summary>
    /// <param name="result">O <see cref="IResult"/> interno que será executado.</param>
    private EndpointResult(IResult result)
    {
        _result = result;
    }

    /// <summary>
    /// Executa o resultado HTTP assincronamente, escrevendo a resposta no <see cref="HttpContext"/>.
    /// </summary>
    /// <param name="httpContext">O contexto HTTP atual.</param>
    /// <returns>Uma <see cref="Task"/> que representa a operação assíncrona.</returns>
    public Task ExecuteAsync(HttpContext httpContext) => _result.ExecuteAsync(httpContext);

    /// <summary>
    /// Permite a conversão implícita de um <see cref="Result"/> para um <see cref="EndpointResult"/>.
    /// </summary>
    /// <param name="result">O <see cref="Result"/> a ser convertido.</param>
    /// <returns>Um <see cref="EndpointResult"/> que encapsula a resposta HTTP correspondente.</returns>
    public static implicit operator EndpointResult(Result result)
    {
        if (result.IsSuccess)
        {
            return new EndpointResult(new SuccessResult(result.SuccessDetails!));
        }
        else
        {
            return new EndpointResult(new ErrorResult(result.Error!));
        }
    }
}

/// <summary>
/// Representa um resultado de endpoint genérico que mapeia um <see cref="Result{TValue}"/>
/// para uma resposta HTTP apropriada, incluindo o código de status e o corpo.
/// </summary>
/// <typeparam name="TValue">O tipo do valor de sucesso contido no <see cref="Result{TValue}"/>.</typeparam>
/// <remarks>
/// Esta classe atua como um adaptador entre a lógica de negócio que retorna um <see cref="Result{TValue}"/>
/// e a camada de apresentação (API) que precisa produzir uma resposta HTTP.
/// Ela implementa <see cref="IResult"/> do ASP.NET Core para ser diretamente retornável de endpoints.
/// </remarks>
public sealed class EndpointResult<TValue> : IResult
{
    private readonly IResult _result;

    /// <summary>
    /// Construtor privado para criar uma nova instância de <see cref="EndpointResult{TValue}"/>.
    /// </summary>
    /// <param name="result">O <see cref="IResult"/> interno que será executado.</param>
    private EndpointResult(IResult result)
    {
        _result = result;
    }

    /// <summary>
    /// Executa o resultado HTTP assincronamente, escrevendo a resposta no <see cref="HttpContext"/>.
    /// </summary>
    /// <param name="httpContext">O contexto HTTP atual.</param>
    /// <returns>Uma <see cref="Task"/> que representa a operação assíncrona.</returns>
    public Task ExecuteAsync(HttpContext httpContext) => _result.ExecuteAsync(httpContext);

    /// <summary>
    /// Cria um EndpointResult a partir de um Result com um tipo de conteúdo específico.
    /// </summary>
    /// <param name="result">O resultado de sucesso ou erro.</param>
    /// <param name="contentType">O tipo de conteúdo desejado (ex: "text/plain").</param>
    /// <returns>Uma nova instância de EndpointResult.</returns>
    public static EndpointResult<TValue> FromResult(Result<TValue> result, string? contentType)
    {
        if (result.IsSuccess)
        {
            return new EndpointResult<TValue>(
                new SuccessResult<TValue>(result.SuccessDetails!, result.Value!, contentType)
            );
        }
        else
        {
            return new EndpointResult<TValue>(new ErrorResult(result.Error!));
        }
    }

    /// <summary>
    /// Permite a conversão implícita de um <see cref="Result{TValue}"/> para um <see cref="EndpointResult{TValue}"/>.
    /// </summary>
    /// <param name="result">O <see cref="Result{TValue}"/> a ser convertido.</param>
    /// <returns>Um <see cref="EndpointResult{TValue}"/> que encapsula a resposta HTTP correspondente.</returns>
    public static implicit operator EndpointResult<TValue>(Result<TValue> result)
    {
        if (result.IsSuccess)
        {
            return new EndpointResult<TValue>(
                new SuccessResult<TValue>(result.SuccessDetails!, result.Value!)
            );
        }
        else
        {
            return new EndpointResult<TValue>(new ErrorResult(result.Error!));
        }
    }

    /// <summary>
    /// Permite a conversão implícita de um <see cref="Error"/> para um <see cref="EndpointResult{TValue}"/>.
    /// </summary>
    /// <param name="error">O <see cref="Error"/> a ser convertido.</param>
    /// <returns>Um <see cref="EndpointResult{TValue}"/> que encapsula a resposta HTTP correspondente.</returns>
    public static implicit operator EndpointResult<TValue>(Error error) =>
        new(new ErrorResult(error));
}
