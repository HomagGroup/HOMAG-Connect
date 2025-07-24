using HomagConnect.Base.Contracts;
using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialAssist.Contracts.Update;

namespace HomagConnect.MaterialAssist.Samples.Update.Boards
{
    public class UpdateBoardEntitiesSamples
    {
        public static async Task Boards_UpdateBoardEntity(MaterialAssistClientBoards materialAssist)
        {
            var boardEntityUpdate = new MaterialAssistUpdateBoardEntity()
            {
                Id = "42",
                Length = 100,
                Comments = "This is a comment",
                Quantity = 1,
            };
            var updateBoardEntity = await materialAssist.UpdateBoardEntity("42", boardEntityUpdate);
            Console.WriteLine($"Updated board entity: {updateBoardEntity.Id}");
        }
        
        public static async Task Boards_StoreBoardEntity(MaterialAssistClientBoards materialAssist)
        {
            var storageLocation = new StorageLocation()
            {
                Name = "Compartment 02",
            };
            //string id, int length, int width, StorageLocation storageLocation
            await materialAssist.StoreBoardEntity("42", 350, 835, storageLocation);
        }
        
        public static async Task Boards_RemoveAllBoardEntitiesFromWorkplace(MaterialAssistClientBoards materialAssist)
        {
            // string id, bool deleteBoardFromInventory = false
            await materialAssist.RemoveAllBoardEntitiesFromWorkplace("42");
        }

        public static async Task Boards_RemoveSubsetBoardEntitiesFromWorkplace(MaterialAssistClientBoards materialAssist)
        {
            //string id, int quantity, bool deleteBoardFromInventory = false
            await materialAssist.RemoveSubsetBoardEntitiesFromWorkplace("42", 3);
        }

        public static async Task Boards_RemoveSingleBoardEntitiesFromWorkplace(MaterialAssistClientBoards materialAssist)
        {
            //string id, int quantity, bool deleteBoardFromInventory = false
            await materialAssist.RemoveSingleBoardEntitiesFromWorkplace("42", 2);
        }
        
    }
}
