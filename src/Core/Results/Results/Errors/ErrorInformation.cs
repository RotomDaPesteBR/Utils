namespace LightningArc.Results.Errors;

/// <summary>
/// Class to encapsulate the details of a specific error.
/// </summary>
public class ErrorInformation
{
    /// <summary>
    /// The unique numerical code of the error.
    /// </summary>
    public int Code { get; init; }

    /// <summary>
    /// The name of the factory method that creates the error.
    /// </summary>
    public string Name { get; init; } = "Erro";

    /// <summary>
    /// The default message associated with this error.
    /// </summary>
    public string Message { get; init; } = "";
}

