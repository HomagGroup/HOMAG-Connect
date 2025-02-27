using Newtonsoft.Json;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;


/// <summary>
/// A (hardware) component.
/// </summary>
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
    /// Gets or sets the height.
    /// </summary>
    public double? Height { get; set; }
}