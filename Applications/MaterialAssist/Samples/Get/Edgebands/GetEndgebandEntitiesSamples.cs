using HomagConnect.Base.Extensions;
using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;

namespace HomagConnect.MaterialAssist.Samples.Get.Edgebands
{
    public class GetEndgebandEntitiesSamples
    {
        public static async Task<IList<EdgebandEntity>> Edgebands_GetEdgebandEntities(MaterialAssistClientEdgebands materialAssist)
        {
            int take = 100000;
            int skip = 0;

            var allEdgebandEntities = new List<EdgebandEntity>();

            IList<EdgebandEntity> edgebandEntities;

            do
            {
                edgebandEntities = await materialAssist.GetEdgebandEntities(take, skip).ToListAsync();

            } while (edgebandEntities.Count == take);

            return edgebandEntities;
        }

        // GetById
        public static async Task<EdgebandEntity?> Edgebands_GetEdgebandEntityById(MaterialAssistClientEdgebands materialAssist)
        {
            var edgebandEntity = await materialAssist.GetEdgebandEntityById("33");
            return edgebandEntity;
        }

        public static async Task<IEnumerable<EdgebandEntity?>> Edgebands_GetEdgebandEntitiesByIds(MaterialAssistClientEdgebands materialAssist)
        {
            List<string> ids = ["33", "34", "35"];
            var edgebandEntities = await materialAssist.GetEdgebandEntitiesByIds(ids);
            return edgebandEntities;
        }

        // GetByEdgebandCode
        public static async Task<IEnumerable<EdgebandEntity>?> Edgebands_GetEdgebandEntitiesByEdgebandCode(MaterialAssistClientEdgebands materialAssist)
        {
            var edgebandEntity = await materialAssist.GetEdgebandEntitiesByEdgebandCode("ABS_White_1mm");
            return edgebandEntity;
        }

        public static async Task<IEnumerable<EdgebandEntity>> Edgebands_GetEdgebandEntitiesByEdgebandCodes(MaterialAssistClientEdgebands materialAssist)
        {
            List<string> allEdgebandCodes = ["ABS_White_1mm"];
            var allEdgebandEntities = await materialAssist.GetEdgebandEntitiesByEdgebandCodes(allEdgebandCodes);
            return allEdgebandEntities;
        }

        // GetStorageLocations
        public static async Task<IEnumerable<Base.Contracts.StorageLocation>> Edgebands_GetStorageLocations(MaterialAssistClientEdgebands materialAssist)
        {
            var allStorageLocations = await materialAssist.GetStorageLocations();
            var storageLocation = await materialAssist.GetStorageLocations("Compartment 02");
            return storageLocation;
        }

        // GetWorkstations
        public static async Task<IEnumerable<Base.Contracts.Workstation>> Edgebands_GetWorkstations(MaterialAssistClientEdgebands materialAssist)
        {
            var workstations = await materialAssist.GetWorkstations();
            return workstations;
        }
    }
}
