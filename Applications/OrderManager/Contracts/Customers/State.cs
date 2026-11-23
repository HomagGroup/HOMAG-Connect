namespace HomagConnect.OrderManager.Contracts.Customers
{
    /// <summary>
    /// Registration and activation state of a customer.    /// </summary>
    public enum State
    {
        /// <summary>
        /// The customer is approved and active.        /// </summary>
        Approved,

        /// <summary>
        /// Registration is pending and not yet <see cref="Approved"/>
        /// </summary>
        Pending,

        /// <summary>
        /// The customer is blocked.        /// </summary>
        Blocked,

        /// <summary>
        /// The customer is archived.        /// </summary>
        Archived
    }
}
