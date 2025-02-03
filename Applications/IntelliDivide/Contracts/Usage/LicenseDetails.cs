using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Usage
{
    /// <summary>
    /// Contains the name and the number of parts covered by a license
    /// </summary>
    public class LicenseDetails
    {
        /// <summary>
        /// The name of the license
        /// </summary>
	[JsonProperty(Order = 1)]
        public string LicenseName { get; set; }

        /// <summary>
        /// The number of parts covered by the license
        /// </summary>
	[JsonProperty(Order = 2)]
        public int PartsCoveredQuantity { get; set; }
    }
}
