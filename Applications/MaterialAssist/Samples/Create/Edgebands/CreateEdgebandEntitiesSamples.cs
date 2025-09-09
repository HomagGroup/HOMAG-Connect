using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialAssist.Samples.Create.Edgebands
{
    public class CreateEdgebandEntitiesSamples
    {
        public static async Task Edgebands_CreateEdgebandEntity(MaterialAssistClientEdgebands materialAssist, string id)
        {
            var edgebandEntityRequest = new MaterialAssistRequestEdgebandEntity()
            {
                Id = id,
                EdgebandCode = "ABS_White_1mm",
                ManagementType = ManagementType.Single,
                Comments = "This is a comment",
                Quantity = 1,
                Length = 1000,
                CurrentThickness = 1.0
            };
            var newEdgebandEntity = await materialAssist.CreateEdgebandEntity(edgebandEntityRequest);
        }

        public static async Task Edgebands_CreateEdgebandType(MaterialAssistClientEdgebands materialAssist, string edgebandCode)
        {
            var edgebandTypeRequest = new MaterialManagerRequestEdgebandType() 
            {
                EdgebandCode = edgebandCode,
                Height = 20,
                Thickness = 1.0,
                DefaultLength = 50.0,
                MaterialCategory = EdgebandMaterialCategory.Veneer,
                Process = EdgebandingProcess.Other,
            };
            var newEdgebandEntity = await materialAssist.CreateEdgebandType(edgebandTypeRequest); 
        }
    }
}
