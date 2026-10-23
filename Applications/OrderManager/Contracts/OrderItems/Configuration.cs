using HomagConnect.OrderManager.Contracts.Interfaces;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

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
        set
        {
            // Ignore
        }
    }

    /// <summary>
    /// The id of this configuration module
    /// </summary>
    [JsonProperty(Order = 20)]
    public string? ModuleId { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 21)]
    public string? ModuleName { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 22)]
    public string? ModuleDescription { get; set; }

    /// <summary>
    /// The sub-article through which this configuration was added (if any)
    /// </summary>
    [JsonProperty(Order = 23)]
    public SubArticle? SubArticle { get; set; }

    /// <summary>
    /// Contains configuration attributes.
    /// </summary>
    [JsonProperty(Order = 50)]
    public Collection<ConfigurationAttribute>? Attributes { get; set; } = new Collection<ConfigurationAttribute>();
}
