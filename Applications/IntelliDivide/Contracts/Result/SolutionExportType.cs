using HomagConnect.IntelliDivide.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// The type of the solution export.
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum SolutionExportType
    {
        /// <summary>
        /// SAW file containing the solution to be used with HOMAG machines.
        /// </summary>
        Saw,

        /// <summary>
        /// PTX file containing the solution to be used with non HOMAG machines.
        /// </summary>
        Ptx,

        /// <summary>
        /// Handout describing the solution in pdf format.
        /// </summary>
        Pdf,

        /// <summary>
        /// Excel file containing the material demand.
        /// </summary>
        MaterialDemand
    }
}