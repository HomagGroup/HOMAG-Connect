using Newtonsoft.Json;

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
        IDictionary<string, object>? Attributes { get; set; }
    }
}
