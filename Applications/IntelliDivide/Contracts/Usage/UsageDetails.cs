using System;
using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Usage
{
    /// <summary>
    /// Shows the transferred job and a few statistics about it
    /// </summary>
    public class UsageDetails
    {
        /// <summary>
        /// The number of parts transferred in the job
        /// </summary>
	[JsonProperty(Order = 1)]
        public int PartsTransferredQuantity { get; set; }

        /// <summary>
        /// The time when the job was transferred
        /// </summary>
	[JsonProperty(Order = 2)]
        public DateTimeOffset TransferredAt { get; set; }

        /// <summary>
        /// The name of the optimization job
        /// </summary>
	[JsonProperty(Order = 3)]
        public string OptimizationName { get; set; }

        /// <summary>
        /// The user who transferred the job
        /// </summary>
	[JsonProperty(Order = 4)]
        public string TransferredBy { get; set; }

        /// <summary>
        /// The name of the machine that will process the job
        /// </summary>
	[JsonProperty(Order = 5)]
        public string MachineName { get; set; }

        /// <summary>
        /// The board waste percentage
        /// </summary>
	[JsonProperty(Order = 6)]
        public double? WastePercentage { get; set; }

        /// <summary>
        /// The time estimation for the job to be processed
        /// </summary>
	[JsonProperty(Order = 7)]
        public TimeSpan ProductionTime { get; set; }
    }
}
