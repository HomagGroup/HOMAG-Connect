namespace HomagConnect.MaterialManager.Contracts.Common;

/// <summary>
/// Identifies an inventory import process so its progress can be associated with an <see cref="ImportStateResponse" />.
/// </summary>
/// <example>{ "correlationId": "import-2025-001" }</example>
public class ImportInventoryResponse
{
    /// <summary>
    /// Identifier of the import process, matching <see cref="ImportStateResponse.CorrelationId" /> in its state response.
    /// </summary>
    /// <example>import-2025-001</example>
    public string CorrelationId { get; set; }
}