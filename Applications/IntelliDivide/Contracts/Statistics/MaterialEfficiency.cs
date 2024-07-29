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
    }
}