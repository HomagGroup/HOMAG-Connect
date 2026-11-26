using HomagConnect.OrderManager.Contracts.Interfaces;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Software configuration of an article, identified by its module and carrying configuration attributes.
/// </summary>
/// <example>
/// { "type": "Configuration", "id": "1", "moduleId": "m1", "moduleName": "Carcase", "articleNumber": "BU-600", "attributes": [ { "name": "width", "value": 600, "isInput": true } ] }
/// </example>
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
    /// Id of the configuration module. Optional.
    /// </summary>
    /// <example>m1</example>
    [JsonProperty(Order = 20)]
    public string? ModuleId { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 21)]
    public string? ModuleName { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 22)]
    public string? ModuleDescription { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 23)]
    public string? ArticleNumber { get; set; }

    [JsonProperty(Order = 24)]
    /// <inheritdoc />
    public string? ArticleName { get; set; }

    [JsonProperty(Order = 25)]
    /// <inheritdoc />
    public string? Description { get; set; }

    [JsonProperty(Order = 26)]
    /// <inheritdoc />
    public string? ArticleCategory { get; set; }

    /// <summary>
    /// Configuration attributes of the module. Optional.
    /// </summary>
    [JsonProperty(Order = 50)]
    public Collection<ConfigurationAttribute>? Attributes { get; set; } = new Collection<ConfigurationAttribute>();
}