using HomagConnect.Base.Services;

namespace HomagConnect.IntelliDivide.Client
{
    public partial class IntelliDivideClient : ServiceBase
    {
        public IntelliDivideClient(HttpClient client) : base(client) { }
    }
}