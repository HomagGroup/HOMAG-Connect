

// Note: This is preliminary code and is subject to change

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
    public double? Height { get; set; }
}