using HomagConnect.ProductionManager.Contracts.ProductionItems;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol
{
    /// <summary>
    /// Represents a processed position in a production protocol.
    /// </summary>
    /// <example>
    /// {
    ///   "type": "ProcessedPosition",
    ///   "identifier": "10",
    ///   "description": "Cabinet Position 10",
    ///   "quantity": 4,
    ///   "orderName": "Kitchen_4711",
    ///   "orderNumber": "4711",
    ///   "customerName": "Miller"
    /// }
    /// </example>
    public class ProcessedPosition : ProcessedOrderItem
    {
        /// <inheritdoc />
        public override ProductionItemType ItemType
        {
            get
            {
                return ProductionItemType.Position;
            }
            // ReSharper disable once ValueParameterNotUsed
            set
            {
                // Ignored, needed for serialization
            }
        }

        /// <inheritdoc />
        public override ProcessedItemType Type
        {
            get
            {
                return ProcessedItemType.ProcessedPosition;
            }
            // ReSharper disable once ValueParameterNotUsed
            set
            {
                // Ignored, needed for serialization
            }
        }
    }
}
