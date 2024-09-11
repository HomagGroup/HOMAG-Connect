using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace HomagConnect.IntelliDivide.Contracts.Statistics
{
    public class EdebandUsage : IExtensibleDataObject, IContainsUnitSystemDependentProperties
    {
        /// <summary>
        /// The time when the optimization job was downloaded or transferred to productionAssist
        /// </summary>
        [JsonProperty(Order = 1)]
        public DateTimeOffset TransferredAt { get; set; }


        /// <summary>
        /// The unique GUID of the optimization job id
        /// </summary>
        [JsonProperty(Order = 2)]
        public Guid OptimizationId { get; set; }

        /// <summary>
        /// The optimization job name from which we computed the edgebands
        /// </summary>
        [JsonProperty(Order = 3)]
        public string OptimizationName { get; set; }


        /// <summary>
        /// the edgeband code assigned to this edgeband
        /// </summary>
        [JsonProperty(Order = 4)]
        public string EdgebandCode { get; set; }

        /// <summary>
        /// The unit system measure, default is metric
        /// </summary>
        [JsonProperty(Order = 5)]
        public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;

        /// <summary>
        /// Total length used for edgebands of this type
        /// </summary>
        [JsonProperty(Order = 6)]
        public double? LengthUsed { get; set; }

        /// <summary>
        /// Cost of edgebands per unit (meters or feet)
        /// </summary>
        [JsonProperty(Order = 7)]
        public double? Costs { get; set; }


        #region Calculated Value

        /// <summary>
        /// Gets the total value of edgebands of this type 
        /// </summary>
        [JsonProperty(Order = 8)]
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

        #endregion

        #region IExtensibleDataObject Members

        /// <inheritdoc />
        public ExtensionDataObject? ExtensionData { get; set; }

        #endregion
    }
}
