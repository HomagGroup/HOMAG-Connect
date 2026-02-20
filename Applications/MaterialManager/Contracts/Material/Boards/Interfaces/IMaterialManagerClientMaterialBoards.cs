using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialManager.Contracts.Common;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Statistics;
using HomagConnect.MaterialManager.Contracts.Update;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards.Interfaces
{
    /// <summary>
    /// materialMaterial Client for boards
    /// </summary>
    public interface IMaterialManagerClientMaterialBoards
    {
        /// <summary>
        /// Creates the board type in materialManager.
        /// </summary>
        /// <returns>The created board type <see cref="BoardType" />.</returns>
        Task<BoardType> CreateBoardType(MaterialManagerRequestBoardType boardTypeRequest);

        /// <summary>
        /// Creates the board type in materialManager.
        /// </summary>
        /// <returns>The created board type <see cref="BoardType" />.</returns>
        Task<BoardType> CreateBoardType(MaterialManagerRequestBoardType boardTypeRequest, FileReference[] fileReferences);

        /// <summary>
        /// Creates the board type allocation in materialManager.
        /// </summary>
        /// <returns>The created board type allocation <see cref="BoardTypeAllocation" />.</returns>
        Task<BoardTypeAllocation> CreateBoardTypeAllocation(BoardTypeAllocationRequest boardTypeAllocationRequest);

        /// <summary>
        /// Delete board type by board code.
        /// </summary>
        /// <param name="boardCode"></param>
        /// <returns></returns>
        Task DeleteBoardType(string boardCode);

        /// <summary>
        /// Deletes board type allocations by allocation names.
        /// </summary>
        Task DeleteBoardTypeAllocations(IEnumerable<string> allocationNames);

        /// <summary>
        /// Delete board types by board codes.
        /// </summary>
        Task DeleteBoardTypes(IEnumerable<string> boardCode);

        /// <summary>
        /// Gets the board type allocations paginated.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown, if take is greater than 1000.</exception>
        Task<IEnumerable<BoardTypeAllocation>?> GetBoardTypeAllocations(int take, int skip = 0);
        
        /// <summary>
        /// Gets only the board type allocations changed since a specific date.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown, if take is greater than 1000.</exception>
        Task<IEnumerable<BoardTypeAllocation>?> GetBoardTypeAllocations(DateTimeOffset changedSince, int take, int skip = 0);

        /// <summary>
        /// Gets the board type allocations paginated by allocation name.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown, if take is greater than 1000.</exception>
        Task<IEnumerable<BoardTypeAllocation>?> GetBoardTypeAllocationsByAllocationNames(IEnumerable<string> allocationNames, int take, int skip = 0);

        /// <summary>
        /// Gets the board type by board code.
        /// </summary>
        Task<BoardType?> GetBoardTypeByBoardCode(string boardCode);
        
        /// <summary>
        /// Gets the board type by board code including details (inventory, allocation, images).
        /// </summary>
        /// <param name="boardCode"></param>
        /// <returns></returns>
        Task<BoardTypeDetails?> GetBoardTypeByBoardCodeIncludingDetails(string boardCode);

        /// <summary>
        /// Gets the board types paginated.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown, if take is greater than 1000.</exception>
        Task<IEnumerable<BoardType>?> GetBoardTypes(int take, int skip = 0);

        /// <summary>
        /// Gets only the board types changed since a specific date.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown, if take is greater than 1000.</exception>
        Task<IEnumerable<BoardType>?> GetBoardTypes(DateTimeOffset changedSince, int take, int skip = 0);
        
        /// <summary>
        /// Gets the board types by board codes.
        /// </summary>
        /// <returns>The board types sorted by <see cref="BoardType.MaterialCode" /> and <see cref="BoardType.BoardCode" />.</returns>
        Task<IEnumerable<BoardType?>> GetBoardTypesByBoardCodes(IEnumerable<string> boardCodes);

        /// <summary>
        /// Gets the board types by board codes including details (inventory, allocation, images).
        /// </summary>
        /// <returns>The board types sorted by <see cref="BoardType.MaterialCode" /> and <see cref="BoardType.BoardCode" />.</returns>
        Task<IEnumerable<BoardTypeDetails?>> GetBoardTypesByBoardCodesIncludingDetails(IEnumerable<string> boardCodes);

        /// <summary>
        /// Gets the board types by material code.
        /// </summary>
        /// <returns>The board types sorted by <see cref="BoardType.BoardCode" />.</returns>
        Task<IEnumerable<BoardType>?> GetBoardTypesByMaterialCode(string materialCode);

        /// <summary>
        /// Gets the board types by material code including details (inventory, allocation, images).
        /// </summary>
        /// <returns>The board types sorted by <see cref="BoardType.BoardCode" />.</returns>
        Task<IEnumerable<BoardTypeDetails>?> GetBoardTypesByMaterialCodeIncludingDetails(string materialCode);

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
        /// Gets the board types paginated including details (inventory, allocation, images).
        /// </summary>
        /// <exception cref="ArgumentException">Thrown, if take is greater than 1000.</exception>
        Task<IEnumerable<BoardTypeDetails>?> GetBoardTypesIncludingDetails(int take, int skip = 0);
        
        /// <summary>
        /// Gets only the board types changed since a specific date including details (inventory, allocation, images).
        /// </summary>
        /// <exception cref="ArgumentException">Thrown, if take is greater than 1000.</exception>
        Task<IEnumerable<BoardTypeDetails>?> GetBoardTypesIncludingDetails(DateTimeOffset changedSince, int take, int skip = 0);

        /// <summary>
        /// Gets a paginated list of materials.
        /// </summary>
        /// <param name="take">The number of materials to return.</param>
        /// <param name="skip">The number of materials to skip.</param>
        /// <returns>A collection of <see cref="Material" />.</returns>
        Task<IEnumerable<Material>?> GetMaterials(int take, int skip = 0);
        
        /// <summary>
        /// Gets only the materials changed since a specific date.
        /// </summary>
        /// <param name="changedSince">The date to filter the materials.</param>
        /// <param name="take">The number of materials to return.</param>
        /// <param name="skip">The number of materials to skip.</param>
        /// <returns>A collection of <see cref="Material" />.</returns>
        Task<IEnumerable<Material>?> GetMaterials(DateTimeOffset changedSince, int take, int skip = 0);

        /// <summary>
        /// Gets the materials by material codes.
        /// </summary>
        /// <param name="materialCodes">A collection of material codes.</param>
        /// <returns>A collection of <see cref="Material" /> objects.</returns>
        Task<IEnumerable<Material>?> GetMaterialsByMaterialCodes(IEnumerable<string> materialCodes);

        /// <summary>
        /// Search the board type allocations.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown, if take is greater than 1000.</exception>
        Task<IEnumerable<BoardTypeAllocation>?> SearchBoardTypeAllocations(string search, int take, int skip = 0);

        /// <summary>
        /// Update board type by board code.
        /// </summary>
        Task<BoardType> UpdateBoardType(string boardTypeCode, MaterialManagerUpdateBoardType boardTypeUpdate);

        /// <summary>
        /// Update board type by board code with file references.
        /// </summary>
        Task<BoardType> UpdateBoardType(string boardTypeCode, MaterialManagerUpdateBoardType boardTypeUpdate, FileReference[] fileReferences);

        /// <summary>
        /// Updates the board type allocation in materialManager.
        /// </summary>
        /// <returns>The updated board type allocation <see cref="BoardTypeAllocation" />.</returns>
        Task<IEnumerable<BoardTypeAllocation>?> UpdateBoardTypeAllocation(string allocationName, BoardTypeAllocationUpdate boardTypeAllocationUpdate);

        #region Inventory History

        /// <summary>
        /// Get <see cref="BoardType" /> inventory history for specific material codes and <see cref="BoardTypeType" />.
        /// </summary>
        Task<IEnumerable<BoardTypeInventoryHistory>> GetBoardTypeInventoryHistoryAsync(IEnumerable<string> materialCodes, BoardTypeType boardTypeType, DateTime from, DateTime to);

        /// <summary>
        /// Get <see cref="BoardType" /> inventory history for all board types.
        /// </summary>
        Task<IEnumerable<BoardTypeInventoryHistory>> GetBoardTypeInventoryHistoryAsync(DateTime from, DateTime to);

        /// <summary>
        /// Get <see cref="BoardType" /> inventory history for specific material codes.
        /// </summary>
        Task<IEnumerable<BoardTypeInventoryHistory>> GetBoardTypeInventoryHistoryAsync(IEnumerable<string> materialCodes, DateTime from, DateTime to);

        /// <summary>
        /// Get <see cref="BoardType" /> inventory history for specific material codes and <see cref="BoardTypeType" />.
        /// </summary>
        Task<IEnumerable<BoardTypeInventoryHistory>> GetBoardTypeInventoryHistoryAsync(IEnumerable<string> materialCodes, BoardTypeType boardTypeType, int daysBack);

        /// <summary>
        /// Get <see cref="BoardType" /> inventory history for all board types.
        /// </summary>
        Task<IEnumerable<BoardTypeInventoryHistory>> GetBoardTypeInventoryHistoryAsync(int daysBack);

        /// <summary>
        /// Get <see cref="BoardType" /> inventory history for specific material codes.
        /// </summary>
        Task<IEnumerable<BoardTypeInventoryHistory>> GetBoardTypeInventoryHistoryAsync(IEnumerable<string> materialCodes, int daysBack);

        /// <summary>
        /// Get the PartHistory by interval dates
        /// </summary>
        Task<IEnumerable<PartHistory>?> GetPartHistoryAsync(DateTime from, DateTime to, int take, int skip = 0);

        /// <summary>
        /// Get the PartHistory by a fixed number of days back
        /// </summary>
        Task<IEnumerable<PartHistory>?> GetPartHistoryAsync(int daysBack, int take, int skip = 0);

        #endregion

        #region Storage Import
        
        /// <summary>
        /// /// Import storage inventory
        /// </summary>
        Task<ImportInventoryResponse> ImportInventory(ImportInventoryRequest data);

        /// <summary>
        /// Get import state by correlation id
        /// </summary>
        Task<ImportStateResponse> GetImportState(string correlationId);

        #endregion
    }
}