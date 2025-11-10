using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;

namespace HomagConnect.MaterialManager.Contracts.Update;

/// <summary>
/// Úpdate object to update a board type in materialManager.
/// </summary>
public class MaterialManagerUpdateBoardType : MaterialManagerUpdateMaterialType
{
    /// <summary>
    /// Gets or sets the board code.
    /// </summary>
    [StringLength(50, MinimumLength = 1)]
    public string? BoardCode { get; set; } = null;

    /// <summary>
    /// Gets or sets the coating category of the board.
    /// </summary>
    public CoatingCategory? CoatingCategory { get; set; } = null;

    /// <summary>
    /// Gets or sets the density of the board.
    /// </summary>
    public double? Density { get; set; }

    /// <summary>
    /// Gets or sets the grain of the board.
    /// </summary>
    public Grain? Grain { get; set; } = null;

    /// <summary>
    /// Gets or sets the length of the board.
    /// </summary>
    [Range(0.1, 19999.9)]
    public double? Length { get; set; } = null;

    /// <summary>
    /// Gets or sets whether the board type is locked for optimization.
    /// </summary>
    public bool? LockedForOptimization { get; set; } = null;

    /// <summary>
    /// Gets or sets the material category of the board.
    /// </summary>
    public BoardMaterialCategory? MaterialCategory { get; set; } = null;

    /// <summary>
    /// Gets or sets the material code.
    /// </summary>
    [StringLength(50, MinimumLength = 1)]
    public string? MaterialCode { get; set; } = null;

    /// <summary>
    /// Gets or sets whether the board type should be optimized against infinite.
    /// </summary>
    public bool? OptimizeAgainstInfinite { get; set; } = null;

    /// <summary>
    /// Gets or set the standard quality.
    /// </summary>
    public StandardQuality? StandardQuality { get; set; } = null;

    /// <summary>
    /// Gets or sets the thickness of the board.
    /// </summary>
    [Range(0.01, double.PositiveInfinity)]
    public double? Thickness { get; set; } = null;

    /// <summary>
    /// Gets or sets the total quantity available warning limit.
    /// </summary>
    public int? TotalQuantityAvailableWarningLimit { get; set; } = null;

    /// <summary>
    /// Gets or sets the type of the board.
    /// </summary>
    public BoardTypeType? Type { get; set; } = null;

    /// <summary>
    /// Gets or sets the length of the board.
    /// </summary>
    [Range(0.1, 19999.9)]
    public double? Width { get; set; } = null;

    /// <summary>
    /// Gets or sets the decor top embossing.
    /// </summary>
    [StringLength(50)]
    public string? EmbossingTop { get; set; } = null;

    /// <summary>
    /// Gets or sets the decor bottom embossing.
    /// </summary>
    [StringLength(50)]
    public string? EmbossingBottom { get; set; } = null;
}