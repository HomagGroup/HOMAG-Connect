using HomagConnect.Base.Contracts;
using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialAssist.Contracts.Storage;
using HomagConnect.MaterialAssist.Contracts.Update;

namespace HomagConnect.MaterialAssist.Samples.Update.Edgebands;

public class UpdateEdgebandEntitiesSamples
{
    public static async Task Edgebands_RemoveAllEdgebandEntitiesFromWorkplace(MaterialAssistClientEdgebands materialAssist)
    {
        // string id, bool deleteBoardFromInventory = false
        await materialAssist.RemoveAllEdgebandEntitiesFromWorkplace("43");
    }

    public static async Task Edgebands_RemoveSingleEdgebandEntitiesFromWorkplace(MaterialAssistClientEdgebands materialAssist)
    {
        //string id, int quantity, bool deleteBoardFromInventory = false
        await materialAssist.RemoveSingleEdgebandEntitiesFromWorkplace("44", 1);
    }

    public static async Task Edgebands_RemoveSubsetEdgebandEntitiesFromWorkplace(MaterialAssistClientEdgebands materialAssist)
    {
        //string id, int quantity, bool deleteBoardFromInventory = false
        await materialAssist.RemoveSubsetEdgebandEntitiesFromWorkplace("45", 1);
    }

    public static async Task Edgebands_StoreEdgebandEntity(MaterialAssistClientEdgebands materialAssist)
    {
        var edgebandEntityStore = new MaterialAssistStoreEdgebandEntity()
        {
            Id = "43",
            Length = 100,
            StorageLocation = new StorageLocation()
            {
                Name = "Compartment 02",
            },
        };
        await materialAssist.StoreEdgebandEntity(edgebandEntityStore);
    }

    public static async Task Edgebands_UpdateEdgebandEntity(MaterialAssistClientEdgebands materialAssist, double randomlength)
    {
        var edgebandEntityUpdate = new MaterialAssistUpdateEdgebandEntity()
        {
            Id = "43",
            Length = randomlength,
            Comments = "This is a comment",
            Quantity = 1,
        };
        var updateEdgebandEntity = await materialAssist.UpdateEdgebandEntity("43", edgebandEntityUpdate);
        Console.WriteLine($"Updated edgeband entity: {updateEdgebandEntity.Id}");
    }
}