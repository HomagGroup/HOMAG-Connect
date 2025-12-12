using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Interfaces;
using HomagConnect.MaterialManager.Contracts.Update;

namespace HomagConnect.MaterialManager.Samples.Update.Allocations
{
    /// <summary>
    /// </summary>
    public class UpdateEdgebandTypeAllocationSamples
    {
        /// <summary>
        /// The example shows how to update an edgeband type allocation.
        /// </summary>
        public static async Task Allocations_CreateEdgebandTypeAllocation(IMaterialManagerClientMaterialEdgebands materialManager)
        {
            var edgebandTypeAllocationUpdate = new EdgebandTypeAllocationUpdate
            {
                AllocatedLength = 5,
                Customer = "customer",
                EdgebandCode = "ABS_White_2mm",
                Order = "order01",
                Project = "project02",
            };

            var result = await materialManager.UpdateEdgebandTypeAllocation(edgebandTypeAllocationUpdate);
            result.Trace();
        }
    }
}