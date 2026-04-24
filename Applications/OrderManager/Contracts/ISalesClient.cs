using HomagConnect.OrderManager.Contracts.Sales;

namespace HomagConnect.OrderManager.Contracts
{
    /// <summary>
    /// Interface for the sales client providing CRUD operations on <see cref="PosArticle" />
    /// and read access to the sales <see cref="MasterData" />.
    /// </summary>
    public interface ISalesClient
    {
        #region PosArticle CRUD

        /// <summary>
        /// Create a new <see cref="PosArticle" />.
        /// </summary>
        /// <param name="article">The article to create.</param>
        /// <returns>The created article including any server-assigned values.</returns>
        Task<PosArticle> CreatePosArticle(PosArticle article);

        /// <summary>
        /// Get a specific <see cref="PosArticle" /> by its article id.
        /// </summary>
        /// <param name="articleId">The unique id of the article.</param>
        /// <returns>The matching article, or <c>null</c> if not found.</returns>
        Task<PosArticle?> GetPosArticle(string articleId);

        /// <summary>
        /// Get all <see cref="PosArticle" /> entries.
        /// </summary>
        /// <param name="take">The maximum number of articles to return.</param>
        /// <param name="skip">The number of articles to skip before starting to collect the result set.</param>
        /// <returns>The articles.</returns>
        Task<IEnumerable<PosArticle>> GetPosArticles(int take, int skip = 0);

        /// <summary>
        /// Update an existing <see cref="PosArticle" />.
        /// </summary>
        /// <param name="article">The article with updated values.</param>
        /// <returns>The updated article.</returns>
        Task<PosArticle> UpdatePosArticle(PosArticle article);

        /// <summary>
        /// Delete a <see cref="PosArticle" /> by its article id.
        /// </summary>
        /// <param name="articleId">The unique id of the article.</param>
        Task DeletePosArticle(string articleId);

        /// <summary>
        /// Delete a list of <see cref="PosArticle" /> by their article ids.
        /// </summary>
        /// <param name="articleIds">The unique ids of the articles.</param>
        Task DeletePosArticles(IEnumerable<string> articleIds);

        #endregion

        #region MasterData

        /// <summary>
        /// Get the sales <see cref="MasterData" /> containing the available modules and attributes.
        /// </summary>
        /// <param name="libraryId">The id of the library to query.</param>
        /// <returns>The current master data.</returns>
        Task<MasterData> GetMasterData(string libraryId);

        #endregion
    }
}
