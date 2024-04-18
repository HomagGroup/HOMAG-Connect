using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards.Interfaces
{
    /// <summary>
    /// materialMaterial Client for boards
    /// </summary>
    public interface IMaterialManagerClientMaterialBoards
    {

        /// <summary>
        /// Gets the board types paginated
        /// </summary>
        /// <exception cref="ArgumentException">Thrown, if take is greater than 1000.</exception>
        Task<IEnumerable<BoardType>> GetBoardTypes(int take, int skip = 0);

        /// <summary>
        /// Gets the board type by board code.
        /// </summary>
        Task<BoardType> GetBoardTypeByBoardCode(string boardCode);

        /// <summary>
        /// Gets the board type by board code including details (inventory, allocation, images).
        /// </summary>
        /// <param name="boardCode"></param>
        /// <returns></returns>
        Task<BoardType> GetBoardTypeByBoardCodeIncludingDetails(string boardCode);

        /// <summary>
        /// Gets the board types by board codes.
        /// </summary>
        /// <returns>The board types sorted by <see cref="BoardType.MaterialCode" /> and <see cref="BoardType.BoardCode" />.</returns>
        Task<IEnumerable<BoardType>> GetBoardTypesByBoardCodes(IEnumerable<string> boardCodes);

        /// <summary>
        /// Gets the board types by material code.
        /// </summary>
        /// <returns>The board types sorted by <see cref="BoardType.BoardCode" />.</returns>
        Task<IEnumerable<BoardType>> GetBoardTypesByMaterialCode(string materialCode);

        /// <summary>
        /// Gets the board types by material code including details (inventory, allocation, images).
        /// </summary>
        /// <returns>The board types sorted by <see cref="BoardType.BoardCode" />.</returns>
        Task<IEnumerable<BoardTypeDetails>> GetBoardTypesByMaterialCodeIncludingDetails(string materialCode);

        /// <summary>
        /// Gets the board types by material codes.
        /// </summary>
        /// <returns>The board types sorted by <see cref="BoardType.MaterialCode" /> and <see cref="BoardType.BoardCode" />.</returns>
        Task<IEnumerable<BoardType>> GetBoardTypesByMaterialCodes(IEnumerable<string> materialCodes);

        /// <summary>
        /// Gets the board types by material codes including details (inventory, allocation, images).
        /// </summary>
        /// <returns>The board types sorted by <see cref="BoardType.MaterialCode" /> and <see cref="BoardType.BoardCode" />.</returns>
        Task<IEnumerable<BoardTypeDetails>> GetBoardTypesByMaterialCodesIncludingDetails(IEnumerable<string> materialCodes);

        /// <summary>
        /// Gets the board types by board codes including details (inventory, allocation, images).
        /// </summary>
        /// <returns>The board types sorted by <see cref="BoardType.MaterialCode" /> and <see cref="BoardType.BoardCode" />.</returns>
        Task<IEnumerable<BoardTypeDetails>> GetBoardTypesByBoardCodesIncludingDetails(IEnumerable<string> boardCodes);

    }
}