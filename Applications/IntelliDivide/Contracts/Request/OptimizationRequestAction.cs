using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    /// <summary>
    /// Specifies the action to perform after submitting an optimization request.
    /// </summary>
    /// <example>Optimize</example>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum OptimizationRequestAction
    {
        /// <summary>
        /// Imports the provided data and creates a new optimization without starting it.
        /// The optimization must be started explicitly via a separate request.
        /// </summary>
        ImportOnly,

        /// <summary>
        /// Imports the provided data, creates a new optimization, and starts the optimization automatically.
        /// </summary>
        Optimize,

        /// <summary>
        /// Imports the provided data, creates and runs the optimization, and sends the balanced solution to the machine.
        /// </summary>
        Send
    }
}