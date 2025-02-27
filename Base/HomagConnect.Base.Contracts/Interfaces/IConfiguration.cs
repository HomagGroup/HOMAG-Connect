using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace HomagConnect.Base.Contracts.Interfaces
{
    /// <summary>
    /// Basic configiration data
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// The id of this configuration module
        /// </summary>
        [JsonProperty(Order = 20)]
        string? ModuleId { get; set; }


        /// <summary>
        /// Contains configuration attributes.
        /// </summary>
        [JsonProperty(Order = 50)]
        Collection<IConfigurationAttribute>? Attributes { get; set; }
    }
}
