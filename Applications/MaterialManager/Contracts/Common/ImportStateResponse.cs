using System;

namespace HomagConnect.MaterialManager.Contracts.Common;

/// <summary>
/// Returned response when querying the state of an import process.
/// </summary>
public class ImportStateResponse
{
    string CorrelationId { get; set; } = string.Empty;
    
    ImportState ImportState { get; set; }
    
    DateTimeOffset? ImportSuccessTime { get; set; }
}