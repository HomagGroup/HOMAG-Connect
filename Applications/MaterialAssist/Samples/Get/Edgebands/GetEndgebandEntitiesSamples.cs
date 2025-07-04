using HomagConnect.Base.Extensions;
using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;

namespace HomagConnect.MaterialAssist.Samples.Get.Edgebands
{
    public class GetEndgebandEntitiesSamples
    {
        public static async Task Edgebands_GetEdgebandEntities(MaterialAssistClientEdgebands materialAssist)
        {
            int take = 100000;
            int skip = 0;

            var allEdgebandEntities = new List<EdgebandEntity>();

            IList<EdgebandEntity> edgebandEntities;

            do
            {
                edgebandEntities = await materialAssist.GetEdgebandEntities(take, skip).ToListAsync();
                allEdgebandEntities.AddRange(edgebandEntities);
                skip += take;
                // different as in the readme.md 

            } while (edgebandEntities.Count == take);

            foreach (var edgebandEntity in allEdgebandEntities)
            {
                Console.WriteLine($"Edgeband entity ID: {edgebandEntity.Id}");
            }
        }

        // GetById
        public static async Task Edgebands_GetEdgebandEntityById(MaterialAssistClientEdgebands materialAssist)
        {
            var edgebandEntity = await materialAssist.GetEdgebandEntityById("id");
            Console.WriteLine(edgebandEntity);
        }

        public static async Task Edgebands_GetEdgebandEntitiesByIds(MaterialAssistClientEdgebands materialAssist)
        {
            List<string> ids = ["id", "id", "id"];
            var edgebandEntities = await materialAssist.GetEdgebandEntitiesByIds(ids);
            foreach (var edgebandEntity in edgebandEntities)
            {
                Console.WriteLine(edgebandEntity);
            }
        }

        // GetByEdgebandCode
        public static async Task Edgebands_GetEdgebandEntitiesByEdgebandCode(MaterialAssistClientEdgebands materialAssist)
        {
            var edgebandEntity = await materialAssist.GetEdgebandEntitiesByEdgebandCode("edgebandCode");
            Console.WriteLine(edgebandEntity);
        }

        public static async Task Edgebands_GetEdgebandEntitiesByEdgebandCodes(MaterialAssistClientEdgebands materialAssist)
        {
            List<string> allEdgebandCodes = ["edgebandCode", "edgebandCode", "edgebandCode"];
            var allEdgebandEntities = await materialAssist.GetEdgebandEntitiesByEdgebandCodes(allEdgebandCodes);
            foreach (var edgebandEntity in allEdgebandEntities)
            {
                Console.WriteLine(edgebandEntity);
            }
        }

        // GetStorageLocations
        public static async Task Edgebands_GetStorageLocations(MaterialAssistClientEdgebands materialAssist)
        {
            var storageLocation1 = await materialAssist.GetStorageLocations();
            var storageLocation2 = await materialAssist.GetStorageLocations("workstationId");

            Console.WriteLine(storageLocation1);
            Console.WriteLine(storageLocation2);
        }

        // GetWorkstations
        public static async Task Edgebands_GetWorkstations(MaterialAssistClientEdgebands materialAssist)
        {
            var workstations = await materialAssist.GetWorkstations();
            Console.WriteLine(workstations);
        }
    }
}
