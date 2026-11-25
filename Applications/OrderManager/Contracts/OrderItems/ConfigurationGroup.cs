using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace HomagConnect.OrderManager.Contracts.OrderItems
{
    /// <summary>
    /// Group of configured articles, with its position and rotation inside the room.
    /// </summary>
    /// <example>
    /// { "type": "ConfigurationGroup", "id": "G1", "position": [0, 0, 0], "rotation": [0, 90, 0], "attributes": [ { "name": "width", "value": 600, "isInput": true } ] }
    /// </example>
    public class ConfigurationGroup : Base
    {
        /// <inheritdoc />
        public override Type Type
        {
            get
            {
                return Type.ConfigurationGroup;
            }
            set
            {
                // Ignore
            }
        }

        /// <summary>
        /// Surrounding contour of the articles in the group. Optional. The data structure is defined separately.
        /// </summary>
        [JsonProperty(Order = 20)]
        public string? ContourInformation { get; set; }

        /// <summary>
        /// Position (x, y, z) of the group inside the room. Optional.
        /// </summary>
        /// <example>[0, 0, 0]</example>
        [JsonProperty(Order = 30)]
        public double[]? Position { get; set; }

        /// <summary>
        /// Rotation (x, y, z) of the group inside the room. Optional.
        /// </summary>
        /// <example>[0, 90, 0]</example>
        [JsonProperty(Order = 40)]
        public double[]? Rotation { get; set; }

        /// <summary>
        /// Configuration attributes of the group. Optional.
        /// </summary>
        [JsonProperty(Order = 50)]
        public Collection<ConfigurationAttribute>? Attributes { get; set; } = new Collection<ConfigurationAttribute>();
    }
}
