// Note: This is preliminary code and is subject to change

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Type of group.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
[ResourceManager(typeof(GroupTypeDisplayNames))]
public enum GroupType
{
    /// <summary>
    /// Default group type or if nothing is specified
    /// </summary>
    Default,

    /// <summary>
    /// Contains the optimization boards as positions
    /// </summary>
    OptimizationBoards,

    /// <summary>
    /// Contains the optimization offcuts as positions
    /// </summary>
    OptimizationOffcuts,

    /// <summary>
    /// Contains the optimization edge bands as positions
    /// </summary>
    OptimizationEdgeBands,
}  