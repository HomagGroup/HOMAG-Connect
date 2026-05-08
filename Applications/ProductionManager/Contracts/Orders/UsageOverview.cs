using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using HomagConnect.Base.Contracts.Interfaces;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Orders 
{
    /// <summary>
    /// Represents license usage totals for a reporting period.
    /// </summary>
    /// <example>
    /// { "period": "2025-03-01T00:00:00Z", "licenses": [{ "applicationId": "7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c", "applicationLicenseFullName": "ProductionManager Professional", "licenseCount": 3 }], "quantityOfPartsCoveredByTheLicenses": 150000, "quantityOfReleasedParts": 126430 }
    /// </example>
    [Display(ResourceType = typeof(UsageOverviewPropertyDisplayNames), Name = nameof(UsageOverview))]
    public class UsageOverview : ISupportsLocalizedSerialization
    {
        /// <summary>
        /// Gets or sets the reporting period for the license usage overview.
        /// </summary>
        /// <example>2025-03-01T00:00:00Z</example>
        [Display(ResourceType = typeof(UsageOverviewPropertyDisplayNames), Name = nameof(Period))]
        [JsonProperty(Order = 1)]
        public DateTime Period { get; set; }

        /// <summary>
        /// Gets or sets the licenses available during the reporting period.
        /// </summary>
        /// <example>[{ "applicationId": "7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c", "applicationLicenseFullName": "ProductionManager Professional", "licenseCount": 3 }]</example>
        [Display(ResourceType = typeof(UsageOverviewPropertyDisplayNames), Name = nameof(Licenses))]
        [JsonProperty(Order = 2)]
        public Collection<License> Licenses { get; set; } = [];

        /// <summary>
        /// Gets or sets the total number of parts that may be released under the available licenses in the reporting period.
        /// </summary>
        /// <example>150000</example>
        [Display(ResourceType = typeof(UsageOverviewPropertyDisplayNames), Name = nameof(QuantityOfPartsCoveredByTheLicenses))]
        [JsonProperty(Order = 3)]
        public int QuantityOfPartsCoveredByTheLicenses { get; set; }

        /// <summary>
        /// Gets or sets the total number of parts that were released in the reporting period.
        /// </summary>
        /// <example>126430</example>
        [Display(ResourceType = typeof(UsageOverviewPropertyDisplayNames), Name = nameof(QuantityOfReleasedParts))]
        [JsonProperty(Order = 4)]
        public int QuantityOfReleasedParts { get; set; }
    }
}
