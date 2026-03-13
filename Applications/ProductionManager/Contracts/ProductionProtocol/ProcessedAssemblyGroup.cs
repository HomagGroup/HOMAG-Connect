using HomagConnect.ProductionManager.Contracts.ProductionItems;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol;

/// <summary>
/// Represents a processed assembly group in a production protocol.
/// </summary>
/// <example>
/// {
///   "type": "ProcessedAssemblyGroup",
///   "identifier": "AG-10",
///   "description": "Base Cabinet Assembly",
///   "quantity": 1,
///   "orderName": "Kitchen_4711",
///   "orderNumber": "4711",
///   "customerName": "Miller"
/// }
/// </example>
public class ProcessedAssemblyGroup : ProcessedOrderItem
{
    /// <inheritdoc />
    public override ProductionItemType ItemType
    {
        get
        {
            return ProductionItemType.AssemblyGroup;
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
            return ProcessedItemType.ProcessedAssemblyGroup;
        }
        // ReSharper disable once ValueParameterNotUsed
        set
        {
            // Ignored, needed for serialization
        }
    }
}