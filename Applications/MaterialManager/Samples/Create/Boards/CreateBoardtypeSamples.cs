using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Interfaces;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialManager.Samples.Create.Boards
{
    public class CreateBoardTypeSamples
    {
        /// <summary>
        /// The example shows how create a boardtype.
        /// </summary>
        public static async Task Boards_CreateBoardType(IMaterialManagerClientMaterialBoards materialManager, string materialCode, string boardCode)
        {
            var boardTypeRequest = new MaterialManagerRequestBoardType
            {
                //The material code is the identifier of the material type
                MaterialCode = materialCode,
                //The board code is the identifier of the board type
                BoardCode = boardCode,
                Length = 4100.0,
                Width = 650.0,
                Thickness = 12.0,
                Type = BoardTypeType.Board,
                MaterialCategory = BoardMaterialCategory.Undefined,
                CoatingCategory = CoatingCategory.Undefined,
                Grain = Grain.None,
            };

            var result = await materialManager.CreateBoardType(boardTypeRequest);
            result.Trace();
        }
        
        /// <summary>
        /// The example shows how create a boardtype.
        /// </summary>
        public static async Task Boards_CreateBoardType_AdditionalData(IMaterialManagerClientMaterialBoards materialManager, string materialCode, string boardCode)
        {
            var boardTypeRequest = new MaterialManagerRequestBoardType
            {
                //The material code is the identifier of the material type
                MaterialCode = materialCode,
                //The board code is the identifier of the board type
                BoardCode = boardCode,
                Length = 4100.0,
                Width = 650.0,
                Thickness = 12.0,
                Type = BoardTypeType.Board,
                MaterialCategory = BoardMaterialCategory.Undefined,
                CoatingCategory = CoatingCategory.Undefined,
                Grain = Grain.None,
            };

            var testFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Red.png");
            var fileReferences = new FileReference[]
            {
                new FileReference("BoardPicture", testFilePath)
            };

            var result = await materialManager.CreateBoardType(boardTypeRequest, fileReferences);
            result.Trace();
        }
    }
}
