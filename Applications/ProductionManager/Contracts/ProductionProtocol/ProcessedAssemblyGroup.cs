using HomagConnect.ProductionManager.Contracts.ProductionItems;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol
{
    /// <summary>
    /// Represents a processed assembly group in a production protocol, including its id, description and quantity processed.
    /// </summary>
    public class ProcessedAssemblyGroup : ProcessedOrderItem
    {
        /// <inheritdoc />
        public override ProductionItemType ItemType
        {
            get
            {
                return ProductionItemType.AssemblyGroup;
            }
        }
    }
}
