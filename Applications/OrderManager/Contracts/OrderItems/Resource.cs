namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// A (hardware) resource.
/// </summary>
public class Resource : Component
{
    /// <inheritdoc cref="Base" />
    public override Type Type
    {
        get
        {
            return Type.Resource;
        }
    }
}