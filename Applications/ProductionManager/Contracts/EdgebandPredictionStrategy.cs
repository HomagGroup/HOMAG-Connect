namespace HomagConnect.ProductionManager.Contracts
{
    /// <summary>
    /// Strategy, that was taken to calculate the edgeband prediction for duration
    /// </summary>
    public enum EdgebandPredictionStrategy
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Data for the requested machine were good.
        /// Prediction is based on the concrete machine
        /// </summary>
        SingleMachine,

        /// <summary>
        /// Prediction is done on all machines of the customer/subscription
        /// </summary>
        MachineGroup,

        /// <summary>
        /// Global
        /// </summary>
        Global = 9
    }
}