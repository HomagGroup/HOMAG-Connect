using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialAssist.Contracts.Storage;
using HomagConnect.MaterialAssist.Contracts.Update;

namespace HomagConnect.MaterialAssist.Samples.Update.Edgebands
{
    public class UpdateEdgebandEntitiesSamples
    {
        public static async Task Edgebands_UpdateEdgebandEntities(MaterialAssistClientEdgebands materialAssist)
        {
            var edgebandEntityUpdate = new MaterialAssistUpdateEdgebandEntity()
            {
                Id = "42",
                Length = 100,
                Comments = "This is a comment",
                Quantity = 1,
            };
            var updateEdgebandEntity = await materialAssist.UpdateEdgebandEntity("42", edgebandEntityUpdate);
            Console.WriteLine($"Updated edgeband entity: {updateEdgebandEntity.Id}");
        }

        public static async Task Edgebands_StoreEdgebandEntities(MaterialAssistClientEdgebands materialAssist)
        {
            var edgebandEntityStore = new MaterialAssistStoreEdgebandEntity()
            {
                Id = "42",
                Length = 100,
                // other values
            };
            await materialAssist.StoreEdgebandEntity(edgebandEntityStore);
        }

        public static async Task Edgebands_RemoveAllEdgebandEntitiesFromWorkplace(MaterialAssistClientEdgebands materialAssist)
        {
            // string id, bool deleteBoardFromInventory = false
            await materialAssist.RemoveAllEdgebandEntitiesFromWorkplace("id");
        }

        public static async Task Edgebands_RemoveSubsetEdgebandEntitiesFromWorkplace(MaterialAssistClientEdgebands materialAssist)
        {
            //string id, int quantity, bool deleteBoardFromInventory = false
            await materialAssist.RemoveSubsetEdgebandEntitiesFromWorkplace("id", 10);
        }

        public static async Task Edgebands_RemoveSingleEdgebandEntitiesFromWorkplace(MaterialAssistClientEdgebands materialAssist)
        {
            //string id, int quantity, bool deleteBoardFromInventory = false
            await materialAssist.RemoveSingleEdgebandEntitiesFromWorkplace("id", 10);
        }
    }
}
