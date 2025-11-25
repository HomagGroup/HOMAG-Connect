using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.Base.Contracts.Interfaces;

/// <summary>
/// Laminating properties
/// </summary>
public interface ILaminatingProperties
{
    /// <summary>
    /// Gets or sets the material code of the laminate type which should get applied on the bottom.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.ILaminatingProperties_LaminateBottom))]
    public string? LaminateBottom { get; set; }

    /// <summary>
    /// Gets or sets the grain of the laminate which was applied on the bottom.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.ILaminatingProperties_LaminateBottomGrain))]
    public Grain? LaminateBottomGrain { get; set; }

    /// <summary>
    /// Gets or sets the material code of the laminate type which should get applied on the top.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.ILaminatingProperties_LaminateTop))]
    public string? LaminateTop { get; set; }

    /// <summary>
    /// Gets or sets the grain of the laminate which was applied on the top.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.ILaminatingProperties_LaminateTopGrain))]
    public Grain? LaminateTopGrain { get; set; }
}