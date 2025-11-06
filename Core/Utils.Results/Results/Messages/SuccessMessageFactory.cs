using LightningArc.Utils.Results.Localization;

namespace LightningArc.Utils.Results.Messages;

internal static class SuccessMessageFactory
{
    internal static IMessageProvider CreateProvider(
        string? message,
        string defaultMessageResourceKey,
        object[]? formatArgs = null
    ) =>
        message is not null
            ? new LiteralMessageProvider(message, formatArgs)
            : new ResourceMessageProvider(
                defaultMessageResourceKey,
                LocalizationManager.GetSuccessString,
                formatArgs
            );
}
