using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.OrderItems
{
    /// <summary>
    /// A configuration group.
    /// </summary>
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
        /// Gets or sets the contour information for this group
        /// which is the surrounding contour of the articles in this group.
        /// </summary>
        public string? ContourInformation { get; set; }

        /// <summary>
        /// The position of this group inside the room (x, y, z)
        /// </summary>
        [JsonProperty(Order = 30)]
        public double[]? Position { get; set; }

        /// <summary>
        /// The rotation of this group inside the room (x, y, z)
        /// </summary>
        [JsonProperty(Order = 40)]
        public double[]? Rotation { get; set; }
    }
}
