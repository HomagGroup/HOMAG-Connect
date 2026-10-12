using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Statistics
{
    /// <summary>
    /// Provides the edgeband usage data for an optimization.
    /// </summary>
    [LocalizationResource(typeof(StatisticsDisplayNames))]
    public class EdgebandUsage : IExtensibleDataObject, IContainsUnitSystemDependentProperties, ISupportsLocalizedSerialization
    {
        /// <summary>
        /// The time when the optimization job was downloaded or transferred
        /// </summary>
        [JsonProperty(Order = 1)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(TransferredAt))]
        public DateTimeOffset TransferredAt { get; set; }

        /// <summary>
        /// The unique GUID of the optimization job id
        /// </summary>
        [JsonProperty(Order = 2)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(OptimizationId))]
        public Guid OptimizationId { get; set; }

        /// <summary>
        /// The optimization name from which we computed the edgebands
        /// </summary>
        [JsonProperty(Order = 3)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(OptimizationName))]
        public string OptimizationName { get; set; }

        /// <summary>
        /// the edgeband code assigned to this edgeband
        /// </summary>
        [JsonProperty(Order = 4)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(EdgebandCode))]
        public string EdgebandCode { get; set; }

        /// <summary>
        /// Total length used for edgebands of this type (meters or feet)
        /// </summary>
        [JsonProperty(Order = 6)]
        [ValueDependsOnUnitSystem(BaseUnit.Meter)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(LengthUsed))]
        public double? LengthUsed { get; set; }

        /// <summary>
        /// Cost of edgebands per unit (meters or feet)
        /// </summary>
        [JsonProperty(Order = 7)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(Costs))]
        public double? Costs { get; set; }

        /// <summary>
        /// Gets the total value of edgebands of this type
        /// </summary>
        [JsonProperty(Order = 8)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(TotalCosts))]
        public double? TotalCosts
        {
            get
            {
                if (LengthUsed.HasValue && Costs.HasValue)
                {
                    return Costs.Value * LengthUsed.Value;
                }

                return null;
            }
        }

        #region IContainsUnitSystemDependentProperties Members

        /// <inheritdoc />
        [JsonProperty(Order = 99)]
        public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;

        #endregion

        #region IExtensibleDataObject Members

        /// <inheritdoc />
        [JsonProperty(Order = 99)]
        public ExtensionDataObject? ExtensionData { get; set; }

        #endregion
    }
}