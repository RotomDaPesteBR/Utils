using System.Text;
using LightningArc.Utils.Results.AspNet.Interfaces;

namespace LightningArc.Utils.Results.AspNet.Services;

/// <summary>
/// A default implementation of <see cref="IErrorListFormatter"/> that formats the error list as a Markdown table.
/// </summary>
internal sealed class MarkdownErrorListFormatter : IErrorListFormatter
{
    public string Format(IEnumerable<ErrorMetadata> errors)
    {
        var markdownBuilder = new StringBuilder();
        markdownBuilder.AppendLine("# Error List");
        markdownBuilder.AppendLine();

        var groupedByModule = errors.GroupBy(e => e.Module).OrderBy(g => g.Key);

        foreach (var moduleGroup in groupedByModule)
        {
            markdownBuilder.AppendLine($"## {moduleGroup.Key}");
            markdownBuilder.AppendLine();

            markdownBuilder.AppendLine("| Code | Name | HTTP Status | Message |");
            markdownBuilder.AppendLine("|:---|:---|:---|:---|");

            foreach (var error in moduleGroup.OrderBy(e => e.Code))
            {
                var httpStatus = error.HttpStatusCode.HasValue ? ((int)error.HttpStatusCode.Value).ToString() : "N/A";
                markdownBuilder.AppendLine($"| `{error.Code:D5}` | `{error.Name}` | `{httpStatus}` | {error.Message} |");
            }
            markdownBuilder.AppendLine();
        }

        return markdownBuilder.ToString();
    }
}
