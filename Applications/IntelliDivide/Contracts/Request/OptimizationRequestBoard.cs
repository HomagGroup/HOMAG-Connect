using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    /// <summary>
    /// Represents a board to use in an optimization.
    /// </summary>
    public class OptimizationRequestBoard : IExtensibleDataObject
    {
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
        public double Width { get; set; }

        /// <summary>
        /// <see cref="IExtensibleDataObject" /> members.
        /// </summary>
        public ExtensionDataObject ExtensionData { get; set; }
    }
}