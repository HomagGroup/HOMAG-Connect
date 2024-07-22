using System.Collections.ObjectModel;

namespace HomagConnect.ProductionManager.Contracts.Details;

/// <summary>
/// Production entity component.
/// </summary>
public class ProductionEntityComponent : ProductionEntity
{
    /// <inheritdoc />
    public override ProductionEntityType Type { get; set; } = ProductionEntityType.Component;

    /// <summary>
    /// Gets or sets the production entities.
    /// </summary>
    public Collection<ProductionEntity>? ProductionEntities { get; set; }
}