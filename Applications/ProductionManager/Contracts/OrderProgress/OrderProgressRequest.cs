namespace HomagConnect.ProductionManager.Contracts.OrderProgress
{
    /// <summary>
    /// request object for order progress data retrieval
    /// </summary>
    public class OrderProgressRequest
    {
        /// <summary>
        /// all OrderNumbers, for which the data must be retrieved
        /// </summary>
        public IEnumerable<string> OrderNumbers { get; set; } = new List<string>();

        /// <summary>
        /// All workplaces, for which data shall be retrieved
        /// </summary>
        public IEnumerable<Guid> WorkplaceIds { get; set; } = new List<Guid>();
    }
}