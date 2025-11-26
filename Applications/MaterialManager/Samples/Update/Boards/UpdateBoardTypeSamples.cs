using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Interfaces;
using HomagConnect.MaterialManager.Contracts.Update;

// ReSharper disable LocalizableElement

namespace HomagConnect.MaterialManager.Samples.Update.Boards
{
    /// <summary>
    /// Update board type samples.
    /// </summary>
    public class UpdateBoardTypeSamples
    {
        /// <summary>
        /// The example shows how update a board type.
        /// </summary>
        public static async Task Boards_UpdateBoardType(IMaterialManagerClientMaterialBoards materialManager, string boardCode, double value)
        {
            var boardTypeUpdate = new MaterialManagerUpdateBoardType
            {
                Costs = value,
                // Add other properties
            };
            var updatedBoardType = await materialManager.UpdateBoardType(boardCode, boardTypeUpdate);
            Console.WriteLine($"Updated Board Type: {updatedBoardType.BoardCode}");
        }

        /// <summary>
        /// The example shows how create a board type.
        /// </summary>
        public static async Task Boards_UpdateBoardType_AdditionalData(
            IMaterialManagerClientMaterialBoards materialManager,
            string materialCode,
            string boardCode)
        {
            var imageFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Red.png");
            var additionalDataImage = new FileReference("Red.png", imageFilePath);

            var boardTypeUpdate = new MaterialManagerUpdateBoardType
            {
                MaterialCode = materialCode,
                BoardCode = boardCode,
                Length = 4100.0,
                Width = 650.0,
                Thickness = 12.0,
                Type = BoardTypeType.Board,
                MaterialCategory = BoardMaterialCategory.Undefined,
                CoatingCategory = CoatingCategory.Undefined,
                Grain = Grain.None,
                AdditionalData = new List<AdditionalDataEntity>
                {
                    new AdditionalDataImage
                    {
                        Category = "Decor",
                        DownloadFileName = additionalDataImage.Reference,
                        DownloadUri = new Uri(additionalDataImage.Reference, UriKind.Relative)
                    }
                }
            };

            var updateBoardType = await materialManager.UpdateBoardType(boardCode, boardTypeUpdate, [additionalDataImage]);
            updateBoardType.Trace();
        }
    }
}