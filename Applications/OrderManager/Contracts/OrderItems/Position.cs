using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.OrderItems;

public class Position : Base
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    [JsonProperty(Order = 1)]
    public string Name { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 0)]
    public override Type Type
    {
        get
        {
            return Type.Position;
        }
    }

    /// <summary>
    /// Gets or sets the article number.
    /// </summary>
    [JsonProperty(Order = 2)]
    public string ArticleNumber { get; set; }
    
    [JsonProperty(Order = 3)]
    public string Description { get; set; }

    [JsonProperty(Order = 4)]
    public int Quantity { get; set; } = 1;


    
}