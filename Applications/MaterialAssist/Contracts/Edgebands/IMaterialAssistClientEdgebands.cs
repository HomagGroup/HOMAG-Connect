using HomagConnect.Base.Contracts;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Contracts.Storage;
using HomagConnect.MaterialAssist.Contracts.Update;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialAssist.Contracts.Edgebands
{
    /// <summary>
    /// Interface for MaterialAssist Edgebands Client.
    /// </summary>
    public interface IMaterialAssistClientEdgebands
    {
        #region Create

        /// <summary>
        /// Creates the edgeband type in materialManager.
        /// </summary>
        /// <returns>The created edgeband type <see cref="EdgebandType" />.</returns>
        Task<EdgebandType> CreateEdgebandType(MaterialManagerRequestEdgebandType edgebandTypeRequest);

        /// <summary>
        /// Create an edgeband entity in materialAssist.
        /// </summary>
        /// <returns>The created edgeband entity <see cref="EdgebandEntity" />.</returns>
        Task<EdgebandEntity> CreateEdgebandEntity(MaterialAssistRequestEdgebandEntity edgebandEntityRequest);

        #endregion Create

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
        Task<IEnumerable<EdgebandEntity>?> GetEdgebandEntities(int take, int skip = 0);

        /// <summary>
        /// Gets only the edgeband entities changed since the given date.
        /// </summary>
        Task<IEnumerable<EdgebandEntity>?> GetEdgebandEntities(DateTimeOffset changedSince, int take, int skip = 0);

        /// <summary>
        /// Gets an edgeband entity by id (#).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EdgebandEntity?> GetEdgebandEntityById(string id);

        /// <summary>
        /// Gets an edgeband entities by id (#).
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IEnumerable<EdgebandEntity?>> GetEdgebandEntitiesByIds(IEnumerable<string> ids);

        /// <summary>
        /// Get edgeband entities by edgeband code.
        /// </summary>
        /// <param name="edgebandCode"></param>
        /// <returns></returns>
        Task<IEnumerable<EdgebandEntity>?> GetEdgebandEntitiesByEdgebandCode(string edgebandCode);

        /// <summary>
        /// Get edgeband entities by edgeband codes.
        /// </summary>
        /// <param name="edgebandCodes"></param>
        /// <returns></returns>
        Task<IEnumerable<EdgebandEntity>> GetEdgebandEntitiesByEdgebandCodes(IEnumerable<string> edgebandCodes);

        /// <summary>
        /// Gets the available storage locations.
        /// </summary>
        Task<IEnumerable<StorageLocation>> GetStorageLocations();

        /// <summary>
        /// Gets the available storage locations for a specific workstationId.
        /// </summary>
        /// <param name="workstationId"></param>
        Task<IEnumerable<StorageLocation>> GetStorageLocations(string workstationId);

        /// <summary>
        /// Get workstations.
        /// </summary>
        Task<IEnumerable<Workstation>> GetWorkstations();

        #endregion Read

        #region Update

        /// <summary>
        /// Update edgeband entity.
        /// </summary>
        Task<EdgebandEntity> UpdateEdgebandEntity(string id, MaterialAssistUpdateEdgebandEntity updateEdgebandEntity);

        /// <summary>
        /// Adds the edgeband entity to storage.
        /// </summary>
        Task StoreEdgebandEntity(MaterialAssistStoreEdgebandEntity storeEdgebandEntity);

        /// <summary>
        /// Adds the edgeband entity to storage.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="storageLocation"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        Task StoreEdgebandEntity(string id, StorageLocation storageLocation, double length);

        /// <summary>
        /// Removes the edgeband entity from storage. Available for all ManagementTypes.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deleteFromInventory"></param>
        /// <returns></returns>
        Task RemoveAllEdgebandEntitiesFromWorkplace(string id, bool deleteFromInventory = false);

        /// <summary>
        /// Remove a subset of edgeband entities from storage. Only available for ManagementTypes GoodsInStock and Stack.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <param name="deleteFromInventory"></param>
        /// <returns></returns>
        Task RemoveSubsetEdgebandEntitiesFromWorkplace(string id, int quantity, bool deleteFromInventory = false);

        /// <summary>
        /// Removes single edgeband entity / entities from storage. Only available for ManagementTypes GoodsInStock and Stack.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <param name="deleteFromInventory"></param>
        /// <returns></returns>
        Task RemoveSingleEdgebandEntitiesFromWorkplace(string id, int quantity, bool deleteFromInventory = false);

        #endregion Update
    }
}