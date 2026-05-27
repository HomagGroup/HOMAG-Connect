namespace HomagConnect.OrderManager.Contracts.Customers
{
    /// <summary>
    /// State
    /// </summary>
    public enum State
    {
        /// <summary>
        /// Active
        /// </summary>
        Active,

        /// <summary>
        /// Registration is pending and not yet <see cref="Active"/>
        /// </summary>
        Pending,

        /// <summary>
        /// Blocked
        /// </summary>
        Blocked,

        /// <summary>
        /// Deactivated
        /// </summary>
        Deactivated,

        /// <summary>
        /// Archived
        /// </summary>
        Archived
    }
}
