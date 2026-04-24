using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Sales
{
    /// <summary>
    /// Represents a single attribute (parameter) of a <see cref="PosModuleData"/>.
    /// </summary>
    public class PosModuleAttribute
    {
        /// <summary>
        /// The id of the attribute
        /// See also <see cref="MDAttributeInfo"/> for more info about attributes
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = null!;

        /// <summary>
        /// The value of this attribute
        /// Can be string, decimal, double, bool
        /// </summary>
        [JsonProperty("value")]
        public object? Value { get; set; }

        /// <summary>
        /// Defines if the attribute was passed as input into the calculation
        /// </summary>
        [JsonProperty("isInput")]
        public bool? IsInput { get; set; }

        /// <summary>
        /// Gets or sets the additional properties configured in the application.
        /// </summary>
        [JsonExtensionData]
        [JsonProperty(Order = 999)]
        public IDictionary<string, object>? AdditionalProperties { get; set; }
    }
}
