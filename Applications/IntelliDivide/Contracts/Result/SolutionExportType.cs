using HomagConnect.IntelliDivide.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum SolutionExportType
    {
        /// <summary>Cutting saw file</summary>
        Saw,

        /// <summary>Cutting ptx file</summary>
        Ptx,

        Pdf,

        MaterialDemand
    }
}