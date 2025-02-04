using HomagConnect.Base.Contracts.Interfaces;
using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.OrderItems
{
    /// <summary>
    /// A root configuration position.
    /// </summary>
    public class ConfigurationPosition : Position, IConfiguration
    {
        /// <inheritdoc />
        public override Type Type
        {
            get
            {
                return Type.ConfigurationPosition;
            }
        }

        /// <summary>
        /// An library id
        /// </summary>
        [JsonProperty(Order = 10)]
        public string? LibraryId { get; set; }

        /// <summary>
        /// The id of this configuration module
        /// </summary>
        [JsonProperty(Order = 20)]
        public string? ModuleId { get; set; }

        /// <summary>
        /// An optional id of the group this module belongs to
        /// </summary>
        [JsonProperty(Order = 25)]
        public string? GroupId { get; set; }

        /// <summary>
        /// An optional position of this module (x, y, z)
        /// </summary>
        [JsonProperty(Order = 30)]
        public double[]? Position { get; set; }

        /// <summary>
        /// An optional rotation of this module (x, y, z)
        /// </summary>
        [JsonProperty(Order = 40)]
        public double[]? Rotation { get; set; }

        /// <summary>
        /// Contains configuration attributes.
        /// </summary>
        [JsonProperty(Order = 50)]
        public IDictionary<string, object>? Attributes { get; set; } = new Dictionary<string, object>(StringComparer.Ordinal);
    }
}
