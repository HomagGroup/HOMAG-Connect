using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.ConfigurationDataManagement.CustomTables;

/// <summary>
/// The materialManager data source a custom table is synchronized with.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum CustomTableSyncDataSource
{
    /// <summary>
    /// No specific data source, or a data source this client does not know yet.
    /// </summary>
    Default,

    /// <summary>
    /// Textures (decors) of materialManager.
    /// </summary>
    Textures,

    /// <summary>
    /// Boards of materialManager.
    /// </summary>
    Boards,

    /// <summary>
    /// Edgebands of materialManager.
    /// </summary>
    Edgebands,

    /// <summary>
    /// Hardware of materialManager.
    /// </summary>
    Hardware
}
