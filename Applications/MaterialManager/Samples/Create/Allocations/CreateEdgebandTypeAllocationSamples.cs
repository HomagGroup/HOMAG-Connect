using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Interfaces;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialManager.Samples.Create.Allocations
{
    /// <summary>
    /// </summary>
    public class CreateEdgebandTypeAllocationSamples
    {
        /// <summary>
        /// The example shows how create an edgeband type allocation.
        /// </summary>
        public static async Task Allocations_CreateEdgebandTypeAllocation(IMaterialManagerClientMaterialEdgebands materialManager)
        {
            var edgebandTypeAllocationRequest = new EdgebandTypeAllocationRequest
            {
                Comments = "Comments",
                CreatedBy = "User1",
                Source = "MaterialManager",
                Workstation = "Workstation1",
                EdgebandCode = "ABS_White_2mm",
                AllocatedLength = 2,
                Customer = "Customer",
                Order = "Order01",
                Project = "Project02",
                UsedLength = 1
            };

            var result = await materialManager.CreateEdgebandTypeAllocation(edgebandTypeAllocationRequest);
            result.Trace();
        }
    }
}