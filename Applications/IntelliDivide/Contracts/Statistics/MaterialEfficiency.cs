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
    /// Provides the material efficiency data for a material within an optimization.
    /// </summary>
    [LocalizationResource(typeof(StatisticsDisplayNames))]
    public class MaterialEfficiency : IExtensibleDataObject, IContainsUnitSystemDependentProperties, ISupportsLocalizedSerialization
    {
        /// <summary>
        /// Gets or sets the name of the machine.
        /// </summary>
        [JsonProperty(Order = 13)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.MachineName))]
        public string MachineName { get; set; }

        /// <summary>
        /// Gets or sets the material code.
        /// </summary>
        [JsonProperty(Order = 14)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.MaterialCode))]
        public string MaterialCode { get; set; }

        /// <summary>
        /// Gets or sets the id of the optimization.
        /// </summary>
        [JsonProperty(Order = 11)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.OptimizationId))]
        public string OptimizationId { get; set; }

        /// <summary>
        /// Gets or sets the name of the optimization.
        /// </summary>
        [JsonProperty(Order = 12)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.OptimizationName))]
        public string OptimizationName { get; set; }

        /// <summary>
        /// Gets or sets the datetime when the optimization was transferred.
        /// </summary>
        [JsonProperty(Order = 10)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.TransferredAt))]
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
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.BoardsUsedArea))]
        public double BoardsUsedArea { get; set; }

        /// <summary>
        /// Gets or sets the quantity of boards used.
        /// </summary>
        [JsonProperty(Order = 21)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.BoardsUsedQuantity))]
        public int BoardsUsedQuantity { get; set; }

        /// <summary>
        /// Gets or sets the area of offcuts used in m² (or ft² in subscriptions using the imperial unit system).
        /// </summary>
        [JsonProperty(Order = 22)]
        [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.OffcutsUsedArea))]
        public double OffcutsUsedArea { get; set; }

        /// <summary>
        /// Gets or sets the quantity of offcuts used.
        /// </summary>
        [JsonProperty(Order = 23)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.OffcutsUsedQuantity))]
        public int OffcutsUsedQuantity { get; set; }

        #endregion

        #region Output

        /// <summary>
        /// Gets or sets the area of parts produced in m² (or ft² in subscriptions using the imperial unit system).
        /// </summary>
        [JsonProperty(Order = 30)]
        [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.PartsArea))]
        public double PartsArea { get; set; }

        /// <summary>
        /// Gets or sets the quantity of parts produced.
        /// </summary>
        [JsonProperty(Order = 31)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.PartsQuantity))]
        public int PartsQuantity { get; set; }

        /// <summary>
        /// Gets or sets the area of offcuts produced in m² (or ft² in subscriptions using the imperial unit system).
        /// </summary>
        [JsonProperty(Order = 32)]
        [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.OffcutsProducedArea))]
        public double OffcutsProducedArea { get; set; }

        /// <summary>
        /// Gets or sets the quantity of offcuts produced.
        /// </summary>
        [JsonProperty(Order = 33)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.OffcutsProducedQuantity))]
        public int OffcutsProducedQuantity { get; set; }

        /// <summary>
        /// Gets or sets the area of offcuts produced - offcuts used in m² (or ft² in subscriptions using the imperial unit
        /// system).
        /// </summary>
        [JsonProperty(Order = 34)]
        [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.OffcutsGrowthArea))]
        public double OffcutsGrowthArea { get; set; }

        /// <summary>
        /// Gets or sets the quantity of offcuts produced - offcuts used.
        /// system).
        /// </summary>
        [JsonProperty(Order = 35)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.OffcutsGrowthQuantity))]
        public double OffcutsGrowthQuantity { get; set; }

        /// <summary>
        /// Gets or sets the area of waste produced in m² (or ft² in subscriptions using the imperial unit system).
        /// </summary>
        [JsonProperty(Order = 36)]
        [ValueDependsOnUnitSystem(BaseUnit.SquareMeter)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.WasteArea))]
        public double WasteArea { get; set; }

        /// <summary>
        /// Cost of material per unit area.
        /// </summary>
        [JsonProperty(Order = 37)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.Costs))]
        public double? Costs { get; set; }

        /// <summary>
        /// Gets the total material costs.
        /// </summary>
        [JsonProperty(Order = 38)]
        [Display(ResourceType = typeof(StatisticsDisplayNames), Name = nameof(StatisticsDisplayNames.TotalCosts))]
        public double? TotalCosts
        {
            get
            {
                if (Costs.HasValue)
                {
                    return Costs.Value * BoardsUsedArea + Costs.Value * OffcutsUsedArea;
                }

                return null;
            }
        }

        #endregion
    }
}