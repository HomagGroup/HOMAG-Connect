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

            } while (edgebandEntities.Count == take);

            foreach (var edgebandEntity in allEdgebandEntities)
            {
                Console.WriteLine($"Edgeband entity ID: {edgebandEntity.Id}");
            }
        }
    }
}
