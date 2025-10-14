using System;

namespace HomagConnect.MaterialManager.Contracts.Common;

/// <summary>
/// Returned response when querying the state of an import process.
/// </summary>
public class ImportStateResponse
{
    /// <summary>
    /// Correlation id of the import process.
    /// </summary>
    public string CorrelationId { get; set; } = string.Empty;
    
    /// <summary>
    /// State of the import process.
    /// </summary>
    public ImportState ImportState { get; set; }
    
    /// <summary>
    /// The time when the import process was finished sucessfully.
    /// </summary>
    public DateTimeOffset? ImportSuccessTime { get; set; }
}