using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.ProductionAssist.Contracts.Cutting.Enumerations
{
    /// <summary>
    /// Production state of a cutting job. Serialized as the member name.
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum CuttingState
    {
        /// <summary>
        /// Production has not started yet.
        /// </summary>
        NotStarted,

        /// <summary>
        /// Production has started; at least one part was produced.
        /// </summary>
        Started,

        /// <summary>
        /// Production is finished; all parts were produced.
        /// </summary>
        Finished,

        /// <summary>
        /// The cutting job was archived.
        /// </summary>
        Archived
    }
}