using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace LightningArc.Utils.Metalama;

/// <summary>
/// Represents the definition of a method parameter at compile time.
/// </summary>
[CompileTime]
public class ParameterDefinition
{
    /// <summary>
    /// Gets or sets the name of the parameter.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Gets or sets the type of the parameter.
    /// </summary>
    public IType Type { get; init; }

    /// <summary>
    /// Gets or sets the reference kind of the parameter (e.g., in, out, ref). Defaults to <see cref="RefKind.None"/>.
    /// </summary>
    public RefKind RefKind { get; set; } = RefKind.None;

    /// <summary>
    /// Gets or sets the default value of the parameter. Defaults to <see langword="null"/>.
    /// </summary>
    public TypedConstant? DefaultValue { get; set; } = null;

    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterDefinition"/> class/>.
    /// </summary>
    public ParameterDefinition()
    {
        Name = null!;
        Type = null!;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterDefinition"/> class with an <see cref="IType"/>.
    /// </summary>
    /// <param name="name">The name of the parameter.</param>
    /// <param name="type">The type of the parameter as an <see cref="IType"/>.</param>
    /// <param name="refKind">The reference kind of the parameter. Defaults to <see cref="RefKind.None"/>.</param>
    /// <param name="defaultValue">The default value of the parameter. Defaults to <see langword="null"/>.</param>
    public ParameterDefinition(
        string name,
        IType type,
        RefKind refKind = RefKind.None,
        TypedConstant? defaultValue = null
    )
    {
        Name = name;
        Type = type;
        RefKind = refKind;
        DefaultValue = defaultValue;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterDefinition"/> class with a <see cref="Type"/>.
    /// </summary>
    /// <param name="name">The name of the parameter.</param>
    /// <param name="type">The type of the parameter as a <see cref="Type"/>.</param>
    /// <param name="refKind">The reference kind of the parameter. Defaults to <see cref="RefKind.None"/>.</param>
    /// <param name="defaultValue">The default value of the parameter. Defaults to <see langword="null"/>.</param>
    public ParameterDefinition(
        string name,
        Type type,
        RefKind refKind = RefKind.None,
        TypedConstant? defaultValue = null
    )
    {
        Name = name;
        Type = TypeFactory.GetType(type);
        RefKind = refKind;
        DefaultValue = defaultValue;
    }
}
