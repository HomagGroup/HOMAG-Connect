using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
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
                EdgebandCode = "EB_White_1mm",
                Height = 20,
                Thickness = 1.0,
                DefaultLength = 23.0,
                MaterialCategory = EdgebandMaterialCategory.Veneer,
                Process = EdgebandingProcess.Other,
            };
            var newEdgebandEntity = await materialAssist.CreateEdgebandType(edgebandTypeRequest); 
        }
    }
}
