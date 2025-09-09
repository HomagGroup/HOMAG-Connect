using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Interfaces;
using HomagConnect.MaterialManager.Contracts.Update;

namespace HomagConnect.MaterialManager.Samples.Update.Boards
{
    public class UpdateBoardTypeSamples
    {
        /// <summary>
        /// The example shows how update a boardtype.
        /// </summary>
        public static async Task Boards_UpdateBoardType(IMaterialManagerClientMaterialBoards materialManager, string boardCode)
        {
            var boardTypeUpdate = new MaterialManagerUpdateBoardType
            {
                CoatingCategory = CoatingCategory.Veneer,
                // Add other properties
            };
            var updatedBoardType = await materialManager.UpdateBoardType(boardCode, boardTypeUpdate);
            Console.WriteLine($"Updated Board Type: {updatedBoardType.BoardCode}");
        }
    }
}
