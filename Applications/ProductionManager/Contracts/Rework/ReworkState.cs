using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Rework
{
    /// <summary>
    /// Workflow state of a rework. Serialized as the member name.
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    [ResourceManager(typeof(ReworkStateDisplayNames))]
    public enum ReworkState
    {
        /// <summary>
        /// The state could not be determined. Do not use it when creating reworks.
        /// </summary>
        Unknown,

        /// <summary>
        /// The rework was captured and is waiting for a decision.
        /// </summary>
        Pending,

        /// <summary>
        /// The rework was approved and the parts are produced again.
        /// </summary>
        Approved,

        /// <summary>
        /// The rework was rejected; see the rejection details.
        /// </summary>
        Rejected,

        /// <summary>
        /// The rework was handed over to production.
        /// </summary>
        Transferred
    }
}