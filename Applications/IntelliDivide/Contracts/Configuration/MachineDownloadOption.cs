using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Configuration;

/// <summary>
/// Defines how optimization results are transferred to a machine that is not connected to HOMAG Connect.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum MachineDownloadOption
{
    /// <summary>
    /// The machine data is downloaded manually.
    /// </summary>
    Download,

    /// <summary>
    /// The machine data is transferred using the HOMAG File Agent.
    /// </summary>
    FileAgent
}
