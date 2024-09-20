using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    /// <summary>
    /// Represents a board to use in an optimization.
    /// </summary>
    public class OptimizationRequestBoard : IExtensibleDataObject, IContainsUnitSystemDependentProperties
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
        /// Gets or sets the board code.
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string BoardCode { get; set; }

        /// <summary>
        /// Gets or sets the costs of the board per m² or ft².
        /// </summary>
        [Range(0, double.PositiveInfinity)]
        public double Costs { get; set; }

        /// <summary>
        /// Gets or sets the grain of the board.
        /// </summary>
        public Grain Grain { get; set; }

        /// <summary>
        /// Gets or sets the length of the board.
        /// </summary>
        [Required]
        [Range(0.1, 9999.9)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter, PropertyDecimals.MillimeterDecimals, PropertyDecimals.InchDecimals)]
        public double Length { get; set; }

        /// <summary>
        /// Gets or sets the material code.
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string MaterialCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the board.
        /// </summary>
        [Range(0, 99999)]
        public int Quantity { get; set; } = 9999;

        /// <summary>
        /// Gets or sets the thickness of the board.
        /// </summary>
        [Required]
        [Range(0.01, double.PositiveInfinity)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter, PropertyDecimals.MillimeterDecimals, PropertyDecimals.InchDecimals)]
        public double Thickness { get; set; }

        /// <summary>
        /// Gets or sets the type of the board.
        /// </summary>
        public BoardTypeType Type { get; set; } = BoardTypeType.Board;

        /// <summary>
        /// Gets or sets the length of the board.
        /// </summary>
        [Required]
        [Range(0.1, 9999.9)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter, PropertyDecimals.MillimeterDecimals, PropertyDecimals.InchDecimals)]
        public double Width { get; set; }

        #region IExtensibleDataObject Members

        /// <inheritdoc />
        public ExtensionDataObject ExtensionData { get; set; }

        #endregion

        #region IContainsUnitSystemDependentProperties

        /// <inheritdoc />
        public UnitSystem UnitSystem { get; set; }

        #endregion
    }
}