#nullable enable
using HomagConnect.ProductionManager.Contracts.ProductionItems;

namespace HomagConnect.ProductionAssist.Contracts
{
    /// <summary>
    /// Provides asynchronous operations to retrieve production order items by item identifier or barcode and to list
    /// available workstations for non-archived orders.
    /// <see href="https://docs.homag.cloud/docs/en/productionassist-tutorial-first-steps">Learn more about productionAssist</see>.
    /// </summary>
    /// <remarks>All operations are read-only and return <c>null</c> when no matching data is
    /// available.</remarks>
    public interface IProductionAssistClient
    {
        /// <summary>
        /// Gets the <see cref="ProductionItemBase" /> for a given <see cref="ProductionItemBase.Id" /> id or
        /// <see cref="ProductionItemBase.Barcode" /> which belongs to any not archived order.
        /// </summary>
        /// <param name="identifier">The id or barcode of the order item.</param>
        /// <returns>The order item, or <c>null</c> if it is not found.</returns>
        Task<ProductionItemBase?> GetOrderItem(string identifier);

        /// <summary>
        /// Gets the <see cref="ProductionItemBase" />s for given <see cref="ProductionItemBase.Id" />s or
        /// <see cref="ProductionItemBase.Barcode" />s which belongs to any not archived order.
        /// </summary>
        /// <param name="identifiers">The ids or barcodes of the order items.</param>
        /// <returns>The order items, or <c>null</c> if none are found.</returns>
        Task<ProductionItemBase[]?> GetOrderItems(string[] identifiers);

        /// <summary>
        /// Retrieve the list all workstations.
        /// </summary>
        /// <returns>The workstations, or <c>null</c> if none are available.</returns>
        Task<IEnumerable<Workstation>?> GetWorkstations();
    }
}