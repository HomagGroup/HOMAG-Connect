using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Interfaces;

namespace HomagConnect.MaterialManager.Samples.Read.Allocations
{
    /// <summary>
    /// </summary>
    public class MaterialManagerReadBoardTypeAllocationsResults
    {
        /// <summary>
        /// The example shows how to read all board type allocations.
        /// </summary>
        public static async Task Allocations_ReadBoardTypeAllocations(IMaterialManagerClientMaterialBoards materialManager)
        {
            var result = await materialManager.GetBoardTypeAllocations(10, 10);
            result.Trace();
        }

        /// <summary>
        /// </summary>
        public static async Task Allocations_ReadBoardTypeAllocationsByAllocationNames(IMaterialManagerClientMaterialBoards materialManager)
        {
            var allocationNames = new List<string> { "HPL_F274_9_12.0_4100.0_650.0_Allocation" };
            var result = await materialManager.GetBoardTypeAllocationsByAllocationNames(allocationNames, 10, 10);
            result.Trace();
        }

        /// <summary>
        /// </summary>
        public static async Task Allocations_SearchBoardTypeAllocations(IMaterialManagerClientMaterialBoards materialManager)
        {
            var result = await materialManager.SearchBoardTypeAllocations("HPL_F274_9_12.0_4100.0_650.0", 10, 10);
            result.Trace();
        }
    }
}