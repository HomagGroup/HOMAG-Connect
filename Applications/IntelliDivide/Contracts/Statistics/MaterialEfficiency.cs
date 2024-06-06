using System;
using System.Runtime.Serialization;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Statistics
{
    /// <summary>
    /// Provides the material efficiency data for a material within an optimization.
    /// </summary>
    public class MaterialEfficiency : IExtensibleDataObject, IContainsUnitSystemDependentProperties
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

        #region IContainsUnitSystemDependentProperties Members

        /// <inheritdoc />
        public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;

        #endregion

        #region IExtensibleDataObject Members

        /// <inheritdoc />
        public ExtensionDataObject ExtensionData { get; set; }

        #endregion

        #region Input

        /// <summary>
        /// Gets or sets the area of boards used in m² (or ft² in subscriptions using the imperial unit system).
        /// </summary>
        [JsonProperty(Order = 20)]
        [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
        public double BoardsUsedArea { get; set; }

        /// <summary>
        /// Gets or sets the quantity of boards used.
        /// </summary>
        [JsonProperty(Order = 21)]
        public int BoardsUsedQuantity { get; set; }

        /// <summary>
        /// Gets or sets the area of offcuts used in m² (or ft² in subscriptions using the imperial unit system).
        /// </summary>
        [JsonProperty(Order = 22)]
        [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
        public double OffcutsUsedArea { get; set; }

        /// <summary>
        /// Gets or sets the quantity of offcuts used.
        /// </summary>
        [JsonProperty(Order = 23)]
        public int OffcutsUsedQuantity { get; set; }

        #endregion

        #region Output

        /// <summary>
        /// Gets or sets the area of parts produced in m² (or ft² in subscriptions using the imperial unit system).
        /// </summary>
        [JsonProperty(Order = 30)]
        [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
        public double PartsArea { get; set; }

        /// <summary>
        /// Gets or sets the quantity of parts produced.
        /// </summary>
        [JsonProperty(Order = 31)]
        public int PartsQuantity { get; set; }

        /// <summary>
        /// Gets or sets the area of offcuts produced in m² (or ft² in subscriptions using the imperial unit system).
        /// </summary>
        [JsonProperty(Order = 32)]
        [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
        public double OffcutsProducedArea { get; set; }

        /// <summary>
        /// Gets or sets the quantity of offcuts produced.
        /// </summary>
        [JsonProperty(Order = 33)]
        public int OffcutsProducedQuantity { get; set; }

        /// <summary>
        /// Gets or sets the area of offcuts produced - offcuts used in m² (or ft² in subscriptions using the imperial unit
        /// system).
        /// </summary>
        [JsonProperty(Order = 34)]
        [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
        public double OffcutsGrowthArea { get; set; }

        /// <summary>
        /// Gets or sets the quantity of offcuts produced - offcuts used.
        /// system).
        /// </summary>
        [JsonProperty(Order = 35)]
        public double OffcutsGrowthQuantity { get; set; }

        /// <summary>
        /// Gets or sets the area of waste produced in m² (or ft² in subscriptions using the imperial unit system).
        /// </summary>
        [JsonProperty(Order = 36)]
        [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
        public double WasteArea { get; set; }

        #endregion

        #region Obsolete Properties

        /// <summary>
        /// Gets or sets the area of offcuts produced in %.
        /// </summary>

        [Obsolete("To avoid misinterpretations, calculate this value at the appropriate aggregation level on the client side.")]
        public double OffcutsProducedPercentage { get; set; }

        /// <summary>
        /// Gets or sets the area of parts produced in %.
        /// </summary>

        [Obsolete("To avoid misinterpretations, calculate this value at the appropriate aggregation level on the client side.")]
        public double PartsPercentage { get; set; }

        /// <summary>
        /// Gets or sets the area of waste produced in %.
        /// </summary>

        [Obsolete("To avoid misinterpretations, calculate this value at the appropriate aggregation level on the client side.")]
        public double WastePercentage { get; set; }

        /// <summary>
        /// Gets or sets the area of offcuts produced - offcuts used used in %.
        /// </summary>
        [Obsolete("To avoid misinterpretations, calculate this value at the appropriate aggregation level on the client side.")]
        public double OffcutsGrowthPercentage { get; set; }

        /// <summary>
        /// Gets or sets the area of offcuts used in %.
        /// </summary>
        [Obsolete("To avoid misinterpretations, calculate this value at the appropriate aggregation level on the client side.")]
        public double OffcutsUsedPercentage { get; set; }

        /// <summary>
        /// Gets or sets the area of boards used in %.
        /// </summary>
        [Obsolete("To avoid misinterpretations, calculate this value at the appropriate aggregation level on the client side.")]
        public double BoardsUsedPercentage { get; set; }

        /// <summary>
        /// Gets or sets the area of parts produced in m² (or ft² in subscriptions using the imperial unit system).
        /// </summary>
        [Obsolete("Use PartsArea instead.")]
        public double Parts
        {
            get
            {
                return PartsArea;
            }
            set
            {
                PartsArea = value;
            }
        }

        /// <summary>
        /// Gets or sets the area of offcuts produced in m² (or ft² in subscriptions using the imperial unit system).
        /// </summary>

        [Obsolete("Use OffcutsProducedArea instead.")]
        public double OffcutsProduced
        {
            get
            {
                return OffcutsProducedArea;
            }
            set
            {
                OffcutsProducedArea = value;
            }
        }

        /// <summary>
        /// Gets or sets the area of waste produced in m² (or ft² in subscriptions using the imperial unit system).
        /// </summary>

        [Obsolete("Use WasteArea instead.")]
        public double Waste
        {
            get
            {
                return WasteArea;
            }
            set
            {
                WasteArea = value;
            }
        }

        /// <summary>
        /// Gets or sets the area of offcuts produced - offcuts used used in m² (or ft² in subscriptions using the imperial unit
        /// system).
        /// </summary>

        [Obsolete("Use OffcutsGrowthArea instead.")]
        public double OffcutsGrowth
        {
            get
            {
                return OffcutsGrowthArea;
            }
            set
            {
                OffcutsGrowthArea = value;
            }
        }

        /// <summary>
        /// Gets or sets the area of boards used in m² (or ft² in subscriptions using the imperial unit system).
        /// </summary>

        [Obsolete("Use BoardsUsedArea instead.")]
        public double BoardsUsed
        {
            get
            {
                return BoardsUsedArea;
            }
            set
            {
                BoardsUsedArea = value;
            }
        }

        /// <summary>
        /// Gets or sets the area of offcuts used in m² (or ft² in subscriptions using the imperial unit system).
        /// </summary>

        [Obsolete("Use OffcutsUsedArea instead.")]
        public double OffcutsUsed
        {
            get
            {
                return OffcutsUsedArea;
            }
            set
            {
                OffcutsUsedArea = value;
            }
        }

        #endregion
    }
}