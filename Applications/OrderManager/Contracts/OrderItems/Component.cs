using HomagConnect.Base.Contracts.Enumerations;

using Type = HomagConnect.OrderManager.Contracts.OrderItems.Type;

namespace HomagConnect.OrderManager.Contracts.OrderItems;

public class Component : OrderItems.Base
{
    /// <summary>
    /// Gets or sets the unit system.
    /// </summary>
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;

    /// <inheritdoc cref="Base"/>
    public override Type Type
    {
        get
        {
            return Type.Component;
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