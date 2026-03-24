using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HomagConnect.IntelliDivide.Contracts.Usage
{
    /// <summary>
    /// Describes usage for a specific period, including transferred parts and available licenses.
    /// </summary>
    /// <example>
    /// {
    ///   "period": "2025-03-01T00:00:00",
    ///   "partsTransferredQuantity": 240,
    ///   "licenses": [
    ///     { "licenseName": "HOMAG intelliDivide Cutting Premium", "partsCoveredQuantity": 5000 }
    ///   ]
    /// }
    /// </example>
    public class UsageOverview
    {
        /// <summary>
        /// Gets or sets the period covered by the usage overview.
        /// </summary>
        /// <example>2025-03-01T00:00:00</example>
        [JsonProperty(Order = 1)]
        public DateTime Period { get; set; }

        /// <summary>
        /// Gets or sets the number of parts transferred in the current period.
        /// </summary>
        /// <example>240</example>
        [JsonProperty(Order = 2)]
        public int? PartsTransferredQuantity { get; set; }

        /// <summary>
        /// Gets or sets the licenses owned in the current period.
        /// </summary>
        /// <example>
        /// [
        ///   { "licenseName": "HOMAG intelliDivide Cutting Premium", "partsCoveredQuantity": 5000 }
        /// ]
        /// </example>
        [JsonProperty(Order = 3)]
        public IEnumerable<LicenseDetails> Licenses { get; set; }
    }
}
