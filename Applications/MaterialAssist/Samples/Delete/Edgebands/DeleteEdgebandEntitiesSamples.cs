using HomagConnect.MaterialAssist.Client;

namespace HomagConnect.MaterialAssist.Samples.Delete.Edgebands
{
    public class DeleteEdgebandEntitiesSamples
    {
        // Funktion für eine Id und mehrere Ids heißt gleich ???
        public static async Task Edgebands_DeleteEdgebandEntity(MaterialAssistClientEdgebands materialAssist)
        {
            await materialAssist.DeleteEdgebandEntity("id");
        }

        public static async Task Edgebands_DeleteEdgebandEntities(MaterialAssistClientEdgebands materialAssist)
        {
            List<string> ids = ["id", "id", "id"];
            await materialAssist.DeleteEdgebandEntity(ids);
        }
    }
}
