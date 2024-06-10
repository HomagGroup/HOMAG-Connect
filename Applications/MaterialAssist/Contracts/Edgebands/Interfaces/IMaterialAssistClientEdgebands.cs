using HomagConnect.MaterialAssist.Contracts.Base;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;

namespace HomagConnect.MaterialAssist.Contracts.Edgebands.Interfaces
{
    /// <summary>
    /// Interface for MaterialAssist Edgebands Client.
    /// </summary>
    public interface IMaterialAssistClientEdgebands
    {
        #region Delete

        /// <summary>
        /// Deletes the edgeband entity by its id (#).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteEdgebandEntity(string id);

        /// <summary>
        /// Deletes edgeband entities by their ids (#).
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task DeleteEdgebandEntity(IEnumerable<string> ids);

        #endregion Delete

        #region Read

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
        /// Gets an edgeband entities by id (#).
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IEnumerable<EdgebandEntity>> GetEdgebandEntitiesByIds(IEnumerable<string> ids);

        /// <summary>
        /// Get edgeband entities by edgeband code.
        /// </summary>
        /// <param name="edgebandCode"></param>
        /// <returns></returns>
        Task<IEnumerable<EdgebandEntity>> GetEdgebandEntitiesByEdgebandCode(string edgebandCode);

        /// <summary>
        /// Get edgeband entities by edgeband codes.
        /// </summary>
        /// <param name="edgebandCodes"></param>
        /// <returns></returns>
        Task<IEnumerable<EdgebandEntity>> GetEdgebandEntitiesByEdgebandCodes(IEnumerable<string> edgebandCodes);

        /// <summary>
        /// Gets the available storage locations.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<StorageLocation>> GetStorageLocations();

        #endregion Read

        #region Update

        /// <summary>
        /// Updates the edgeband entity dimensions by id (#).
        /// </summary>
        /// <param name="id"></param>
        /// <param name="length"></param>
        /// <param name="currentThickness"></param>
        /// <returns></returns>
        Task UpdateEdgebandEntityDimensions(string id, double length, double currentThickness);

        /// <summary>
        /// Updates the edgeband entity comments by id (#).
        /// </summary>
        /// <param name="id"></param>
        /// <param name="comments"></param>
        /// <returns></returns>
        Task UpdateEdgebandEntityComments(string id, string comments);

        /// <summary>
        /// Adds the edgeband entity to storage.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="storageLocation"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        Task StoreEdgebandEntity(string id, string storageLocation, double length);

        /// <summary>
        /// Removes the edgeband entity from storage. Available for all ManagementTypes.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deleteFromInventory"></param>
        /// <returns></returns>
        Task RemoveAllEdgebandEntities(string id, bool deleteFromInventory = false);

        /// <summary>
        /// Remove a subset of edgeband entities from storage. Only available for ManagementTypes GoodsInStock and Stack.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <param name="deleteFromInventory"></param>
        /// <returns></returns>
        Task RemoveSubsetEdgebandEntities(string id, int quantity, bool deleteFromInventory = false);

        /// <summary>
        /// Removes single edgeband entity / entities from storage. Only available for ManagementTypes GoodsInStock and Stack.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <param name="deleteFromInventory"></param>
        /// <returns></returns>
        Task RemoveSingleEdgebandEntities(string id, int quantity, bool deleteFromInventory = false);


        #endregion Update

        #region Create

        /// <summary>
        /// Adds a new edgeband entity.
        /// </summary>
        /// <param name="edgebandTypeCode"></param>
        /// <param name="edgebandEntity"></param>
        /// <returns></returns>
        Task<EdgebandEntity> CreateEdgebandEntity(string edgebandTypeCode, EdgebandEntity edgebandEntity);

        /// <summary>
        /// Create a new edgeband type.
        /// </summary>
        /// <param name="edgebandType"></param>
        /// <returns></returns>
        Task CreateEdgebandType(EdgebandType edgebandType);

        #endregion Create
    }
}