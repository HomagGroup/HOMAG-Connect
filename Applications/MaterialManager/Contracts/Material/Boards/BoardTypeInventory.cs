#nullable enable
using HomagConnect.Base.Contracts.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards;

/// <summary>
/// Represents inventory information for a specific board type.
/// </summary>
/// <example>
/// { "code": "B-1001", "creationDate": "2025-04-01T08:30:00+00:00", "location": "Main Buffer 03", "orderNumber": "4711", "quantity": 12, "workstation": "Saw-01", "additionalCommentsBoards": "Reserved for production" }
/// </example>
public class BoardTypeInventory : ISupportsAdditionalProperties
{
    /// <summary>
    /// Gets or sets the additional comments for the board.
    /// </summary>
    /// <example>Reserved for production</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeInventoryProperties_AdditionalCommentsBoards))]
    public string? AdditionalCommentsBoards { get; set; }

    /// <summary>
    /// Gets or sets the board code.
    /// </summary>
    /// <example>B-1001</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeInventoryProperties_Code))]
    public string? Code { get; set; }

    /// <summary>
    /// Gets or sets the creation date of the instance data.
    /// </summary>
    /// <example>2025-04-01T08:30:00+00:00</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeInventoryProperties_CreationDate))]
    public DateTimeOffset? CreationDate { get; set; }

    /// <summary>
    /// Gets or sets the location.
    /// </summary>
    /// <example>Main Buffer 03</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeInventoryProperties_Location))]
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets the order number.
    /// </summary>
    /// <example>4711</example>
    public string? OrderNumber { get; set; }

    /// <summary>
    /// Gets or sets the quantity.
    /// </summary>
    /// <example>12</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeInventoryProperties_Quantity))]
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the workstation.
    /// </summary>
    /// <example>Saw-01</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeInventoryProperties_Workstation))]
    public string? Workstation { get; set; }
        
    /// <inheritdoc/>
    [JsonExtensionData]
    [JsonProperty(Order = 999)]
    [Display(ResourceType = typeof(HomagConnect.Base.Contracts.Resources), Name = nameof(AdditionalProperties))]
    public IDictionary<string, object>? AdditionalProperties { get; set; }
}