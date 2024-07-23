using System.Collections.ObjectModel;

namespace HomagConnect.ProductionManager.Contracts.ProductionEntity;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Production entity component.
/// </summary>
public class ProductionEntityAssemblyUnit : ProductionEntity
{
    /// <inheritdoc />
    public override ProductionEntityType Type { get; set; } = ProductionEntityType.AssemblyUnit;

    /// <summary>
    /// Gets or sets the production entities.
    /// </summary>
    public Collection<Contracts.ProductionEntity.ProductionEntity>? ProductionEntities { get; set; }
}