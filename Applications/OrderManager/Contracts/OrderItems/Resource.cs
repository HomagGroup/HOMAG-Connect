

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// A (hardware) resource.
/// </summary>
public class Resource : ComponentBase
{
    /// <inheritdoc cref="Base" />
    public override Type Type
    {
        get
        {
            return Type.Resource;
        }
    }

    /// <summary>
    /// Gets or sets the height.
    /// </summary>
    public double? Height { get; set; }
}