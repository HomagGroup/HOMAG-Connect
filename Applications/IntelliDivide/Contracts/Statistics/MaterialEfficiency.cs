using System;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Statistics
{
    /// <summary>
    /// Provides the material efficiency data for a material within an optimization.
    /// </summary>
    public class MaterialEfficiency
    {
        /// <summary>
        /// Gets or sets the name of the machine.
        /// </summary>
        [JsonProperty(Order = 13)]
        public string MachineName { get; set; }

        /// <summary>
        /// Gets or sets the material code.
        /// </summary>
        [JsonProperty(Order = 14)]
        public string MaterialCode { get; set; }

        /// <summary>
        /// Gets or sets the id of the optimization.
        /// </summary>
        [JsonProperty(Order = 11)]
        public string OptimizationId { get; set; }

        /// <summary>
        /// Gets or sets the name of the optimization.
        /// </summary>
        [JsonProperty(Order = 12)]
        public string OptimizationName { get; set; }

        /// <summary>
        /// Gets or sets the datetime when the optimization was transferred.
        /// </summary>
        [JsonProperty(Order = 10)]
        public DateTimeOffset TransferredAt { get; set; }

        #region Input

        /// <summary>
        /// Gets or sets the area of boards used in m² (or ft² in subscriptions using the imperial unit system).
        /// </summary>
        [JsonProperty(Order = 20)]
        public double BoardsUsed { get; set; }

        /// <summary>
        /// Gets or sets the area of boards used in %.
        /// </summary>
        [JsonProperty(Order = 21)]
        public double BoardsUsedPercentage { get; set; }

        /// <summary>
        /// Gets or sets the area of offcuts used in m² (or ft² in subscriptions using the imperial unit system).
        /// </summary>
        [JsonProperty(Order = 22)]
        public double OffcutsUsed { get; set; }

        /// <summary>
        /// Gets or sets the area of offcuts used in %.
        /// </summary>
        [JsonProperty(Order = 23)]
        public double OffcutsUsedPercentage { get; set; }

        #endregion

        #region Output

        /// <summary>
        /// Gets or sets the area of parts produced in m² (or ft² in subscriptions using the imperial unit system).
        /// </summary>
        [JsonProperty(Order = 30)]
        public double Parts { get; set; }

        /// <summary>
        /// Gets or sets the area of parts produced in %.
        /// </summary>
        [JsonProperty(Order = 31)]
        public double PartsPercentage { get; set; }

        /// <summary>
        /// Gets or sets the area of offcuts produced in m² (or ft² in subscriptions using the imperial unit system).
        /// </summary>
        [JsonProperty(Order = 32)]
        public double OffcutsProduced { get; set; }

        /// <summary>
        /// Gets or sets the area of offcuts produced in %.
        /// </summary>
        [JsonProperty(Order = 33)]
        public double OffcutsProducedPercentage { get; set; }

        /// <summary>
        /// Gets or sets the area of offcuts produced - offcuts used used in m² (or ft² in subscriptions using the imperial unit
        /// system).
        /// </summary>
        [JsonProperty(Order = 34)]
        public double OffcutsGrowth { get; set; }

        /// <summary>
        /// Gets or sets the area of offcuts produced - offcuts used used in %.
        /// </summary>
        [JsonProperty(Order = 35)]
        public double OffcutsGrowthPercentage { get; set; }

        /// <summary>
        /// Gets or sets the area of waste produced in m² (or ft² in subscriptions using the imperial unit system).
        /// </summary>
        [JsonProperty(Order = 36)]
        public double Waste { get; set; }

        /// <summary>
        /// Gets or sets the area of waste produced in %.
        /// </summary>
        [JsonProperty(Order = 37)]
        public double WastePercentage { get; set; }

        #endregion
    }
}