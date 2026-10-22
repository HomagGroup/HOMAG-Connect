using HomagConnect.OrderManager.Contracts.OrderItems;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace HomagConnect.OrderManager.Contracts.Interfaces
{
    /// <summary>
    /// Basic configuration data
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// The id of this configuration module
        /// </summary>
        [JsonProperty(Order = 20)]
        string? ModuleId { get; set; }

        /// <summary>
        /// The localized short name of the configuration module
        /// </summary>
        [JsonProperty(Order = 21)]
        string? ModuleShortName { get; set; }

        /// <summary>
        /// The localized description of the configuration module
        /// </summary>
        [JsonProperty(Order = 22)]
        string? ModuleDescription { get; set; }

        /// <summary>
        /// The image url of the configuration module
        /// </summary>
        [JsonProperty(Order = 23)]
        string? ModuleImage { get; set; }

        /// <summary>
        /// Contains configuration attributes.
        /// </summary>
        [JsonProperty(Order = 50)]
        Collection<ConfigurationAttribute>? Attributes { get; set; }
    }
}
