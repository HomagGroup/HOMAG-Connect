using System;
using System.Collections.Generic;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands;

/// <summary>
/// Extends an edgeband type with related additional data and individual inventory records.
/// <see href="https://docs.homag.cloud/docs/materialmanager-materialtypen-kantenbaender">Learn more about edgeband types</see>.
/// </summary>
/// <example>
/// {
///   "unitSystem": "Metric",
///   "edgebandCode": "ABS_Hunton_Oak_dark_2.00_23.0_HM",
///   "height": 23.0,
///   "thickness": 2.0,
///   "additionalData": [],
///   "inventory": [
///     { "code": "edge-001", "location": "LOC-01-02-03", "length": 50.0, "quantity": 1 }
///   ]
/// }
/// </example>
public class EdgebandTypeDetails : EdgebandType
{
    /// <summary>
    /// Additional structured information associated with this edgeband type, when included.
    /// </summary>
    public ICollection<AdditionalDataEntity>? AdditionalData { get; set; }

    /// <summary>
    /// Legacy image information. Use <see cref="AdditionalData" /> for additional image data.
    /// </summary>
    [Obsolete("This parameter is obsolete. Use AdditionalData instead.", true)]
    public ICollection<ImageInformation> Images { get; set; } = new List<ImageInformation>();

    /// <summary>
    /// Inventory records associated with this edgeband type.
    /// </summary>
    public ICollection<EdgebandTypeInventory> Inventory { get; set; } = new List<EdgebandTypeInventory>();
}