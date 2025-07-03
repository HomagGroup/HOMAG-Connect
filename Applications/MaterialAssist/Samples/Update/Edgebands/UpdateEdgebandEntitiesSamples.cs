using HomagConnect.MaterialAssist.Client;
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
    }
}
