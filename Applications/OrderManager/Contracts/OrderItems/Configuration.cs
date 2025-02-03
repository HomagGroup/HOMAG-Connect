using HomagConnect.Base.Contracts.Interfaces;
using Newtonsoft.Json;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// (Software) Configuration information.
/// </summary>
public class Configuration : Base, IConfiguration
{
    /// <inheritdoc />
    public override Type Type
    {
        get
        {
            return Type.Configuration;
        }
    }

    /// <summary>
    /// The id of this configuration module
    /// </summary>
    [JsonProperty(Order = 20)]
    public string? ModuleId { get; set; }


    /// <summary>
    /// Contains configuration attributes.
    /// </summary>
    [JsonProperty(Order = 50)]
    public IDictionary<string, object>? Attributes { get; set; } = new Dictionary<string, object>(StringComparer.Ordinal);
}