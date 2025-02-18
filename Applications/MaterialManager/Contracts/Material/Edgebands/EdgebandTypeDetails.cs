using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands;

/// <summary>
/// The edgeband type details.
/// </summary>
public class EdgebandTypeDetails : EdgebandType
{
    /// <summary>
    /// Gets or sets the additional data.
    /// </summary>
    public Collection<AdditionalDataTexture>? AdditionalData { get; set; }

    /// <summary>
    /// Gets or sets the list of additional images.
    /// </summary>
    [Obsolete("This parameter is obsolete. Use AdditionalData instead.")]
    public ICollection<ImageInformation> Images { get; set; } = new List<ImageInformation>();

    /// <summary>
    /// Gets or sets the board type inventory.
    /// </summary>
    public ICollection<EdgebandTypeInventory> Inventory { get; set; } = new List<EdgebandTypeInventory>();
}