using Newtonsoft.Json;

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
    }

    /// <summary>
    /// Gets or sets the height.
    /// </summary>
    public double? Height { get; set; }
}