using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Interfaces;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialManager.Samples.Create.Allocations
{
    /// <summary>
    /// </summary>
    public class CreateBoardTypeAllocationSamples
    {
        /// <summary>
        /// The example shows how create a board type allocation.
        /// </summary>
        public static async Task Allocations_CreateBoardTypeAllocation(IMaterialManagerClientMaterialBoards materialManager)
        {
            var boardTypeAllocationRequest = new BoardTypeAllocationRequest
            {
                BoardTypeCode = "HPL_F274_9_12.0_4100.0_650.0",
                Name = "HPL_F274_9_12.0_4100.0_650.0_Allocation",
                Comments = "Comments",
                CreatedBy = "User1",
                Quantity = 1,
                Source = "MaterialManager",
                Workstation = "Workstation1"
            };

            var result = await materialManager.CreateBoardTypeAllocation(boardTypeAllocationRequest);
            result.Trace();
        }
    }
}