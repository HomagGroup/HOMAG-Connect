using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Predict
{
    /// <summary>
    /// Method, that was taken to predict the edgebanding duration.
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum PredictionMethod
    {
        /// <summary>
        /// Fallback value, if the prediction method is unknown.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The prediction was based on data from a single machine. This method is selected when there is enough data for the
        /// machine.
        /// </summary>
        Machine,

        /// <summary>
        /// The prediction was made using data from multiple machines in the subscription. This method is chosen when there is
        /// insufficient data for a single machine or when no specific machine is specified.
        /// </summary>
        MachineGroup,

        /// <summary>
        /// The prediction was based on data from a single workplace. This method is selected when there is enough data for the
        /// workplace.
        /// </summary>
        Workplace,

        /// <summary>
        /// The prediction was made using data from multiple workplaces in the subscription. This method is chosen when there is
        /// insufficient data for a single workplace or when no specific machine is specified.
        /// </summary>
        WorkplaceGroup,

        /// <summary>
        /// The prediction was made using data from multiple machine in multiple subscriptions. This method is chosen when there is
        /// insufficient data available in the current subscription.
        /// </summary>
        Global = 9
    }
}