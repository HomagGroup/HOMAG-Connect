using System.Diagnostics;

namespace HomagConnect.ProductionManager.Contracts.Import;

/// <summary>
/// Request that controls how an order is imported into productionManager.
/// </summary>
/// <example>
/// { "action": "ImportOnly" }
/// </example>
[DebuggerDisplay("Action={Action}")]
public class ImportOrderRequest
{
    /// <summary>
    /// Action to perform on import. Defaults to ImportOnly.
    /// </summary>
    public ImportOrderRequestAction Action { get; set; } = ImportOrderRequestAction.ImportOnly;
}
