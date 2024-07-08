using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;

namespace HomagConnect.MaterialManager.Contracts.Request;

/// <summary>
/// Request object to create a board in materialManager.
/// </summary>
public class MaterialManagerRequestBoardType : MaterialManagerRequestMaterialType
{
        /// <summary>
        /// Gets or sets the material code.
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string MaterialCode { get; set; }
    
        /// <summary>
        /// Gets or sets the board code.
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string BoardCode { get; set; }

        /// <summary>
        /// Gets or sets the length of the board.
        /// </summary>
        [Required]
        [Range(0.1, 9999.9)]
        public double Length { get; set; }

        /// <summary>
        /// Gets or sets the length of the board.
        /// </summary>
        [Required]
        [Range(0.1, 9999.9)]
        public double Width { get; set; }

        /// <summary>
        /// Gets or sets the thickness of the board.
        /// </summary>
        [Required]
        [Range(0.01, double.PositiveInfinity)]
        public double Thickness { get; set; }

        /// <summary>
        /// Gets or sets the type of the board.
        /// </summary>
        [Required]
        public BoardTypeType Type { get; set; } = BoardTypeType.Board;

        /// <summary>
        /// Gets or sets the material category of the board.
        /// </summary>
        [Required]
        public BoardMaterialCategory MaterialCategory { get; set; } = BoardMaterialCategory.Undefined;

        /// <summary>
        /// Gets or sets the coating category of the board.
        /// </summary>
        [Required]
        public CoatingCategory CoatingCategory { get; set; } = CoatingCategory.Undefined;

        /// <summary>
        /// Gets or sets the grain of the board.
        /// </summary>
        [Required]
        public Grain Grain { get; set; }

        /// <summary>
        /// Gets or set the standard quality.
        /// </summary>
        public StandardQuality StandardQuality { get; set; } = StandardQuality.FinishCut;
        
        /// <summary>
        /// Gets or sets the total quantity available warning limit.
        /// </summary>
        public int? TotalQuantityAvailableWarningLimit { get; set; }

        /// <summary>
        /// Gets or sets whether the board type should be optimized against infinite.
        /// </summary>
        public bool OptimizeAgainstInfinite { get; set; } = true;

        /// <summary>
        /// Gets or sets whether the board type is locked for optimization.
        /// </summary>
        public bool LockedForOptimization { get; set; }
}