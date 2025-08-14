using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialAssist.Contracts.Request;

namespace HomagConnect.MaterialAssist.Samples.Create.Templates
{
    public class CreateTemplateEntitiesSamples
    {
        public static async Task Boards_CreateTemplateEntity(MaterialAssistClientBoards materialAssist)
        {
            var templateEntityRequest = new MaterialAssistRequestTemplateEntity
            {
                Id = "42",
                //The board code is the identifier of the board type
                BoardCode = "MDF_H3171_12_11.6_2800.0_1310.0",
                Comments = "This is a comment",
                Length = 1000,
                Width = 500,
                Quantity = 5,
            };
            var newTemplateEntity = await materialAssist.CreateTemplateEntity(templateEntityRequest);
            Console.WriteLine($@"Created template entity: {newTemplateEntity.Id}");
        }
    }
}