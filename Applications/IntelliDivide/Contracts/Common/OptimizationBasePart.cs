#nullable enable

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Common
{
    /// <summary>
    /// Describes a part in context of an intelliDivide optimization.
    /// </summary>
    [DebuggerDisplay("{Description}, {MaterialCode}, {Length} x {Width}")]
    public class OptimizationBasePart : IEdgebandingProperties, ILaminatingProperties, IDimensionProperties, ICncProgramProperties
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public OptimizationBasePart() { }

        /// <summary>
        /// Creates a new instance using the given <paramref name="unitSystem" />.
        /// </summary>
        public OptimizationBasePart(UnitSystem unitSystem) : this()
        {
            UnitSystem = unitSystem;
        }

        #endregion

        #region (1) Required properties

        /// <summary>
        /// Gets or sets a description for the part.
        /// </summary>
        [Required]
        [JsonProperty(Order = 10)]
        [StringLength(100, MinimumLength = 1)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the material code.
        /// </summary>
        [Required]
        [JsonProperty(Order = 11)]
        [StringLength(50, MinimumLength = 1)]
        public string? MaterialCode { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Grain" /> of the part.
        /// </summary>
        [Required]
        [JsonProperty(Order = 12)]
        public Grain Grain { get; set; } = Grain.None;

        #endregion

        #region (2) Cutting / Nesting

        /// <inheritdoc />
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? Thickness { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 20)]
        [Range(0.1, 9999.9)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? Length { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 21)]
        [Range(0.1, 9999.9)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? Width { get; set; }

        #endregion

        #region (3) Edgebanding

        /// <summary>
        /// Gets or sets the edgeband code of the edgeband type which should get applied on the front.
        /// </summary>
        [JsonProperty(Order = 31)]
        [StringLength(50, MinimumLength = 1)]
        public string? EdgeFront { get; set; }

        /// <summary>
        /// Gets or sets the edgeband code of the edgeband type which should get applied on the back.
        /// </summary>
        [JsonProperty(Order = 32)]
        [StringLength(50, MinimumLength = 1)]
        public string? EdgeBack { get; set; }

        /// <summary>
        /// Gets or sets the edgeband code of the edgeband type which should get applied on the left.
        /// </summary>
        [JsonProperty(Order = 33)]
        [StringLength(50, MinimumLength = 1)]
        public string? EdgeLeft { get; set; }

        /// <summary>
        /// Gets or sets the edgeband code of the edgeband type which should get applied on the right.
        /// </summary>
        [JsonProperty(Order = 34)]
        [StringLength(50, MinimumLength = 1)]
        public string? EdgeRight { get; set; }

        /// <summary>
        /// Gets or sets how the edgebands should get applied.
        /// </summary>
        [JsonProperty(Order = 35)]
        public string? EdgeDiagram { get; set; }

        #endregion

        #region (4) CNC processing

        /// <inheritdoc />
        [JsonProperty(Order = 41)]
        [StringLength(100, MinimumLength = 1)]
        public string? CncProgramName1 { get; set; }

        /// <summary>
        /// Gets or sets the program name of the CNC program to execute.
        /// </summary>
        [JsonProperty(Order = 42)]
        [StringLength(100, MinimumLength = 1)]
        public string? CncProgramName2 { get; set; }

        /// <summary>
        /// Gets or sets the program name of the CNC program to execute.
        /// </summary>
        [JsonProperty(Order = 43)]
        [StringLength(100, MinimumLength = 1)]
        public string? CncProgramName3 { get; set; }

        #endregion

        #region (5) Laminating

        /// <summary>
        /// Gets or sets the material code of the laminate type which should get applied on the top.
        /// </summary>
        [JsonProperty(Order = 51)]
        [StringLength(50, MinimumLength = 1)]
        public string? LaminateTop { get; set; }

        /// <summary>
        /// Gets or sets the material code of the laminate type which should get applied on the bottom.
        /// </summary>
        [JsonProperty(Order = 52)]
        [StringLength(50, MinimumLength = 1)]
        public string? LaminateBottom { get; set; }

        #endregion

        #region (6) Order

        /// <summary>
        /// Gets or sets the id of the part.
        /// </summary>
        [JsonProperty(Order = 1)]
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer who has ordered the part.
        /// </summary>
        [JsonProperty(Order = 60)]
        public string? CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the order.
        /// </summary>
        [JsonProperty(Order = 61)]
        public string? OrderName { get; set; }

        /// <summary>
        /// Gets or sets the item of the order.
        /// </summary>
        [JsonProperty(Order = 62)]
        public string? OrderItem { get; set; }

        /// <summary>
        /// Gets or sets the order date.
        /// </summary>
        [JsonProperty(Order = 63)]
        public DateTime? OrderDate { get; set; }

        /// <summary>
        /// Gets or sets the finish length.
        /// </summary>
        [JsonProperty(Order = 64)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? FinishLength { get; set; }

        /// <summary>
        /// Gets or sets the finish length.
        /// </summary>
        [JsonProperty(Order = 65)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? FinishWidth { get; set; }

        /// <summary>
        /// Gets or sets the second cut length.
        /// </summary>
        [JsonProperty(Order = 66)]
        [Range(0.1, 9999.9)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? SecondCutLength { get; set; }

        /// <summary>
        /// Gets or sets the second cut width.
        /// </summary>
        [JsonProperty(Order = 67)]
        [Range(0.1, 9999.9)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? SecondCutWidth { get; set; }

        /// <summary>
        /// Gets or sets the production route.
        /// </summary>
        [JsonProperty(Order = 68)]
        public string? ProductionRoute { get; set; }

        #endregion

        #region (7) Label

        /// <summary>
        /// Gets or sets the label layout.
        /// </summary>
        [JsonProperty(Order = 70)]
        public string? LabelLayout { get; set; }

        #endregion

        #region (8) Additional properties

        /// <summary>
        /// Gets or sets the notes of the production entity.
        /// </summary>
        [JsonProperty(Order = 80)]
        public string? Notes { get; set; }

        /// <summary>
        /// Gets or sets the lot in wich the part is contained.
        /// </summary>
        [JsonProperty(Order = 81)]
        public string? Lot { get; set; }

        /// <summary>
        /// Gets or sets the additional properties configured in the application.
        /// </summary>
        [JsonProperty(Order = 80)]
        [JsonExtensionData]
        public IDictionary<string, object>? AdditionalProperties { get; set; } = new Dictionary<string, object>();

        #endregion

        #region IContainsUnitSystemDependentProperties Members

        /// <inheritdoc />
        [DefaultValue(UnitSystem.Metric)]
        public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;

        #endregion
    }
}