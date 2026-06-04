namespace HomagConnect.OrderManager.Contracts.Customers
{
    /// <summary>
    /// State
    /// </summary>
    public enum State
    {
        /// <summary>
        /// Approved
        /// </summary>
        Approved,

        /// <summary>
        /// Registration is pending and not yet <see cref="Approved"/>
        /// </summary>
        Pending,

        /// <summary>
        /// Blocked
        /// </summary>
        Blocked,

        /// <summary>
        /// Archived
        /// </summary>
        Archived
    }
}
