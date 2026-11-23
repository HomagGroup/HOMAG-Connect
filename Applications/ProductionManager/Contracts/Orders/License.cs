using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionManager.Contracts.Orders
{
    /// <summary>
    /// Represents license information assigned to an application within a usage overview.
    /// </summary>
    /// <example>
    /// { "applicationId": "7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c", "applicationLicenseFullName": "ProductionManager Professional", "licenseCount": 3 }
    /// </example>
    public class License
    {
        /// <summary>
        /// Unique identifier of the licensed application.
        /// </summary>
        /// <example>7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c</example>
        [JsonProperty(Order = 1)]
        [Display(ResourceType = typeof(UsageOverviewPropertyDisplayNames), Name = nameof(ApplicationId))]
        public Guid ApplicationId { get; set; }

        /// <summary>
        /// Full product name of the licensed application.
        /// </summary>
        /// <example>ProductionManager Professional</example>
        [JsonProperty(Order = 2)]
        [Display(ResourceType = typeof(UsageOverviewPropertyDisplayNames), Name = nameof(ApplicationLicenseFullName))]
        public string ApplicationLicenseFullName { get; set; } = string.Empty;

        /// <summary>
        /// Number of licenses available for the application. Defaults to 1.
        /// </summary>
        /// <example>3</example>
        [JsonProperty(Order = 3)]
        [Display(ResourceType = typeof(UsageOverviewPropertyDisplayNames), Name = nameof(LicenseCount))]
        public int LicenseCount { get; set; } = 1;
    }
}
