using Newtonsoft.Json;
using HomagConnect.ProductionManager.Contracts.ProductionItems;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol
{
    /// <summary>
    /// Represents a processed position in a production protocol, including its id, description and quantity processed.
    /// </summary>
    public class ProcessedPosition : ProcessedOrderItem
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
