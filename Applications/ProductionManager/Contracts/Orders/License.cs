using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Orders
{
    /// <summary>
    /// Contains information about the license
    /// </summary>
    public class License
    {
        /// <summary>
        /// The id of the application
        /// </summary>
        [JsonProperty(Order = 1)]
        public Guid ApplicationId { get; set; }

        /// <summary>
        /// The full name of the application
        /// </summary>
        [JsonProperty(Order = 2)]
        public string ApplicationLicenseFullName { get; set; }

        /// <summary>
        /// The number of licenses
        /// </summary>
        [JsonProperty(Order = 3)]
        public int LicenseCount { get; set; }
    }
}
