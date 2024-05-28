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
        /// <param name="code"></param>
        /// <returns></returns>
        Task DeleteBoardByCode(int code);

        #endregion Delete

        #region Read

        /// <summary>
        /// Get all boards from the inventory.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Board>> GetBoardsFromInventory();

        /// <summary>
        /// Get all board from the storage
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Board>> GetBoardsFromStorage();

        /// <summary>
        /// Get board by code (#).
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<Board> GetBoardByCode(int code);

        /// <summary>
        /// Get boards by code (#).
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        Task<IEnumerable<Board>> GetBoardsByCodes(IEnumerable<int> codes);

        /// <summary>
        /// Get board by board code including details.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<BoardDetails> GetBoardByCodeIncludingDetails(int code);

        /// <summary>
        /// Get boards by code including details.
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        Task<IEnumerable<BoardDetails>> GetBoardsByCodesIncludingDetails(IEnumerable<int> codes);

        /// <summary>
        /// Get storage locations.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<string>> GetStorageLocations();

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
        Task CreateBoard(string boardTypeCode, int quantity, ManagementType managementType, string comments, string storageLocation,
            bool printLabel = false);

        /// <summary>
        /// Create a board type.
        /// </summary>
        /// <param name="boardType"></param>
        /// <returns></returns>
        Task CreateBoardType(BoardType boardType);

        #endregion Create

        #region Update

        /// <summary>
        /// Update a board properties by its code (#).
        /// </summary>
        /// <param name="code"></param>
        /// <param name="length"></param>
        /// <param name="width"></param>
        /// <param name="comments"></param>
        /// <returns></returns>
        Task UpdateBoardByCode(int code, double length, double width, string comments);

        /// <summary>
        /// Print a label for a board by its code (#).
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task PrintLabelByCode(int code);

        /// <summary>
        /// Store a board by its code (#).
        /// </summary>
        /// <param name="code"></param>
        /// <param name="length"></param>
        /// <param name="width"></param>
        /// <param name="storageLocation"></param>
        /// <param name="printLabel"></param>
        /// <returns></returns>
        Task StoreBoardByCode(int code, int length, int width, string storageLocation, bool printLabel = false);

        /// <summary>
        /// Remove a board by its code (#).
        /// </summary>
        /// <param name="code"></param>
        /// <param name="deleteBoardFromInventory"></param>
        /// <returns></returns>
        Task RemoveBoardByCode(int code, bool deleteBoardFromInventory = false);

        #endregion Update
    }
}