using System.ComponentModel.DataAnnotations;

namespace HomagConnect.Base.Contracts.Interfaces;

public interface IHasMaterialCode
{
    /// <summary>       
    /// Gets or sets the material.
    /// </summary>
    [StringLength(50, MinimumLength = 1)]
    public string MaterialCode { get; set; }
}