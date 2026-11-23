using System.Diagnostics;

namespace HomagConnect.OrderManager.Contracts.Import;

/// <summary>
/// Request to import an order into productionManager.
/// </summary>
/// <example>
/// { "action": "ImportOnly" }
/// </example>
[DebuggerDisplay("Action={Action}")]
public class ImportOrderRequest
{
    /// <summary>
    /// Action to perform on import. Defaults to <see cref="ImportOrderRequestAction.ImportOnly" />.
    /// </summary>
    /// <example>ImportOnly</example>
    public ImportOrderRequestAction Action { get; set; } = ImportOrderRequestAction.ImportOnly;
}