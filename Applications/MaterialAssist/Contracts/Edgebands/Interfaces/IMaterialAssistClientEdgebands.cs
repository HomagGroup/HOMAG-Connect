namespace HomagConnect.MaterialAssist.Contracts.Edgebands.Interfaces
{
    /// <summary>
    /// Interface for MaterialAssist Edgebands Client.
    /// </summary>
    public interface IMaterialAssistClientEdgebands
    {
        /// <summary>
        /// Gets all edgebands.
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        Task<IEnumerable<EdgebandType>> GetEdgebands(int take, int skip = 0);

        /// <summary>
        /// Gets an edgeband by edgeband code.
        /// </summary>
        /// <param name="edgebandCode"></param>
        /// <returns></returns>
        Task<EdgebandType> GetEdgebandByEdgebandCode(string edgebandCode);

        /// <summary>
        /// Gets an edgeband by edgeband code.
        /// </summary>
        /// <param name="edgebandCodes"></param>
        /// <returns></returns>
        Task<IEnumerable<EdgebandType>> GetEdgebandsByEdgebandCodes(IEnumerable<string> edgebandCodes);

        /// <summary>
        /// Updates the edgeband by edgeband code.
        /// </summary>
        /// <param name="edgebandCode"></param>
        /// <param name="length"></param>
        /// <param name="currentThickness"></param>
        /// <param name="comments"></param>
        /// <returns></returns>
        Task UpdateEdgebandByEdgebandCode(string edgebandCode, double length, double currentThickness, string comments = "");

        /// <summary>
        /// Deletes the edgeband by edgeband code.
        /// </summary>
        /// <param name="edgebandCode"></param>
        /// <returns></returns>
        Task DeleteEdgebandByEdgebandCode(string edgebandCode);

        /// <summary>
        /// Gets the available shelf ids.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<string>> GetAvailableShelfIds();

        /// <summary>
        /// Adds the edgeband to storage.
        /// </summary>
        /// <param name="edgebandCode"></param>
        /// <param name="shelfId"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        Task AddEdgebandToStorage(string edgebandCode, string shelfId, double length);

        /// <summary>
        /// Removes the edgeband from storage.
        /// </summary>
        /// <param name="edgebandCode"></param>
        /// <param name="deleteFromInventory"></param>
        /// <returns></returns>
        Task RemoveEdgebandFromStorage(string edgebandCode, bool deleteFromInventory = false);

        /// <summary>
        /// Adds a new edgeband.
        /// </summary>
        /// <param name="edgebandCode"></param>
        /// <param name="length"></param>
        /// <param name="quantity"></param>
        /// <param name="currentThickness"></param>
        /// <param name="management"></param>
        /// <param name="comments"></param>
        /// <param name="shelfId"></param>
        /// <returns></returns>
        Task AddNewEdgeband(string edgebandCode, double length, int quantity, double currentThickness, string management,
            string comments, string shelfId);
    }
}