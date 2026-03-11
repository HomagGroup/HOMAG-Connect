
using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    /// <summary>
    /// Represents a board available for an intelliDivide optimization request. When no boards are specified,
    /// board data is automatically retrieved from materialManager.
    /// </summary>
    /// <example>
    /// {
    ///   "materialCode": "P2_White_19.0",
    ///   "boardCode": "P2_White_19.0_2800_2070",
    ///   "length": 2800,
    ///   "width": 2070,
    ///   "thickness": 19.0,
    ///   "grain": "None",
    ///   "costs": 10,
    ///   "quantity": 70
    /// }
    /// </example>
    public class OptimizationRequestBoard : IContainsUnitSystemDependentProperties
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public OptimizationRequestBoard() { }

        /// <summary>
        /// Creates a new instance using the given <paramref name="unitSystem" />.
        /// </summary>
        public OptimizationRequestBoard(UnitSystem unitSystem)
        {
            UnitSystem = unitSystem;
        }

        #endregion

        /// <summary>
        /// Gets or sets the unique board code that identifies this board type. Maximum length is 50 characters.
        /// </summary>
        /// <example>OAK_19.0_2800_2070</example>
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string BoardCode { get; set; }

        /// <summary>
        /// Gets or sets the cost per area unit of the board.
        /// Unit: per m² when <see cref="UnitSystem" /> is <c>Metric</c>; per ft² when <c>Imperial</c>.
        /// </summary>
        /// <example>10</example>
        [Range(0, double.PositiveInfinity)]
        public double Costs { get; set; }

        /// <summary>
        /// Gets or sets the grain direction of the board. Supported values: <c>None</c>, <c>Lengthwise</c>, <c>Crosswise</c>.
        /// </summary>
        /// <example>None</example>
        public Grain Grain { get; set; }

        /// <summary>
        /// Gets or sets the length of the board.
        /// Unit: mm when <see cref="UnitSystem" /> is <c>Metric</c>; inches when <c>Imperial</c>.
        /// </summary>
        /// <example>2800</example>
        [Required]
        [Range(0.1, 9999.9)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double Length { get; set; }

        /// <summary>
        /// Gets or sets the material code that links this board to a material definition. Maximum length is 50 characters.
        /// </summary>
        /// <example>P2_White_19.0</example>
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string MaterialCode { get; set; }

        /// <summary>
        /// Gets or sets the number of boards available for the optimization. Must be between 0 and 99,999. Defaults to 9,999.
        /// </summary>
        /// <example>70</example>
        [Range(0, 99999)]
        public int Quantity { get; set; } = 9999;

        /// <summary>
        /// Gets or sets the thickness of the board.
        /// Unit: mm when <see cref="UnitSystem" /> is <c>Metric</c>; inches when <c>Imperial</c>.
        /// </summary>
        /// <example>19.0</example>
        [Required]
        [Range(0.01, double.PositiveInfinity)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double Thickness { get; set; }

        /// <summary>
        /// Gets or sets the type of the board. Supported values: <c>Board</c>, <c>Offcut</c>, <c>Template</c>. Defaults to <c>Board</c>.
        /// </summary>
        /// <example>Board</example>
        public BoardTypeType Type { get; set; } = BoardTypeType.Board;

        /// <summary>
        /// Gets or sets the width of the board.
        /// Unit: mm when <see cref="UnitSystem" /> is <c>Metric</c>; inches when <c>Imperial</c>.
        /// </summary>
        /// <example>2070</example>
        [Required]
        [Range(0.1, 9999.9)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double Width { get; set; }

        /// <summary>
        /// Gets or sets an optional comment for the board.
        /// </summary>
        /// <example>Remaining stock from order 4711</example>
        public string? Comment { get; set; }

        /// <summary>
        /// Gets or sets additional custom properties configured in the application. Any JSON properties not mapped
        /// to a typed member are captured here via <c>[JsonExtensionData]</c>.
        /// </summary>
        /// <example>{ "customField1": "value1" }</example>
        [JsonProperty(Order = 80)]
        [JsonExtensionData]
        public IDictionary<string, object>? AdditionalProperties { get; set; }

        #region IContainsUnitSystemDependentProperties

        /// <inheritdoc />
        public UnitSystem UnitSystem { get; set; }

        #endregion
    }
}