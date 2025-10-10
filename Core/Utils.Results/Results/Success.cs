namespace LightningArc.Utils.Results;

/// <summary>
/// Representa um resultado de sucesso genérico.
/// Esta classe base é usada para padronizar as respostas de sucesso, permitindo que as subclasses
/// definam tipos específicos de sucesso.
/// </summary>
public abstract class Success
{
    /// <summary>
    /// Obtém o código numérico do sucesso.
    /// </summary>
    public int Code { get; }

    /// <summary>
    /// Obtém a mensagem de sucesso opcional.
    /// </summary>
    public string? Message { get; }

    /// <remarks>
    /// Construtor protegido para inicializar a instância base de <see cref="Success"/>.
    /// </remarks>
    /// <param name="code">O código numérico do sucesso.</param>
    /// <param name="message">A mensagem opcional de sucesso.</param>
    protected Success(int code, string? message)
    {
        if (code <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(code),
                "Success code must be a positive integer."
            );
        }

        Code = code;
        Message = message;
    }

    /// <summary>
    /// Cria uma instância de sucesso com o código genérico para "OK".
    /// </summary>
    /// <param name="message">A mensagem opcional de sucesso. O padrão é "Operação concluída com sucesso."</param>
    /// <returns>Uma nova instância de <see cref="Success"/>.</returns>
    public static Success Ok(string? message = "Operação concluída com sucesso.") =>
        new OkSuccess(message);

    /// <summary>
    /// Cria uma instância de sucesso com o código genérico para "Created".
    /// </summary>
    /// <param name="message">A mensagem opcional de sucesso. O padrão é "Recurso criado com sucesso."</param>
    /// <returns>Uma nova instância de <see cref="Success"/>.</returns>
    public static Success Created(string? message = "Recurso criado com sucesso.") =>
        new CreatedSuccess(message);

    /// <summary>
    /// Cria uma instância de sucesso com o código genérico para "Accepted".
    /// </summary>
    /// <param name="message">A mensagem opcional de sucesso. O padrão é "A solicitação foi aceita para processamento."</param>
    /// <returns>Uma nova instância de <see cref="Success"/>.</returns>
    public static Success Accepted(
        string? message = "A solicitação foi aceita para processamento."
    ) => new AcceptedSuccess(message);

    /// <summary>
    /// Cria uma instância de sucesso com o código genérico para "No Content".
    /// </summary>
    /// <param name="message">A mensagem opcional de sucesso. O padrão é null, pois esta resposta geralmente não deve ter corpo.</param>
    /// <returns>Uma nova instância de <see cref="Success"/>.</returns>
    public static Success NoContent(string? message = null) => new NoContentSuccess(message);

    /// <summary>
    /// Cria uma instância de sucesso com o código genérico para "OK" e encapsula um valor.
    /// </summary>
    /// <param name="value">O valor a ser encapsulado no sucesso.</param>
    /// <param name="message">A mensagem opcional de sucesso. O padrão é "Operação concluída com sucesso."</param>
    /// <returns>Uma nova instância de <see cref="Success{TValue}"/>.</returns>
    public static Success<TValue> Ok<TValue>(
        TValue value,
        string? message = "Operação concluída com sucesso."
    ) => new Success<TValue>.OkSuccess(value, message);

    /// <summary>
    /// Cria uma instância de sucesso com o código genérico para "Created" e encapsula um valor.
    /// </summary>
    /// <param name="value">O valor a ser encapsulado no sucesso.</param>
    /// <param name="message">A mensagem opcional de sucesso. O padrão é "Recurso criado com sucesso."</param>
    /// <returns>Uma nova instância de <see cref="Success{TValue}"/>.</returns>
    public static Success<TValue> Created<TValue>(
        TValue value,
        string? message = "Recurso criado com sucesso."
    ) => new Success<TValue>.CreatedSuccess(value, message);

    /// <summary>
    /// Cria uma instância de sucesso com o código genérico para "Accepted" e encapsula um valor.
    /// </summary>
    /// <param name="value">O valor a ser encapsulado no sucesso.</param>
    /// <param name="message">A mensagem opcional de sucesso. O padrão é "A solicitação foi aceita para processamento."</param>
    /// <returns>Uma nova instância de <see cref="Success{TValue}"/>.</returns>
    public static Success<TValue> Accepted<TValue>(
        TValue value,
        string? message = "A solicitação foi aceita para processamento."
    ) => new Success<TValue>.AcceptedSuccess(value, message);

    /// <summary>
    /// Cria uma instância de sucesso com o código genérico para "No Content".
    /// </summary>
    /// <param name="value">O valor de sucesso.</param>
    /// <param name="message">A mensagem opcional de sucesso. O padrão é null, pois esta resposta geralmente não deve ter corpo.</param>
    /// <returns>Uma nova instância de <see cref="Success"/>.</returns>
    public static Success<TValue> NoContent<TValue>(TValue value, string? message = null) =>
        new Success<TValue>.NoContentSuccess(value, message);

    /// <summary>
    /// Cria um novo sucesso tipado (<see cref="Success{TValue}"/>) com base nos metadados 
    /// deste sucesso (código e mensagem), adicionando um valor tipado.
    /// </summary>
    /// <typeparam name="TValue">O tipo do valor a ser encapsulado no novo sucesso.</typeparam>
    /// <param name="value">O valor a ser encapsulado.</param>
    /// <returns>Uma nova instância de <see cref="Success{TValue}"/> com os mesmos metadados.</returns>
    public abstract Success<TValue> WithValue<TValue>(TValue value);

    /// <summary>
    /// Representa o tipo de sucesso para uma operação "Ok".
    /// </summary>
    public sealed class OkSuccess : Success
    {
        internal OkSuccess(string? message = null)
            : base(100, message) { }

        /// <inheritdoc/>
        public override Success<TValue> WithValue<TValue>(TValue value) =>
            new Success<TValue>.OkSuccess(value, Message);
    }

    /// <summary>
    /// Representa o tipo de sucesso para uma operação "Created".
    /// </summary>
    public sealed class CreatedSuccess : Success
    {
        internal CreatedSuccess(string? message = null)
            : base(101, message) { }

        /// <inheritdoc/>
        public override Success<TValue> WithValue<TValue>(TValue value) =>
            new Success<TValue>.CreatedSuccess(value, Message);
    }

    /// <summary>
    /// Representa o tipo de sucesso para uma operação "Accepted".
    /// </summary>
    public sealed class AcceptedSuccess : Success
    {
        internal AcceptedSuccess(string? message = null)
            : base(102, message) { }

        /// <inheritdoc/>
        public override Success<TValue> WithValue<TValue>(TValue value) =>
            new Success<TValue>.AcceptedSuccess(value, Message);
    }

    /// <summary>
    /// Representa o tipo de sucesso para uma operação "No Content".
    /// </summary>
    public sealed class NoContentSuccess : Success
    {
        internal NoContentSuccess(string? message = null)
            : base(103, message) { }

        /// <inheritdoc/>
        public override Success<TValue> WithValue<TValue>(TValue value) =>
            new Success<TValue>.NoContentSuccess(value, Message);
    }
}

/// <summary>
/// Representa um resultado de sucesso que encapsula um valor específico junto com os metadados de sucesso.
/// Esta classe estende <see cref="Success"/> para fornecer um valor tipado.
/// </summary>
/// <typeparam name="TValue">O tipo do valor de sucesso que este objeto encapsula.</typeparam>
public abstract class Success<TValue> : Success
{
    /// <summary>
    /// Obtém o valor de sucesso encapsulado.
    /// </summary>
    public TValue Value { get; }

    /// <remarks>
    /// Construtor interno para inicializar a instância de <see cref="Success{TValue}"/>.
    /// </remarks>
    /// <param name="code">O código numérico do sucesso.</param>
    /// <param name="message">A mensagem opcional de sucesso.</param>
    /// <param name="value">O valor de sucesso a ser encapsulado.</param>
    internal Success(int code, string? message, TValue value)
        : base(code, message)
    {
        Value = value;
    }

    /// <remarks>
    /// Construtor interno para inicializar a instância de <see cref="Success{TValue}"/> 
    /// a partir de um <see cref="Success"/> não genérico.
    /// </remarks>
    /// <param name="existingSuccess">O objeto <see cref="Success"/> existente.</param>
    /// <param name="value">O valor de sucesso a ser encapsulado.</param>
    internal Success(Success existingSuccess, TValue value)
        : base(existingSuccess.Code, existingSuccess.Message)
    {
        // Este construtor é chamado pelas subclasses internas (OkSuccess, CreatedSuccess, etc.)
        Value = value;
    }

    /// <summary>
    /// Representa o tipo de sucesso para uma operação "Ok" com um valor de <typeparamref name="TValue"/>.
    /// </summary>
    public new sealed class OkSuccess : Success<TValue>
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="OkSuccess"/>.
        /// </summary>
        /// <param name="value">O valor de sucesso.</param>
        /// <param name="message">A mensagem opcional.</param>
        internal OkSuccess(TValue value, string? message = null)
            : base(100, message, value) { }

        internal OkSuccess(int code, string? message, TValue value) // Para códigos customizados
            : base(code, message, value) { }

        internal OkSuccess(Success existingSuccess, TValue value)
            : base(existingSuccess, value) { }

        /// <inheritdoc/>
        public override Success<TMappedValue> WithValue<TMappedValue>(TMappedValue value) =>
            new Success<TMappedValue>.OkSuccess(value, Message);
    }

    /// <summary>
    /// Representa o tipo de sucesso para uma operação "Created" com um valor de <typeparamref name="TValue"/>.
    /// </summary>
    public new sealed class CreatedSuccess : Success<TValue>
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CreatedSuccess"/>.
        /// </summary>
        /// <param name="value">O valor de sucesso.</param>
        /// <param name="message">A mensagem opcional.</param>
        internal CreatedSuccess(TValue value, string? message = null)
            : base(101, message, value) { }

        internal CreatedSuccess(Success existingSuccess, TValue value)
            : base(existingSuccess, value) { }

        /// <inheritdoc/>
        public override Success<TMappedValue> WithValue<TMappedValue>(TMappedValue value) =>
            new Success<TMappedValue>.CreatedSuccess(value, Message);
    }

    /// <summary>
    /// Representa o tipo de sucesso para uma operação "Accepted" com um valor de <typeparamref name="TValue"/>.
    /// </summary>
    public new sealed class AcceptedSuccess : Success<TValue>
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="AcceptedSuccess"/>.
        /// </summary>
        /// <param name="value">O valor de sucesso.</param>
        /// <param name="message">A mensagem opcional.</param>
        internal AcceptedSuccess(TValue value, string? message = null)
            : base(102, message, value) { }

        internal AcceptedSuccess(Success existingSuccess, TValue value)
            : base(existingSuccess, value) { }

        /// <inheritdoc/>
        public override Success<TMappedValue> WithValue<TMappedValue>(TMappedValue value) =>
            new Success<TMappedValue>.AcceptedSuccess(value, Message);
    }

    /// <summary>
    /// Representa o tipo de sucesso para uma operação "No Content".
    /// </summary>
    public new sealed class NoContentSuccess : Success<TValue>
    {
        internal NoContentSuccess(TValue value, string? message = null)
            : base(103, message, value) { }

        internal NoContentSuccess(Success existingSuccess, TValue value)
            : base(existingSuccess, value) { }

        /// <inheritdoc/>
        public override Success<TMappedValue> WithValue<TMappedValue>(TMappedValue value) =>
            new Success<TMappedValue>.NoContentSuccess(value, Message);
    }
}
