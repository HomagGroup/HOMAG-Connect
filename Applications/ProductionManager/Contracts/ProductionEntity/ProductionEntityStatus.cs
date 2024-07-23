using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionEntity;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Production entity status.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum ProductionEntityStatus
{
    /// <summary>
    /// Unknown
    /// </summary>
    Unknown,

    /// <summary>
    /// New
    /// </summary>
    New,

    /// <summary>
    /// Ready for production
    /// </summary>
    ReadyForProduction,

    /// <summary>
    /// In Production
    /// </summary>
    InProduction,

    /// <summary>
    /// Completed
    /// </summary>
    Completed,
}