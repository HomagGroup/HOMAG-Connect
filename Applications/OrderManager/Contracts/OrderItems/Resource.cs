

// Note: This is preliminary code and is subject to change

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Hardware resource of an order.
/// </summary>
/// <example>
/// { "type": "Resource", "id": "R1", "articleNumber": "RS-100", "description": "Assembly kit", "quantity": 1, "state": "New" }
/// </example>
public class Resource : ComponentBase
{
    /// <inheritdoc cref="Base" />
    public override Type Type
    {
        get
        {
            return Type.Resource;
        }
        set
        {
            // Ignore
        }
    }

    /// <summary>
    /// Height. Optional.
    /// </summary>
    /// <example>35</example>
    [JsonProperty(Order = 15)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Height { get; set; }
}