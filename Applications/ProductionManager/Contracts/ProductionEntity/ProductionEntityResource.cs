namespace HomagConnect.ProductionManager.Contracts.ProductionEntity;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Production entity resource.
/// </summary>
public class ProductionEntityResource : ProductionEntity
{
    /// <inheritdoc />
    public override ProductionEntityType Type { get; set; } = ProductionEntityType.Resource;

}