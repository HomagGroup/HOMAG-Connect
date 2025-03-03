using Newtonsoft.Json;
using System.ComponentModel;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// An order position.
/// </summary>
public class Position : Base
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    [JsonProperty(Order = 1)]
    public string? Name { get; set; }

    /// <inheritdoc />
    public override Type Type
    {
        get
        {
            return Type.Position;
        }
        set
        {
            // Ignore
        }
    }

    /// <summary>
    /// Gets or sets the article number.
    /// </summary>
    [JsonProperty(Order = 2)]
    public string? ArticleNumber { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    [JsonProperty(Order = 3)]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the quantity.
    /// </summary>
    [JsonProperty(Order = 4)]
    public int Quantity { get; set; } = 1;

    /// <summary>
    /// Gets or sets the length.
    /// </summary>
    [JsonProperty(Order = 5)]
    public double? Length { get; set; }

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    [JsonProperty(Order = 6)]
    public double? Width { get; set; }

    /// <summary>
    /// Gets or sets the height.
    /// </summary>
    [JsonProperty(Order = 7)]
    public double? Height { get; set; }

    /// <summary>
    /// Gets or sets the state of the entity.
    /// </summary>
    [JsonProperty(Order = 2)]
    [DefaultValue(State.New)]
    public State State { get; set; } = State.New;

}