#nullable enable
using HomagConnect.ProductionManager.Contracts.ProductionItems;

namespace HomagConnect.ProductionAssist.Contracts
{
    /// <summary>
    /// productionAssist Client
    /// </summary>
    public interface IProductionAssistClient
    {
        /// <summary>
        /// Gets the <see cref="ProductionItemBase" /> for a given <see cref="ProductionItemBase.Id" /> id or
        /// <see cref="ProductionItemBase.Barcode" /> which belongs to any not archived order.
        /// </summary>
        Task<ProductionItemBase?> GetOrderItem(string identifier);

        /// <summary>
        /// Gets the <see cref="ProductionItemBase" />s for given <see cref="ProductionItemBase.Id" />s or
        /// <see cref="ProductionItemBase.Barcode" />s which belongs to any not archived order.
        /// </summary>
        Task<ProductionItemBase[]?> GetOrderItems(string[] identifiers);

        /// <summary>
        /// Retrieve the list all workstations.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Workstation>?> GetWorkstations();
    }
}