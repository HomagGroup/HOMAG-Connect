namespace HomagConnect.OrderManager.Contracts.Sales
{
    
    /// <summary>
    /// Represents a paginated response containing a list of POS articles.
    /// </summary>
    public class PosArticlesResponse
    {
        /// <summary>
        /// Gets or sets the list of POS articles returned in the current page.
        /// </summary>
        public IList<PosArticle> Data { get; set; } = new List<PosArticle>();

        /// <summary>
        /// Gets or sets the maximum number of articles requested per page.
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// Gets or sets the number of articles skipped before the current page.
        /// </summary>
        public int Skip { get; set; }
    }
}
