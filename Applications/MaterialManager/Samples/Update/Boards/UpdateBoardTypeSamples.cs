using HomagConnect.MaterialManager.Contracts.Material.Boards.Interfaces;
using HomagConnect.MaterialManager.Contracts.Update;

namespace HomagConnect.MaterialManager.Samples.Update.Boards
{
    public class UpdateBoardTypeSamples
    {
        /// <summary>
        /// The example shows how update a boardtype.
        /// </summary>
        public static async Task Boards_UpdateBoardType(IMaterialManagerClientMaterialBoards materialManager)
        {
            var boardTypeUpdate = new MaterialManagerUpdateBoardType
            {
                Length = 500.0,
                // Add other properties
            };
            var updatedBoardType = await materialManager.UpdateBoardType("HPL_F274_9_12.0_4100.0_650.0", boardTypeUpdate);
            Console.WriteLine($"Updated Board Type: {updatedBoardType.BoardCode}");
        }
    }
}
