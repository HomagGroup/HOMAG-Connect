using HomagConnect.MaterialAssist.Contracts.Base;
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
        /// Gets all edgeband entities.
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        Task<IEnumerable<EdgebandEntity>> GetEdgebandEntities(int take, int skip = 0);
        
        /// <summary>
        /// Gets an edgeband entity by id (#).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EdgebandEntity> GetEdgebandEntityById(string id);

        /// <summary>
        /// Gets an edgeband entity by id (#) including details.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EdgebandEntityDetails> GetEdgebandEntityByIdIncludingDetails(string id);

        /// <summary>
        /// Gets an edgeband entities by id (#).
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IEnumerable<EdgebandEntity>> GetEdgebandEntitiesByIds(IEnumerable<string> ids);

        /// <summary>
        /// Gets an edgeband entities by id (#) including details.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IEnumerable<EdgebandEntityDetails>> GetEdgebandEntitiesByIdsIncludingDetails(IEnumerable<string> ids);

        /// <summary>
        /// Get edgeband entities by edgeband code.
        /// </summary>
        /// <param name="edgebandCode"></param>
        /// <returns></returns>
        Task<IEnumerable<EdgebandEntity>> GetEdgebandEntitiesByEdgebandCode(string edgebandCode);

        /// <summary>
        /// Get edgeband entities by edgeband code including details.
        /// </summary>
        /// <param name="edgebandCode"></param>
        /// <returns></returns>
        Task<IEnumerable<EdgebandEntityDetails>> GetEdgebandEntitiesByEdgebandCodeIncludingDetails(string edgebandCode);
        
        /// <summary>
        /// Get edgeband entities by edgeband codes.
        /// </summary>
        /// <param name="edgebandCodes"></param>
        /// <returns></returns>
        Task<IEnumerable<EdgebandEntity>> GetEdgebandEntitiesByEdgebandCodes(IEnumerable<string> edgebandCodes);

        /// <summary>
        /// Get edgeband entities by edgeband codes including details.
        /// </summary>
        /// <param name="edgebandCodes"></param>
        /// <returns></returns>
        Task<IEnumerable<EdgebandEntity>> GetEdgebandEntitiesByEdgebandCodesIncludingDetails(IEnumerable<string> edgebandCodes);

        /// <summary>
        /// Updates the edgeband entity by id (#).
        /// </summary>
        /// <param name="id"></param>
        /// <param name="length"></param>
        /// <param name="currentThickness"></param>
        /// <param name="comments"></param>
        /// <returns></returns>
        Task UpdateEdgeband(string id, double length, double currentThickness, string comments = "");

        /// <summary>
        /// Prints the label by id (#).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task PrintLabel(string id);

        /// <summary>
        /// Deletes the edgeband by id (#).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteEdgebandEntity(string id);

        /// <summary>
        /// Gets the available storage locations.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<StorageLocation>> GetStorageLocations();

        /// <summary>
        /// Adds the edgeband entity to storage.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="storageLocation"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        Task StoreEdgebandEntity(string id, string storageLocation, double length);

        /// <summary>
        /// Removes the edgeband entity from storage.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deleteFromInventory"></param>
        /// <returns></returns>
        Task RemoveEdgebandEntity(string id, bool deleteFromInventory = false);

        /// <summary>
        /// Adds a new edgeband entity.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="length"></param>
        /// <param name="quantity"></param>
        /// <param name="currentThickness"></param>
        /// <param name="managementType"></param>
        /// <param name="comments"></param>
        /// <param name="storageLocation"></param>
        /// <returns></returns>
        Task CreateEdgebandEntity(string id, double length, int quantity, double currentThickness, ManagementType managementType,
            string comments, StorageLocation storageLocation);

        /// <summary>
        /// Create a new edgeband type.
        /// </summary>
        /// <param name="edgebandType"></param>
        /// <returns></returns>
        Task CreateEdgebandType(EdgebandType edgebandType);
    }
}