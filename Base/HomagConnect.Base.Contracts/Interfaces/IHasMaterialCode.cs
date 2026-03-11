using System.ComponentModel.DataAnnotations;

namespace HomagConnect.Base.Contracts.Interfaces;

/// <summary>
/// Defines a material code property for contracts that reference a material definition.
/// </summary>
public interface IHasMaterialCode
{
    /// <summary>       
    /// Gets or sets the material code.
    /// Maximum length is 50 characters.
    /// </summary>
    /// <example>P2_White_19.0</example>
    [StringLength(50, MinimumLength = 1)]
    public string MaterialCode { get; set; }
}