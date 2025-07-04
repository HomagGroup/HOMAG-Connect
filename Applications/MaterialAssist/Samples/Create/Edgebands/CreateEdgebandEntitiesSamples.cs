using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialAssist.Samples.Create.Edgebands
{
    public class CreateEdgebandEntitiesSamples
    {
        public static async Task Edgebands_CreateEdgebandEntity(MaterialAssistClientEdgebands materialAssist)
        {
            var edgebandEntityRequest = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "42",
                EdgebandCode = "White Edgeband 19mm",
                ManagementType = ManagementType.Single,
                Comments = "This is a comment",
                Quantity = 1,
                Length = 1000,
                CurrentThickness = 19
            };
            var newEdgebandEntity = await materialAssist.CreateEdgebandEntity(edgebandEntityRequest);
            Console.WriteLine($"Created edgeband entity: {newEdgebandEntity.Id}");
        }

        public static async Task Edgebands_CreateEdgebandType(MaterialAssistClientEdgebands materialAssist)
        {
            var edgebandTypeRequest = new MaterialManagerRequestEdgebandType() 
            {
                EdgebandCode = "White Edgeband 19mm",
                Comments = "This is a comment",
                // other values
            };
            var newEdgebandEntity = await materialAssist.CreateEdgebandType(edgebandTypeRequest); 
        }
    }
}
