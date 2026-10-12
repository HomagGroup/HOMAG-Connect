using System;
using System.Collections.Generic;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands;

/// <summary>
/// Edgeband type details bring together a material's stock and supporting information for managing it in materialManager.
/// <see href="https://docs.homag.cloud/docs/materialmanager-materialtypen-kantenbaender">Learn more about edgeband types</see>.
/// </summary>
public class EdgebandTypeDetails : EdgebandType
{
    /// <summary>
    /// Gets or sets the additional data.
    /// </summary>
    public ICollection<AdditionalDataEntity>? AdditionalData { get; set; }

    /// <summary>
    /// Gets or sets the list of additional images.
    /// </summary>
    [Obsolete("This parameter is obsolete. Use AdditionalData instead.", true)]
    public ICollection<ImageInformation> Images { get; set; } = new List<ImageInformation>();

    /// <summary>
    /// Gets or sets the board type inventory.
    /// </summary>
    public ICollection<EdgebandTypeInventory> Inventory { get; set; } = new List<EdgebandTypeInventory>();
}