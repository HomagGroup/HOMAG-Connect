using System.Diagnostics;

namespace HomagConnect.OrderManager.Contracts.Import;

/// <summary>
/// Optimization request class to use to create requests on object model.
/// </summary>
[DebuggerDisplay("Action={Action}")]
public class ImportOrderRequest
{
    /// <summary>
    /// Gets or sets the action which should get performed on import.
    /// </summary>
    public ImportOrderRequestAction Action { get; set; } = ImportOrderRequestAction.ImportOnly;
}