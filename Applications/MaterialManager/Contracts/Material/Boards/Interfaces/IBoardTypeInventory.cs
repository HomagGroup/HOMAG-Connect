using System.Collections.Generic;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards.Interfaces;

public interface IBoardCodeWithInventory
{
    /// <summary>
    /// Gets or sets the board code.
    /// </summary>
    string BoardCode { get; set; }

    /// <summary>
    /// Gets or sets the total quantity of boards of this type in the inventory.
    /// </summary>
    int? TotalQuantityInInventory { get; set; }

    /// <summary>
    /// Gets or sets the total quantity of boards of this type which have been allocated to a production order.
    /// </summary>
    int? TotalQuantityAllocated { get; set; }

    /// <summary>
    /// Gets or sets the total quantity of boards of this type which are available in the inventory.
    /// </summary>
    int? TotalQuantityAvailable { get; }

    /// <summary>
    /// Gets or sets the total area of boards of this type in the inventory. The unit depends on the settings of the
    /// subscription (metric: m², imperial: ft²).
    /// </summary>
    double? TotalAreaInventory { get; }

    /// <summary>
    /// Gets or sets the total area of boards of this type which have been allocated to a production order. The unit depends on
    /// the settings of the subscription (metric: m², imperial: ft²).
    /// </summary>
    double? TotalAreaAllocated { get; }

    /// <summary>
    /// Gets or sets the total area of boards of this type which are available in the inventory. The unit depends on the
    /// settings of the subscription (metric: m², imperial: ft²).
    /// </summary>
    double? TotalAreaAvailable { get; }

    /// <summary>
    /// Gets or sets the board type inventory.
    /// </summary>
    ICollection<BoardTypeInventory> Inventory { get; set; }
}