namespace HomagConnect.OrderManager.Contracts.Items;

public class Component : OrderItemBase
{

    /// <inheritdoc cref="OrderItemBase"/>
    public override OrderItemType Type
    {
        get
        {
            return OrderItemType.Component;
        }
    }

    /// <summary>
    /// Gets or sets the thickness.
    /// </summary>
    public double? Thickness { get; set; }

    #region (20) Production

    /// <summary>
    /// Gets or sets the quantity planned.
    /// </summary>
    public int? QuantityPlanned { get; set; }

    /// <summary>
    /// Gets or sets the production route.
    /// </summary>
    public string? ProductionRoute { get; set; }

    /// <summary>
    /// Gets or sets the production order type.
    /// </summary>
    public string? ProductionOrderType { get; set; }

    #endregion
}