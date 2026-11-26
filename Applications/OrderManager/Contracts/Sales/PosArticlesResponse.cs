namespace HomagConnect.OrderManager.Contracts.Sales
{
    
    /// <summary>
    /// Paginated response containing a list of POS articles.
    /// </summary>
    public class PosArticlesResponse
    {
        /// <summary>
        /// List of POS articles returned in the current page.
        /// </summary>
        public IList<PosArticle> Data { get; set; } = new List<PosArticle>();

        /// <summary>
        /// Maximum number of articles requested per page.
        /// </summary>
        /// <example>50</example>
        public int Take { get; set; }

        /// <summary>
        /// Number of articles skipped before the current page.
        /// </summary>
        /// <example>0</example>
        public int Skip { get; set; }
    }
}
