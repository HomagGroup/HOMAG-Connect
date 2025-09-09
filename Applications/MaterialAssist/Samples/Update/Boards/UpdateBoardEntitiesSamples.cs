using HomagConnect.Base.Contracts;
using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialAssist.Contracts.Storage;
using HomagConnect.MaterialAssist.Contracts.Update;

namespace HomagConnect.MaterialAssist.Samples.Update.Boards;

public class UpdateBoardEntitiesSamples
{
    public static async Task Boards_RemoveAllBoardEntitiesFromWorkplace(MaterialAssistClientBoards materialAssist)
    {
        // string id, bool deleteBoardFromInventory = false
        await materialAssist.RemoveAllBoardEntitiesFromWorkplace("40");
    }

    public static async Task Boards_RemoveSingleBoardEntitiesFromWorkplace(MaterialAssistClientBoards materialAssist)
    {
        //string id, int quantity, bool deleteBoardFromInventory = false
        await materialAssist.RemoveSingleBoardEntitiesFromWorkplace("41", 2);
    }

    public static async Task Boards_RemoveSubsetBoardEntitiesFromWorkplace(MaterialAssistClientBoards materialAssist)
    {
        //string id, int quantity,Boards_RemoveSubsetBoardEntitiesFromWorkplace bool deleteBoardFromInventory = false
        await materialAssist.RemoveSubsetBoardEntitiesFromWorkplace("42", 3);
    }

    public static async Task Boards_StoreBoardEntity(MaterialAssistClientBoards materialAssist)
    {
        var boardEntityStore = new MaterialAssistStoreBoardEntity()
        {
            Id = "42",
            Length = 100,
            Width = 70,
            StorageLocation = new StorageLocation()
            {
                Name = "Compartment 02",
            },
        };
        await materialAssist.StoreBoardEntity(boardEntityStore);
    }

    public static async Task Boards_UpdateBoardEntity(MaterialAssistClientBoards materialAssist)
    {
        var boardEntityUpdate = new MaterialAssistUpdateBoardEntity()
        {
            Id = "42",
            Length = 100,
            Width = 70,
            Comments = "This is a comment",
            Quantity = 1,
        };
        var updateBoardEntity = await materialAssist.UpdateBoardEntity("42", boardEntityUpdate);
        Console.WriteLine($"Updated board entity: {updateBoardEntity.Id}");
    }
}