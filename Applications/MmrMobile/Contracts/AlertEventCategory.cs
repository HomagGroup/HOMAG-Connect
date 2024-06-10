namespace HomagConnect.MmrMobile.Contracts
{
    /// <summary>
    /// Grouping to categorize an AlertEvent
    /// </summary>
    public enum AlertEventCategory
    {
        /// <summary>
        /// Invalid category
        /// </summary>
        None = 0,

        /// <summary>
        /// Report category
        /// </summary>
        Report = 2,

        /// <summary>
        /// Information category
        /// </summary>
        Information = 3,

        /// <summary>
        /// Warning category
        /// </summary>
        Warning = 4,

        /// <summary>
        /// Error category
        /// </summary>
        Fault = 5,

        /// <summary>
        /// Alarm category
        /// </summary>
        Alarm = 6,

        /// <summary>
        /// Danger category
        /// </summary>
        Danger = 7,

        /// <summary>
        /// UserInstruction category
        /// </summary>
        UserInstruction = 8
    }
}