using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Predict
{
    /// <summary>
    /// Specifies the method used to calculate a production duration prediction.
    /// </summary>
    /// <example>Machine</example>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum PredictionMethod
    {
        /// <summary>
        /// Fallback value when the prediction method is unknown.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Prediction based on data from a single machine.
        /// </summary>
        Machine,

        /// <summary>
        /// Prediction based on data from multiple machines in the current subscription.
        /// </summary>
        MachineGroup,

        /// <summary>
        /// Prediction based on data from a single workplace.
        /// </summary>
        Workplace,

        /// <summary>
        /// Prediction based on data from multiple workplaces in the current subscription.
        /// </summary>
        WorkplaceGroup,

        /// <summary>
        /// Prediction based on data from multiple machines across multiple subscriptions.
        /// </summary>
        Global = 9
    }
}