﻿using HomagConnect.Base.Contracts.Enumerations;
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
        public static async Task Boards_CreateBoardType(IMaterialManagerClientMaterialBoards materialManager)
        {
            var boardTypeRequest = new MaterialManagerRequestBoardType
            {
                //The material code is the identifier of the material type
                MaterialCode = "HPL_F274_9_12.0",
                //The board code is the identifier of the board type
                BoardCode = "HPL_F274_9_12.0_4100.0_650.0",
                Length = 2070.0,
                Width = 2800.0,
                Thickness = 19.0,
                Type = BoardTypeType.Board,
                MaterialCategory = BoardMaterialCategory.Undefined,
                CoatingCategory = CoatingCategory.Undefined,
                Grain = Grain.None,
            };

            var result = await materialManager.CreateBoardType(boardTypeRequest);
            result.Trace();
        }
    }
}
