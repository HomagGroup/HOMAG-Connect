using System;
using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Usage
{
	/// <summary>
	/// Describes the overview per period of the usage of the owned licenses and packages
	/// </summary>
    public class UsageOverview : UsageBanner
    {
        /// <summary>
		/// The id of the subscription
		/// </summary>
		[JsonProperty(Order = 1)]
		public Guid SubscriptionId { get; set; }

		/// <summary>
		/// The month of the usage overview
		/// </summary>
		[JsonProperty(Order = 2)]
		public DateTime Period { get; set; }
    }
}
