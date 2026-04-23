using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Sales
{
    /// <summary>
    /// Represents a module within an article, including its sub modules and attributes.
    /// </summary>
    public class PosModuleData
    {
        /// <summary>
        /// An unique id of this module; this is also stable across multiple calculation calls
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = null!;

        /// <summary>
        /// The name of this module; this is NOT unique
        /// It is internally needed to calculate the article
        /// See also <see cref="MDModuleInfo"/> for more info about modules
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// possible sub modules
        /// </summary>
        [JsonProperty("modules")]
        public IList<PosModuleData>? Modules { get; set; }

        /// <summary>
        /// These are the parameters of this module
        /// </summary>
        [JsonProperty("attributes")]
        public IList<PosModuleAttribute>? Attributes { get; set; }
    }
}
