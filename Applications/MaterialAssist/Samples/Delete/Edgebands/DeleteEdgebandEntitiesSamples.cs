using HomagConnect.MaterialAssist.Client;

namespace HomagConnect.MaterialAssist.Samples.Delete.Edgebands
{
    public class DeleteEdgebandEntitiesSamples
    {
        public static async Task Edgebands_CreateEdgebandEntity(MaterialAssistClientEdgebands materialAssist)
        {
            await materialAssist.DeleteEdgebandEntity("42");
        }
    }
}
