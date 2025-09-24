using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts.Enumerations;

/// <summary>
/// The type of the workstation.
/// </summary>
[ResourceManager(typeof(WorkstationTypeDisplayNames))]
[JsonConverter(typeof(TolerantEnumConverter))]
public enum WorkstationType
{
    /// <summary>
    /// The workstation is a Boards one
    /// </summary>
    Boards,

    /// <summary>
    /// The workstation is an Edgebanding one
    /// </summary>
    Edgebanding,
}
