#nullable enable
using HomagConnect.ProductionManager.Contracts.ProductionEntity;

namespace HomagConnect.ProductionAssist.Contracts
{
    /// <summary>
    /// productionAssist Client
    /// </summary>
    public interface IProductionAssistClient
    {
        /// <summary>
        /// Gets the <see cref="ProductionEntity" /> for a given <see cref="ProductionEntity.Id" /> id or
        /// <see cref="ProductionEntity.Barcode" /> which belongs to any not archived order.
        /// </summary>
        Task<ProductionEntity?> GetOrderItem(string identifier);

        /// <summary>
        /// Gets the <see cref="ProductionEntity" />s for given <see cref="ProductionEntity.Id" />s or
        /// <see cref="ProductionEntity.Barcode" />s which belongs to any not archived order.
        /// </summary>
        Task<ProductionEntity[]?> GetOrderItems(string[] identifiers);
    }
}