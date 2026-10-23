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
        /// The localized name of the configuration module
        /// </summary>
        [JsonProperty(Order = 21)]
        string? ModuleName { get; set; }

        /// <summary>
        /// The localized description of the configuration module
        /// </summary>
        [JsonProperty(Order = 22)]
        string? ModuleDescription { get; set; }

        /// <summary>
        /// The sub-article through which this configuration was added (if any)
        /// </summary>
        [JsonProperty(Order = 23)]
        public SubArticle? SubArticle { get; set; }

        /// <summary>
        /// Contains configuration attributes.
        /// </summary>
        [JsonProperty(Order = 50)]
        Collection<ConfigurationAttribute>? Attributes { get; set; }
    }
}
