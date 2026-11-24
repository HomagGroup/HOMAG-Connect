using Newtonsoft.Json;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;


/// <summary>
/// Hardware component of an order, for example a hinge or a handle.
/// </summary>
/// <example>
/// { "type": "Component", "id": "C1", "articleNumber": "HW-4711", "description": "Hinge 110 degrees", "quantity": 4, "state": "New" }
/// </example>
public class Component : ComponentBase
{

    /// <inheritdoc cref="Base" />
    [JsonProperty(Order = 0)]
    public override Type Type
    {
        get
        {
            return Type.Component;
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
    [JsonProperty(Order = 1)]
    public double? Height { get; set; }
}