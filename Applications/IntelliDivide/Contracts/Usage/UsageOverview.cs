using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HomagConnect.IntelliDivide.Contracts.Usage
{
    /// <summary>
    /// Describes the overview per period of the usage of the owned licenses and packages
    /// </summary>
    public class UsageOverview
    {
	/// <summary>
	/// The month of the usage overview
	/// </summary>
	[JsonProperty(Order = 1)]
	public DateTime Period { get; set; }

 	/// <summary>
        /// The number of parts transferred in the current period
        /// </summary>
	[JsonProperty(Order = 2)]
        public int? NumberOfPartsTransferred { get; set; }

        /// <summary>
        /// The licenses owned in the current period
        /// </summary>
	[JsonProperty(Order = 3)]
        public IEnumerable<LicenseDetails> Licenses { get; set; }
    }
}
