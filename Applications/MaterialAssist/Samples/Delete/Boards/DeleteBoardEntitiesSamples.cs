﻿using HomagConnect.MaterialAssist.Client;

namespace HomagConnect.MaterialAssist.Samples.Delete.Boards
{
    public class DeleteBoardEntitiesSamples
    {
        public static async Task Boards_DeleteBoardEntity(MaterialAssistClientBoards materialAssist, string id)
        {
            await materialAssist.DeleteBoardEntity(id);
        }

        public static async Task Boards_DeleteBoardEntities(MaterialAssistClientBoards materialAssist , List<string> boardIds)
        {
            await materialAssist.DeleteBoardEntities(boardIds);
        }
    }
}
