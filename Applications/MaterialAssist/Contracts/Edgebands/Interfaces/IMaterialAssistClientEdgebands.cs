using HomagConnect.MaterialAssist.Contracts.Base.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;

namespace HomagConnect.MaterialAssist.Contracts.Edgebands.Interfaces
{
    /// <summary>
    /// Interface for MaterialAssist Edgebands Client.
    /// </summary>
    public interface IMaterialAssistClientEdgebands
    {
        /// <summary>
        /// Gets all edgebands from inventory.
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        Task<IEnumerable<Edgeband>> GetEdgebandsFromInventory(int take, int skip = 0);

        /// <summary>
        /// Gets all edgebands from storage.
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        Task<IEnumerable<Edgeband>> GetEdgebandsFromStorage(int take, int skip = 0);

        /// <summary>
        /// Gets an edgeband by edgeband code.
        /// </summary>
        /// <param name="edgebandCode"></param>
        /// <returns></returns>
        Task<Edgeband> GetEdgebandByEdgebandCode(string edgebandCode);

        /// <summary>
        /// Gets an edgeband by edgeband code including details.
        /// </summary>
        /// <param name="edgebandCode"></param>
        /// <returns></returns>
        Task<EdgebandDetails> GetEdgebandByEdgebandCodeIncludingDetails(string edgebandCode);

        /// <summary>
        /// Gets an edgebands by edgeband code.
        /// </summary>
        /// <param name="edgebandCodes"></param>
        /// <returns></returns>
        Task<IEnumerable<Edgeband>> GetEdgebandsByEdgebandCodes(IEnumerable<string> edgebandCodes);

        /// <summary>
        /// Gets an edgebands by edgeband code including details.
        /// </summary>
        /// <param name="edgebandCodes"></param>
        /// <returns></returns>
        Task<IEnumerable<EdgebandDetails>> GetEdgebandsByEdgebandCodesIncludingDetails(IEnumerable<string> edgebandCodes);

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
        /// Prints the label by edgeband code.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task PrintLabelByEdgebandCode(string edgebandCode);

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
        Task<IEnumerable<string>> GetStorageLocations();

        /// <summary>
        /// Adds the edgeband to storage.
        /// </summary>
        /// <param name="edgebandCode"></param>
        /// <param name="storageLocation"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        Task AddEdgebandToStorageByEdgebandCode(string edgebandCode, string storageLocation, double length);

        /// <summary>
        /// Removes the edgeband from storage.
        /// </summary>
        /// <param name="edgebandCode"></param>
        /// <param name="deleteFromInventory"></param>
        /// <returns></returns>
        Task RemoveEdgebandFromStorageByEdgebandCode(string edgebandCode, bool deleteFromInventory = false);

        /// <summary>
        /// Adds a new edgeband.
        /// </summary>
        /// <param name="edgebandCode"></param>
        /// <param name="length"></param>
        /// <param name="quantity"></param>
        /// <param name="currentThickness"></param>
        /// <param name="managementType"></param>
        /// <param name="comments"></param>
        /// <param name="storageLocation"></param>
        /// <returns></returns>
        Task CreateEdgeband(string edgebandCode, double length, int quantity, double currentThickness, ManagementType managementType,
            string comments, string storageLocation);

        /// <summary>
        /// Create a new edgeband type.
        /// </summary>
        /// <param name="edgebandType"></param>
        /// <returns></returns>
        Task CreateEdgebandType(EdgebandType edgebandType);
    }
}