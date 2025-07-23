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

            } while (edgebandEntities.Count == take);

            foreach (var edgebandEntity in allEdgebandEntities)
            {
                Console.WriteLine($"Edgeband entity ID: {edgebandEntity.Id}");
            }
        }

        // GetById
        public static async Task Edgebands_GetEdgebandEntityById(MaterialAssistClientEdgebands materialAssist)
        {
            var edgebandEntity = await materialAssist.GetEdgebandEntityById("42");
            Console.WriteLine(edgebandEntity);
        }

        public static async Task Edgebands_GetEdgebandEntitiesByIds(MaterialAssistClientEdgebands materialAssist)
        {
            List<string> ids = ["42", "50", "23"];
            var edgebandEntities = await materialAssist.GetEdgebandEntitiesByIds(ids);
            foreach (var edgebandEntity in edgebandEntities)
            {
                Console.WriteLine(edgebandEntity);
            }
        }

        // GetByEdgebandCode
        public static async Task Edgebands_GetEdgebandEntitiesByEdgebandCode(MaterialAssistClientEdgebands materialAssist)
        {
            var edgebandEntity = await materialAssist.GetEdgebandEntitiesByEdgebandCode("RAUKANTEX COLOR 22/1,3");
            Console.WriteLine(edgebandEntity);
        }

        public static async Task Edgebands_GetEdgebandEntitiesByEdgebandCodes(MaterialAssistClientEdgebands materialAssist)
        {
            List<string> allEdgebandCodes = ["RAUKANTEX COLOR 22/1,3", "RAUKANTEX dekor pro 23 x 1,4", "ABS_Schwarz_2_23_HM"];
            var allEdgebandEntities = await materialAssist.GetEdgebandEntitiesByEdgebandCodes(allEdgebandCodes);
            foreach (var edgebandEntity in allEdgebandEntities)
            {
                Console.WriteLine(edgebandEntity);
            }
        }

        // GetStorageLocations
        public static async Task Edgebands_GetStorageLocations(MaterialAssistClientEdgebands materialAssist)
        {
            var allStorageLocations = await materialAssist.GetStorageLocations();
            var storageLocation = await materialAssist.GetStorageLocations("Compartment 02");

            Console.WriteLine(allStorageLocations);
            Console.WriteLine(storageLocation);
        }

        // GetWorkstations
        public static async Task Edgebands_GetWorkstations(MaterialAssistClientEdgebands materialAssist)
        {
            var workstations = await materialAssist.GetWorkstations();
            Console.WriteLine(workstations);
        }
    }
}
