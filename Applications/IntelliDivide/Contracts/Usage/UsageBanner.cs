using System.Collections.Generic;
using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Usage
{
    /// <summary>
    /// Shows the number of transferred parts and the licenses owned in the current period
    /// </summary>
    public class UsageBanner
    {
        /// <summary>
        /// The number of parts transferred in the current period
        /// </summary>
	[JsonProperty(Order = 3)]
        public int? NumberOfPartsTransferred { get; set; }

        /// <summary>
        /// The licenses owned in the current period
        /// </summary>
	[JsonProperty(Order = 4)]
        public IEnumerable<LicenseDetails> Licenses { get; set; }
    }
}
