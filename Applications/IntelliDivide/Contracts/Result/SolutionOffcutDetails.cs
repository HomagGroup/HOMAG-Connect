#nullable enable

using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Provides information about a specific offcut.
    /// </summary>
    /// <example>
    /// {
    ///   "id": "XID-1000000",
    ///   "quantity": 3,
    /// }
    /// </example>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionOffcutDetails
    {
        /// <summary>
        /// Gets or sets the offcut id.
        /// </summary>
        [JsonProperty(Order = 1)]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the offcut quantity.
        /// </summary>
        [JsonProperty(Order = 2)]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
