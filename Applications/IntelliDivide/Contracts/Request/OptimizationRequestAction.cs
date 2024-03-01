using HomagConnect.IntelliDivide.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    /// <summary>
    /// Defines the actions which should get performed on a the request.
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum OptimizationRequestAction
    {
        /// <summary>
        /// Creates a new optimization in intelliDivide using the provided data.
        /// </summary>
        ImportOnly,

        /// <summary>
        /// Creates a new optimization in intelliDivide using the provided data and executes the optimization.
        /// </summary>
        Optimize,

        /// <summary>
        /// Creates a new optimization in intelliDivide using the provided data, executes the optimization and sends the balanced
        /// solution to the machine.
        /// </summary>
        Send
    }
}