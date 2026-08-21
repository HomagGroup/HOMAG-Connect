using System;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.IntelliDivide.Contracts.Statistics;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Usage
{
    /// <summary>
    /// Describes a transferred optimization job and related usage statistics.
    /// </summary>
    /// <example>
    /// {
    ///   "partsTransferredQuantity": 24,
    ///   "transferredAt": "2025-03-10T14:30:00+00:00",
    ///   "optimizationName": "Kitchen_Order_4711",
    ///   "transferredBy": "max.mustermann@example.com",
    ///   "machineName": "SAWTEQ-B300",
    ///   "wastePercentage": 12.5,
    ///   "productionTime": "01:35:00"
    /// }
    /// </example>
    [LocalizationResource(typeof(StatisticsDisplayNames))]
    public class UsageDetails : ISupportsLocalizedSerialization
    {
        /// <summary>
        /// Gets or sets the number of parts transferred in the job.
        /// </summary>
        /// <example>24</example>
        [JsonProperty(Order = 1)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.PartsTransferredQuantity))]
        public int PartsTransferredQuantity { get; set; }

        /// <summary>
        /// Gets or sets the time when the job was transferred.
        /// </summary>
        /// <example>2025-03-10T14:30:00+00:00</example>
        [JsonProperty(Order = 2)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.TransferredAt))]
        public DateTimeOffset TransferredAt { get; set; }

        /// <summary>
        /// Gets or sets the name of the optimization job.
        /// </summary>
        /// <example>Kitchen_Order_4711</example>
        [JsonProperty(Order = 3)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.OptimizationName))]
        public string OptimizationName { get; set; }

        /// <summary>
        /// Gets or sets the user who transferred the job.
        /// </summary>
        /// <example>max.mustermann@example.com</example>
        [JsonProperty(Order = 4)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.TransferredBy))]
        public string TransferredBy { get; set; }

        /// <summary>
        /// Gets or sets the name of the machine that will process the job.
        /// </summary>
        /// <example>SAWTEQ-B300</example>
        [JsonProperty(Order = 5)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.MachineName))]
        public string MachineName { get; set; }

        /// <summary>
        /// Gets or sets the board waste percentage.
        /// </summary>
        /// <example>12.5</example>
        [JsonProperty(Order = 6)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.WastePercentage))]
        public double? WastePercentage { get; set; }

        /// <summary>
        /// Gets or sets the estimated production time for the job.
        /// </summary>
        /// <example>01:35:00</example>
        [JsonProperty(Order = 7)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.ProductionTime))]
        public TimeSpan ProductionTime { get; set; }
    }
}
