using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Interfaces;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Name and value of a configuration attribute of an article or module.
/// </summary>
/// <example>
/// { "name": "Width", "displayName": "Width", "value": 600, "isInput": true, "isMain": true }
/// </example>
public class ConfigurationAttribute : ISupportsAdditionalData
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
    /// Technical name of the attribute. Required.
    /// </summary>
    /// <example>Width</example>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Localized display name of the attribute. Optional.
    /// </summary>
    /// <example>Width</example>
    public string? DisplayName { get; set; }

    /// <summary>
    /// Localized category of the attribute. Optional.
    /// </summary>
    /// <example>Dimensions</example>
    public string? Category { get; set; }

    /// <summary>
    /// <c>true</c> if this is a main attribute of the configuration. Optional.
    /// </summary>
    /// <example>true</example>
    public bool? IsMain { get; set; }

    /// <summary>
    /// Specifies whether the attribute is relevant for the configuration in the point of sale (POS) system.
    /// If <c>false</c>, the attribute is used internally only.
    /// </summary>
    /// <example>true</example>
    public bool? IsPosRelevant { get; set; }

    /// <summary>
    /// <c>true</c> if the attribute is an input value, <c>false</c> if it is an output value. Optional.
    /// </summary>
    /// <example>true</example>
    public bool? IsInput { get; set; }

    /// <summary>
    /// Value of the attribute. Can be a string, number or boolean. Optional.
    /// </summary>
    /// <example>600</example>
    public object? Value { get; set; } = null!;

    /// <summary>
    /// Localized display value of the attribute. Optional. Not used for numbers.
    /// </summary>
    /// <example>Oak</example>
    public string? DisplayValue { get; set; }

    /// <summary>
    /// Sort position of the attribute within its category. Optional.
    /// </summary>
    /// <example>10</example>
    public int? Sorting { get; set; }

    /// <summary>
    /// User right required to see the attribute: <c>Master</c>, <c>Advanced</c> or <c>Simple</c>. Optional.
    /// </summary>
    /// <example>Simple</example>
    [JsonProperty("userRight")]
    public string? UserRight { get; set; }

    /// <summary>
    /// Specifies if the attribute was marked as hidden during creation.
    /// </summary>
    /// <example>false</example>
    [JsonProperty("isHidden")]
    public bool? IsHidden { get; set; }


    /// <summary>
    /// Additional data.
    /// </summary>
    [JsonProperty(Order = 997)]
    public Collection<AdditionalDataEntity>? AdditionalData { get; set; }

    /// <summary>
    /// Additional properties configured in the application.
    /// </summary>
    [JsonExtensionData]
    [JsonProperty(Order = 999)]
    public IDictionary<string, object>? AdditionalProperties { get; set; }
}