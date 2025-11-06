using System.Globalization;
using LightningArc.Utils.Results.Messages;

namespace LightningArc.Utils.Results;

/// <summary>
/// Represents a generic success result.
/// This base class is used to standardize success responses, allowing subclasses
/// to define specific types of success.
/// </summary>
public abstract class Success
{
    /// <summary>
    /// Gets the numeric code of the success.
    /// </summary>
    public int Code { get; }

    /// <summary>
    /// Gets the message provider for this success.
    /// </summary>
    internal readonly IMessageProvider? MessageProvider;

    /// <summary>
    /// Gets the optional success message.
    /// </summary>
    public string? Message => MessageProvider?.GetMessage(CultureInfo.CurrentCulture);

    /// <summary>
    /// Protected constructor to initialize the base <see cref="Success"/> instance with a message provider.
    /// </summary>
    /// <param name="code">The numeric code of the success.</param>
    /// <param name="messageProvider">The message provider for this success.</param>
    protected Success(int code, IMessageProvider? messageProvider)
    {
        if (code <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(code),
                "Success code must be a positive integer."
            );
        }

        Code = code;
        MessageProvider = messageProvider;
    }

    /// <param name="code">The numeric code of the success.</param>
    /// <param name="message">The optional literal success message. Will be used to create a LiteralMessageProvider.</param>
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
        MessageProvider = message is not null ? new LiteralMessageProvider(message) : null;
    }

    /// <summary>
    /// Creates a success instance with the generic code for "OK".
    /// </summary>
    /// <param name="message">The optional success message. If not provided, the default localized message will be used.</param>
    /// <returns>A new <see cref="Success"/> instance.</returns>
    public static Success Ok(string? message = null) =>
        new OkSuccess(SuccessMessageFactory.CreateProvider(message, "Success_Ok"));

    /// <summary>
    /// Creates a success instance with the generic code for "Created".
    /// </summary>
    /// <param name="message">The optional success message. If not provided, the default localized message will be used.</param>
    /// <returns>A new <see cref="Success"/> instance.</returns>
    public static Success Created(string? message = null) =>
        new CreatedSuccess(SuccessMessageFactory.CreateProvider(message, "Success_Created"));

    /// <summary>
    /// Creates a success instance with the generic code for "Accepted".
    /// </summary>
    /// <param name="message">The optional success message. If not provided, the default localized message will be used.</param>
    /// <returns>A new <see cref="Success"/> instance.</returns>
    public static Success Accepted(string? message = null) =>
        new AcceptedSuccess(SuccessMessageFactory.CreateProvider(message, "Success_Accepted"));

    /// <summary>
    /// Creates a success instance with the generic code for "No Content".
    /// </summary>
    /// <param name="message">The optional success message. If not provided, the default localized message will be used.</param>
    /// <returns>A new <see cref="Success"/> instance.</returns>
    public static Success NoContent(string? message = null) =>
        new NoContentSuccess(SuccessMessageFactory.CreateProvider(message, "Success_NoContent"));

    /// <summary>
    /// Creates a success instance with the generic code for "OK" and encapsulates a value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to be encapsulated in the success.</typeparam>
    /// <param name="value">The value to be encapsulated in the success.</param>
    /// <param name="message">The optional success message. If not provided, the default localized message will be used.</param>
    /// <returns>A new <see cref="Success{TValue}"/> instance.</returns>
    public static Success<TValue> Ok<TValue>(
        TValue value,
        string? message = null
    ) =>
        new Success<TValue>.OkSuccess(
            value,
            SuccessMessageFactory.CreateProvider(message, "Success_Ok")
        );

    /// <summary>
    /// Creates a success instance with the generic code for "Created" and encapsulates a value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to be encapsulated in the success.</typeparam>
    /// <param name="value">The value to be encapsulated in the success.</param>
    /// <param name="message">The optional success message. If not provided, the default localized message will be used.</param>
    /// <returns>A new <see cref="Success{TValue}"/> instance.</returns>
    public static Success<TValue> Created<TValue>(
        TValue value,
        string? message = null
    ) =>
        new Success<TValue>.CreatedSuccess(
            value,
            SuccessMessageFactory.CreateProvider(message, "Success_Created")
        );

    /// <summary>
    /// Creates a success instance with the generic code for "Accepted" and encapsulates a value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to be encapsulated in the success.</typeparam>
    /// <param name="value">The value to be encapsulated in the success.</param>
    /// <param name="message">The optional success message. If not provided, the default localized message will be used.</param>
    /// <returns>A new <see cref="Success{TValue}"/> instance.</returns>
    public static Success<TValue> Accepted<TValue>(
        TValue value,
        string? message = null
    ) =>
        new Success<TValue>.AcceptedSuccess(
            value,
            SuccessMessageFactory.CreateProvider(message, "Success_Accepted")
        );

    /// <summary>
    /// Creates a success instance with the generic code for "No Content".
    /// </summary>
    /// <typeparam name="TValue">The type of the value to be encapsulated in the success.</typeparam>
    /// <param name="value">The success value.</param>
    /// <param name="message">The optional success message. If not provided, the default localized message will be used.</param>
    /// <returns>A new <see cref="Success"/> instance.</returns>
    public static Success<TValue> NoContent<TValue>(TValue value, string? message = null) =>
        new Success<TValue>.NoContentSuccess(
            value,
            SuccessMessageFactory.CreateProvider(message, "Success_NoContent")
        );

    /// <summary>
    /// Creates a new typed success (<see cref="Success{TValue}"/>) based on the metadata
    /// of this success (code and message), adding a typed value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to be encapsulated in the new success.</typeparam>
    /// <param name="value">The value to be encapsulated.</param>
    /// <returns>A new <see cref="Success{TValue}"/> instance with the same metadata.</returns>
    public abstract Success<TValue> WithValue<TValue>(TValue value);

    /// <summary>
    /// Represents the success type for an "Ok" operation.
    /// </summary>
    internal sealed class OkSuccess : Success
    {
        internal OkSuccess(IMessageProvider? messageProvider)
            : base(100, messageProvider) { }

        /// <inheritdoc/>
        public override Success<TValue> WithValue<TValue>(TValue value) =>
            new Success<TValue>.OkSuccess(value, MessageProvider);
    }

    /// <summary>
    /// Represents the success type for a "Created" operation.
    /// </summary>
    internal sealed class CreatedSuccess : Success
    {
        internal CreatedSuccess(IMessageProvider? messageProvider)
            : base(101, messageProvider) { }

        /// <inheritdoc/>
        public override Success<TValue> WithValue<TValue>(TValue value) =>
            new Success<TValue>.CreatedSuccess(value, MessageProvider);
    }

    /// <summary>
    /// Represents the success type for an "Accepted" operation.
    /// </summary>
    internal sealed class AcceptedSuccess : Success
    {
        internal AcceptedSuccess(IMessageProvider? messageProvider)
            : base(102, messageProvider) { }

        /// <inheritdoc/>
        public override Success<TValue> WithValue<TValue>(TValue value) =>
            new Success<TValue>.AcceptedSuccess(value, MessageProvider);
    }

    /// <summary>
    /// Represents the success type for a "No Content" operation.
    /// </summary>
    public sealed class NoContentSuccess : Success
    {
        internal NoContentSuccess(IMessageProvider? messageProvider)
            : base(103, messageProvider) { }

        /// <inheritdoc/>
        public override Success<TValue> WithValue<TValue>(TValue value) =>
            new Success<TValue>.NoContentSuccess(value, MessageProvider);
    }
}

/// <summary>
/// Represents a success result that encapsulates a specific value along with success metadata.
/// This class extends <see cref="Success"/> to provide a typed value.
/// </summary>
/// <typeparam name="TValue">The type of the success value that this object encapsulates.</typeparam>
public abstract class Success<TValue> : Success
{
    /// <summary>
    /// Gets the encapsulated success value.
    /// </summary>
    public TValue Value { get; }

    /// <remarks>
    /// Internal constructor to initialize the <see cref="Success{TValue}"/> instance.
    /// </remarks>
    /// <param name="code">The numeric code of the success.</param>
    /// <param name="messageProvider">The message provider for this success.</param>
    /// <param name="value">The success value to be encapsulated.</param>
    internal Success(int code, IMessageProvider? messageProvider, TValue value)
        : base(code, messageProvider)
    {
        Value = value;
    }

    /// <remarks>
    /// Internal constructor to initialize the <see cref="Success{TValue}"/> instance
    /// from a non-generic <see cref="Success"/>.
    /// </remarks>
    /// <param name="existingSuccess">The existing <see cref="Success"/> object.</param>
    /// <param name="value">The success value to be encapsulated.</param>
    internal Success(Success existingSuccess, TValue value)
        : base(existingSuccess.Code, existingSuccess.MessageProvider)
    {
        // Este construtor é chamado pelas subclasses internas (OkSuccess, CreatedSuccess, etc.)
        Value = value;
    }

    /// <summary>
    /// Represents the success type for an "Ok" operation with a <typeparamref name="TValue"/> value.
    /// </summary>
    internal new sealed class OkSuccess : Success<TValue>
    {
        /// <param name="value">The success value.</param>
        /// <param name="messageProvider">The optional message provider.</param>
        internal OkSuccess(TValue value, IMessageProvider? messageProvider)
            : base(100, messageProvider, value) { }

        internal OkSuccess(int code, IMessageProvider? messageProvider, TValue value) // Para códigos customizados
            : base(code, messageProvider, value) { }

        internal OkSuccess(Success existingSuccess, TValue value)
            : base(existingSuccess, value) { }

        /// <inheritdoc/>
        public override Success<TMappedValue> WithValue<TMappedValue>(TMappedValue value) =>
            new Success<TMappedValue>.OkSuccess(value, MessageProvider);
    }

    /// <summary>
    /// Represents the success type for a "Created" operation with a <typeparamref name="TValue"/> value.
    /// </summary>
    internal new sealed class CreatedSuccess : Success<TValue>
    {
        /// <param name="value">The success value.</param>
        /// <param name="messageProvider">The optional message provider.</param>
        internal CreatedSuccess(TValue value, IMessageProvider? messageProvider)
            : base(101, messageProvider, value) { }

        internal CreatedSuccess(Success existingSuccess, TValue value)
            : base(existingSuccess, value) { }

        /// <inheritdoc/>
        public override Success<TMappedValue> WithValue<TMappedValue>(TMappedValue value) =>
            new Success<TMappedValue>.CreatedSuccess(value, MessageProvider);
    }

    /// <summary>
    /// Represents the success type for an "Accepted" operation with a <typeparamref name="TValue"/> value.
    /// </summary>
    internal new sealed class AcceptedSuccess : Success<TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptedSuccess"/> class.
        /// </summary>
        /// <param name="value">The success value.</param>
        /// <param name="messageProvider">The optional message provider.</param>
        internal AcceptedSuccess(TValue value, IMessageProvider? messageProvider)
            : base(102, messageProvider, value) { }

        internal AcceptedSuccess(Success existingSuccess, TValue value)
            : base(existingSuccess, value) { }

        /// <inheritdoc/>
        public override Success<TMappedValue> WithValue<TMappedValue>(TMappedValue value) =>
            new Success<TMappedValue>.AcceptedSuccess(value, MessageProvider);
    }

    /// <summary>
    /// Represents the success type for a "No Content" operation.
    /// </summary>
    internal new sealed class NoContentSuccess : Success<TValue>
    {
        internal NoContentSuccess(TValue value, IMessageProvider? messageProvider)
            : base(103, messageProvider, value) { }

        internal NoContentSuccess(Success existingSuccess, TValue value)
            : base(existingSuccess, value) { }

        /// <inheritdoc/>
        public override Success<TMappedValue> WithValue<TMappedValue>(TMappedValue value) =>
            new Success<TMappedValue>.NoContentSuccess(value, MessageProvider);
    }
}
