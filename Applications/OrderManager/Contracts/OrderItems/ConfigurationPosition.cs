using HomagConnect.OrderManager.Contracts.Interfaces;
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
            set
            {
                // Ignore
            }
        }

        /// <summary>
        /// Id of the library the configuration belongs to. Optional.
        /// </summary>
        /// <example>kitchen-lib</example>
        [JsonProperty(Order = 10)]
        public string? LibraryId { get; set; }

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

        /// <summary>
        /// Id of the group the module belongs to. Optional.
        /// </summary>
        /// <example>g1</example>
        [JsonProperty(Order = 25)]
        public string? GroupId { get; set; }

        /// <summary>
        /// An optional parameter that is used reflect a link to 
        /// a <seealso cref="Configuration"/>.<seealso cref="Base.Id"/> from 
        /// another <seealso cref="ConfigurationPosition"/> or their sub-<seealso cref="Configuration"/>
        /// </summary>
        /// <remarks>
        /// This is used to reflect a position for a sub-article
        /// </remarks>
        [JsonProperty(Order = 20)]
        public string? LinkedConfigurationId { get; set; }

        /// <summary>
        /// Position (x, y, z) of the module. Optional.
        /// </summary>
        /// <example>[0, 0, 0]</example>
        [JsonProperty(Order = 30)]
        public double[]? Position { get; set; }

        /// <summary>
        /// Rotation (x, y, z) of the module. Optional.
        /// </summary>
        /// <example>[0, 90, 0]</example>
        [JsonProperty(Order = 40)]
        public double[]? Rotation { get; set; }

        /// <summary>
        /// Contains the optional context data for this module. The data structure is separately defined.
        /// It is needed i.e. for the correct handling of docked modules; it contains the docking vector and the position of the docked modules in relation to the root module.
        /// </summary>
        [JsonProperty(Order = 60)]
        public string? ContextData { get; set; }
    }
}
