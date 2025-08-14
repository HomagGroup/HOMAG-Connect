using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Interfaces;
using HomagConnect.MaterialManager.Contracts.Update;

namespace HomagConnect.MaterialManager.Samples.Update.Allocations
{
    /// <summary>
    /// </summary>
    public class UpdateBoardTypeAllocationSamples
    {
        /// <summary>
        /// The example shows how to update a board type allocation.
        /// </summary>
        public static async Task Allocations_CreateBoardTypeAllocation(IMaterialManagerClientMaterialBoards materialManager)
        {
            var boardTypeAllocationUpdate = new BoardTypeAllocationUpdate
            {
                BoardTypeCode = "HPL_F274_9_12.0_4100.0_650.0",
                Name = "HPL_F274_9_12.0_4100.0_650.0_Allocation",
                Comments = "Comments",
                Quantity = 1,
                Source = "MaterialManager",
                Workstation = "Workstation1"
            };

            var result = await materialManager.UpdateBoardTypeAllocation(boardTypeAllocationUpdate.Name, boardTypeAllocationUpdate);
            result.Trace();
        }
    }
}