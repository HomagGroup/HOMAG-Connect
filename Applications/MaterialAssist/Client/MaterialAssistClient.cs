using HomagConnect.Base.Services;

namespace HomagConnect.MaterialAssist.Client
{
    public class MaterialAssistClient : ServiceBase
    {
        public MaterialAssistClient(HttpClient client) : base(client)
        {
            Boards = new MaterialAssistClientBoards(client);
            Edgebands = new MaterialAssistClientEdgebands(client);
        }

        public MaterialAssistClientBoards Boards { get; set; }

        public MaterialAssistClientEdgebands Edgebands { get; }
    }
}