using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts
{
    /// <summary>The state of an optimization</summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum OptimizationStatus
    {
        /// <summary>
        /// Fallback state, if the state is not known; should normally not happen
        /// </summary>
        Unknown,

        /// <summary>
        /// The has been saved but the optimization has been started yet.
        /// </summary>
        New,

        /// <summary>The execution of the optimization has been started.</summary>
        Started,

        /// <summary>The execution of the optimization was canceled.</summary>
        Canceled,

        /// <summary>The execution failed.</summary>
        Faulted,

        /// <summary>The execution was finished successfully.</summary>
        Optimized,

        /// <summary>The job has been archived</summary>
        Archived,

        /// <summary>
        /// The optimization result has been downloaded or transferred to machine.
        /// </summary>
        Transferred,

        /// <summary>
        /// Download of SAW-File to machine pending or in progress.
        /// </summary>
        Transferring = 9,
    }
}