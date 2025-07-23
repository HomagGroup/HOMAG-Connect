using HomagConnect.MaterialAssist.Client;

namespace HomagConnect.MaterialAssist.Samples.Delete.Edgebands
{
    public class DeleteEdgebandEntitiesSamples
    {
        public static async Task Edgebands_DeleteEdgebandEntity(MaterialAssistClientEdgebands materialAssist, string id)
        {
            await materialAssist.DeleteEdgebandEntity(id);
        }

        public static async Task Edgebands_DeleteEdgebandEntities(MaterialAssistClientEdgebands materialAssist, List<string> ids)
        {
            await materialAssist.DeleteEdgebandEntity(ids);
        }
    }
}
