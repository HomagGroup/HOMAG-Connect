﻿using System.Collections.ObjectModel;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.ProductionManager.Contracts.ProductionItems;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Orders;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Order details
/// </summary>
public class OrderDetails : Order
{
    /// <summary>
    /// Gets or sets the additional data.
    /// </summary>
    [JsonProperty(Order = 401)]
    public Collection<AdditionalDataEntity>? AdditionalData { get; set; }

    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 600)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Gets or sets the bill of materials.
    /// </summary>
    [JsonProperty(Order = 500)]
    public Collection<ProductionItemBase>? Items { get; set; }
}