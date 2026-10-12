using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts.Enumerations;

/// <summary>
/// Represents the base type of a machine (e.g. saw, edge banding, cnc, ...).
/// </summary>
[ResourceManager(typeof(MachineBaseTypeDisplayNames))]
[JsonConverter(typeof(TolerantEnumConverter))]
public enum MachineBaseType
{
    /// <summary>
    /// Default value
    /// </summary>
    Unknown,

    /// <summary>
    /// Machine is a edge banding
    /// </summary>
    Edge,

    /// <summary>
    /// Machine is a saw
    /// </summary>
    Saw,

    /// <summary>
    /// Machine is a cnc or nesting
    /// </summary>
    Cnc,

    /// <summary>
    /// Machine is a sanding machine
    /// </summary>
    Sanding,

    /// <summary>
    /// Machine is a drilling machine
    /// </summary>
    Drilling,

    /// <summary>
    /// Machine is an assembly machine
    /// </summary>
    Assembly,

    /// <summary>
    /// Machine is a storing machine
    /// </summary>
    Storing,

    /// <summary>
    /// Machine is a stock
    /// </summary>
    Stock,

    /// <summary>
    /// Machine is a sorting machine
    /// </summary>
    Sorting,

    /// <summary>
    /// Machine is a shipping machine
    /// </summary>
    Shipping,

    /// <summary>
    /// Machine is a pre assembly machine
    /// </summary>
    PreAssembly,

    /// <summary>
    /// Machine is a lamination machine
    /// </summary>
    Lamination,

    /// <summary>
    /// Machine is a moulding machine
    /// </summary>
    Moulding,

    /// <summary>
    /// Machine is a packaging machine
    /// </summary>
    Packaging,

    /// <summary>
    /// Machine is a handling machine
    /// </summary>
    Handling
}
