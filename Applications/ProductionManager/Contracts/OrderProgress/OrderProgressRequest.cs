namespace HomagConnect.ProductionManager.Contracts.OrderProgress
{
    /// <summary>
    /// Request for the processed part totals of orders.
    /// </summary>
    /// <example>
    /// { "orderNumbers": [ "4711" ], "workplaceIds": [ "7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c" ] }
    /// </example>
    public class OrderProgressRequest
    {
        /// <summary>
        /// Order numbers to retrieve the progress for. Defaults to an empty list.
        /// </summary>
        /// <example>[ "4711" ]</example>
        public IEnumerable<string> OrderNumbers { get; set; } = new List<string>();

        /// <summary>
        /// Unique identifiers of the workstations to retrieve the progress for. Defaults to an empty list.
        /// </summary>
        /// <example>[ "7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c" ]</example>
        public IEnumerable<Guid> WorkplaceIds { get; set; } = new List<Guid>();
    }
}
