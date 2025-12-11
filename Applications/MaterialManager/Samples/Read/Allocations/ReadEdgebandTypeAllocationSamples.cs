using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Interfaces;

namespace HomagConnect.MaterialManager.Samples.Read.Allocations
{
    /// <summary>
    /// </summary>
    public class ReadEdgebandTypeAllocationSamples
    {
        /// <summary>
        /// The example shows how to read all edgeband type allocations.
        /// </summary>
        public static async Task Allocations_ReadEdgebandTypeAllocations(IMaterialManagerClientMaterialEdgebands materialManager)
        {
            var result = await materialManager.GetEdgebandTypeAllocations();
            result.Trace();
        }

        /// <summary>
        /// </summary>
        public static async Task Allocations_ReadEdgebandTypeAllocationsByAllocationNames(IMaterialManagerClientMaterialEdgebands materialManager)
        {
            var result = await materialManager.GetEdgebandTypeAllocation(
                order: "Order", customer: "Customer", project: "Project", edgebandCode: "ABS_White_2mm"
            );
            result.Trace();
        }
    }
}