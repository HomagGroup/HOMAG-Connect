using HomagConnect.Base.Contracts;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Contracts.Update;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialAssist.Contracts.Boards
{
    public interface IMaterialAssistClientBoards
    {
        #region Delete

        /// <summary>
        /// Deletes a board from the inventory by their ids (#).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteBoardEntity(string id);

        /// <summary>
        /// Delete a board entities from the inventory by their ids (#).
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task DeleteBoardEntities(IEnumerable<string> ids);


        #endregion Delete

        #region Read

        /// <summary>
        /// Get all board entities from the inventory.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<BoardEntity>?> GetBoardEntities(int take, int skip = 0);

        /// <summary>
        /// Get board entity by id (#).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BoardEntity?> GetBoardEntityById(string id);

        /// <summary>
        /// Get board entities by id´s (#).
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IEnumerable<BoardEntity>> GetBoardEntitiesByIds(IEnumerable<string> ids);

        /// <summary>
        /// Get board entities by board code.
        /// </summary>
        /// <param name="boardCode"></param>
        /// <returns></returns>
        Task<IEnumerable<BoardEntity>?> GetBoardEntitiesByBoardCode(string boardCode);

        /// <summary>
        /// Get board entities by board codes.
        /// </summary>
        /// <param name="boardCodes"></param>
        /// <returns></returns>
        Task<IEnumerable<BoardEntity>> GetBoardEntitiesByBoardCodes(IEnumerable<string> boardCodes);

        /// <summary>
        /// Get board entities by material code.
        /// </summary>
        /// <param name="materialCode"></param>
        /// <returns></returns>
        Task<IEnumerable<BoardEntity>?> GetBoardEntitiesByMaterialCode(string materialCode);

        /// <summary>
        /// Get board entities by material codes.
        /// </summary>
        /// <param name="materialCodes"></param>
        /// <returns></returns>
        Task<IEnumerable<BoardEntity>> GetBoardEntitiesByMaterialCodes(IEnumerable<string> materialCodes);

        /// <summary>
        /// Get storage locations.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<StorageLocation>> GetStorageLocations();

        /// <summary>
        /// Get storage locations by workstationId.
        /// </summary>
        /// <param name="workstationId"></param>
        /// <returns></returns>
        Task<IEnumerable<StorageLocation>> GetStorageLocations(string workstationId);


        #endregion Read

        #region Create

        /// <summary>
        /// Creates the board type in materialManager.
        /// </summary>
        /// <returns>The created board type <see cref="BoardType" />.</returns>
        Task<BoardType> CreateBoardType(MaterialManagerRequestBoardType boardTypeRequest);

        /// <summary>
        /// Create a board entity in materialAssist.
        /// </summary>
        /// <returns>The created board entity <see cref="BoardEntity" />.</returns>
        Task<BoardEntity> CreateBoardEntity(MaterialAssistRequestBoardEntity boardEntityRequest);

        /// <summary>
        /// Create a offcut entity in materialAssist.
        /// </summary>
        /// <param name="offcutEntityRequest"></param>
        /// <returns></returns>
        Task<BoardEntity> CreateOffcutEntity(MaterialAssistRequestOffcutEntity offcutEntityRequest);

        #endregion Create

        #region Update

        /// <summary>
        /// Update board entity by its id (#).
        /// </summary>
        Task<BoardEntity> UpdateBoardEntity(string id, MaterialAssistUpdateBoardEntity updateBoardEntity);

        /// <summary>
        /// Store a board by its code (#).
        /// </summary>
        /// <param name="id"></param>
        /// <param name="length"></param>
        /// <param name="width"></param>
        /// <param name="storageLocation"></param>
        /// <returns></returns>
        Task StoreBoardEntity(string id, int length, int width, StorageLocation storageLocation);

        /// <summary>
        /// Remove all board entities by its code (#). This is available for all ManagementTypes.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deleteBoardFromInventory"></param>
        /// <returns></returns>
        Task RemoveAllBoardEntitiesFromWorkplace(string id, bool deleteBoardFromInventory = false);

        /// <summary>
        /// Remove a single board entity / entities by its code (#). This is available for ManagementTypes GoodsInStock and Stack.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <param name="deleteBoardFromInventory"></param>
        /// <returns></returns>
        Task RemoveSingleBoardEntitiesFromWorkplace(string id, int quantity, bool deleteBoardFromInventory = false);

        /// <summary>
        /// Remove a subset of board entities by its code (#). This is available for ManagementTypes GoodsInStock and Stack.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <param name="deleteBoardFromInventory"></param>
        /// <returns></returns>
        Task RemoveSubsetBoardEntitiesFromWorkplace(string id, int quantity, bool deleteBoardFromInventory = false);

        #endregion Update
    }
}