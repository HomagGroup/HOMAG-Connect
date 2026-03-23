using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Usage
{
    /// <summary>
    /// Describes a license and the number of parts covered by it.
    /// </summary>
    /// <example>
    /// { "licenseName": "HOMAG intelliDivide Cutting Premium", "partsCoveredQuantity": 5000 }
    /// </example>
    public class LicenseDetails
    {
        /// <summary>
        /// Gets or sets the name of the license.
        /// </summary>
        /// <example>HOMAG intelliDivide Cutting Premium</example>
        [JsonProperty(Order = 1)]
        public string LicenseName { get; set; }

        /// <summary>
        /// Gets or sets the number of parts covered by the license.
        /// </summary>
        /// <example>5000</example>
        [JsonProperty(Order = 2)]
        public int PartsCoveredQuantity { get; set; }
    }
}
