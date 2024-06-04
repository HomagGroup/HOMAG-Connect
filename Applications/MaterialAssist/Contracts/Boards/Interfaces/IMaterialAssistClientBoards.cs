using HomagConnect.MaterialAssist.Contracts.Base;
using HomagConnect.MaterialAssist.Contracts.Base.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Boards;

namespace HomagConnect.MaterialAssist.Contracts.Boards.Interfaces
{
    public interface IMaterialAssistClientBoards
    {
        #region Delete

        /// <summary>
        /// Deletes a board from the inventory by its code (#).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteBoardById(string id);

        #endregion Delete

        #region Read

        /// <summary>
        /// Get all board entities from the inventory.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<BoardEntity>> GetBoardEntities(int take, int skip = 0);

        /// <summary>
        /// Get board entity by id (#).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BoardEntity> GetBoardEntityById(string id);

        /// <summary>
        /// Get board entities by id´s (#).
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IEnumerable<BoardEntity>> GetBoardEntitiesByIds(IEnumerable<string> ids);

        /// <summary>
        /// Get board entity by id (#) including details.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BoardEntityDetails> GetBoardEntityByIdIncludingDetails(string id);

        /// <summary>
        /// Get boards by code including details.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IEnumerable<BoardEntityDetails>> GetBoardEntitiesByIdsIncludingDetails(IEnumerable<string> ids);

        /// <summary>
        /// Get board entities by board code.
        /// </summary>
        /// <param name="boardCode"></param>
        /// <returns></returns>
        Task<IEnumerable<BoardEntity>> GetBoardEntitiesByBoardCode(string boardCode);

        /// <summary>
        /// Get board entities by board code including details.
        /// </summary>
        /// <param name="boardCode"></param>
        /// <returns></returns>
        Task<IEnumerable<BoardEntityDetails>> GetBoardEntitiesByBoardCodeIncludingDetails(string boardCode);

        /// <summary>
        /// Get board entities by board codes.
        /// </summary>
        /// <param name="boardCodes"></param>
        /// <returns></returns>
        Task<IEnumerable<BoardEntity>> GetBoardEntitiesByBoardCodes(IEnumerable<string> boardCodes);

        /// <summary>
        /// Get board entities by board codes including details.
        /// </summary>
        /// <param name="boardCodes"></param>
        /// <returns></returns>
        Task<IEnumerable<BoardEntityDetails>> GetBoardEntitiesByBoardCodesIncludingDetails(IEnumerable<string> boardCodes);

        /// <summary>
        /// Get storage locations.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<StorageLocation>> GetStorageLocations();

        #endregion Read

        #region Create

        /// <summary>
        /// Create a board instance for a certain boardType.
        /// </summary>
        /// <param name="boardTypeCode"></param>
        /// <param name="quantity"></param>
        /// <param name="managementType"></param>
        /// <param name="comments"></param>
        /// <param name="storageLocation"></param>
        /// <param name="printLabel"></param>
        /// <returns></returns>
        Task CreateBoardEntity(string boardTypeCode, int quantity, ManagementType managementType, string comments,
            StorageLocation storageLocation, bool printLabel = false);

        /// <summary>
        /// Create a board type.
        /// </summary>
        /// <param name="boardType"></param>
        /// <returns></returns>
        Task CreateBoardType(BoardType boardType);

        #endregion Create

        #region Update

        /// <summary>
        /// Update board entity dimensions by its id (#).
        /// </summary>
        /// <param name="id"></param>
        /// <param name="length"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        Task UpdateBoardEntityDimensions(string id, double length, double width);

        /// <summary>
        /// Update board entity comments by its id (#).
        /// </summary>
        /// <param name="id"></param>
        /// <param name="comments"></param>
        /// <returns></returns>
        Task UpdateBoardEntityComment(string id, string comments);

        /// <summary>
        /// Print a label for a board by its code (#).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task PrintLabel(string id);

        /// <summary>
        /// Store a board by its code (#).
        /// </summary>
        /// <param name="id"></param>
        /// <param name="length"></param>
        /// <param name="width"></param>
        /// <param name="storageLocation"></param>
        /// <param name="printLabel"></param>
        /// <returns></returns>
        Task StoreBoardEntity(string id, int length, int width, StorageLocation storageLocation, bool printLabel = false);

        /// <summary>
        /// Remove a board by its code (#).
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deleteBoardFromInventory"></param>
        /// <returns></returns>
        Task RemoveBoardEntity(string id, bool deleteBoardFromInventory = false);

        #endregion Update
    }
}