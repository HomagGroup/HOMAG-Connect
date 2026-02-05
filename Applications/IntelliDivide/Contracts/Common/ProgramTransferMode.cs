using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Common
{
    /// <summary>
    /// Specifies how the machine program (e.g., saw, ptx, mpr) is transferred to the machine.
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    [ResourceManager(typeof(ProgramTransferModeDisplayNames))]
    public enum ProgramTransferMode
    {
        /// <summary>
        /// Program is manually downloaded and copied to the machine’s shared directory.
        /// </summary>
        ManualDownload,

        /// <summary>
        /// Program is transferred to productionAssist which will handle further processing.
        /// </summary>
        ProductionAssist,

        /// <summary>
        /// Program is automatically downloaded by the HOMAG File agent and copied to the machine’s shared directory.
        /// </summary>
        FileAgent,

        /// <summary>
        /// Program is sent directly to the machine via tapio.
        /// </summary>
        MachineConnection
    }
}
