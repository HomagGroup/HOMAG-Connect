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
                Length = 2070.0,
                // Add other properties
            };
            var updatedBoardType = await materialManager.UpdateBoardType("BT_White_19mm", boardTypeUpdate);
            Console.WriteLine($"Updated Board Type: {updatedBoardType.BoardCode}");
        }
    }
}
