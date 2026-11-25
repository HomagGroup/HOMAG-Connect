using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Sales
{
    /// <summary>
    /// Represents a single attribute (parameter) of a <see cref="PosModuleData"/>.
    /// </summary>
    /// <example>
    /// { "id": "width", "value": 600, "isInput": true }
    /// </example>
    public class PosModuleAttribute
    {
        /// <summary>
        /// The id of the attribute
        /// See also <see cref="MDAttributeInfo"/> for more info about attributes
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = null!;

        /// <summary>
        /// Value of the attribute. Can be a string, decimal, double or boolean.
        /// </summary>
        /// <example>600</example>
        [JsonProperty("value")]
        public object? Value { get; set; }

        /// <summary>
        /// <c>true</c> if the attribute was passed as input into the calculation. Optional.
        /// </summary>
        /// <example>true</example>
        [JsonProperty("isInput")]
        public bool? IsInput { get; set; }

        /// <summary>
        /// Additional properties configured in the application.
        /// </summary>
        [JsonExtensionData]
        [JsonProperty(Order = 999)]
        public IDictionary<string, object>? AdditionalProperties { get; set; }
    }
}
