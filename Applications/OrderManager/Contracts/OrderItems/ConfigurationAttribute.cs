using HomagConnect.Base.Contracts.Interfaces;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Configuration attribute.
/// </summary>
public class ConfigurationAttribute
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public ConfigurationAttribute() { }

    /// <summary>
    /// Constructor which marks them as input by default.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <param name="isInput"></param>
    public ConfigurationAttribute(string name, object? value, bool? isInput = null)
    {
        Name = name;
        Value = value;
        IsInput = isInput;
    }

    /// <summary>
    /// The name of the attribute.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// An optional localized display name of the attribute.
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// Specifies if this is a main attribute for the configuration.
    /// </summary>
    public bool? IsMain { get; set; }

    /// <summary>
    /// Defines if the attribute is an input value or an output value.
    /// </summary>
    public bool? IsInput { get; set; }

    /// <summary>
    /// The value of the attribute.
    /// </summary>
    public object? Value { get; set; } = null!;
}