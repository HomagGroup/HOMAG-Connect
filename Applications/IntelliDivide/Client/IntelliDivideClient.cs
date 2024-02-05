using HomagConnect.Base.Services;

namespace HomagConnect.IntelliDivide.Client
{
    /// <summary />
    public partial class IntelliDivideClient : ServiceBase
    {
        /// <summary>
        /// Creates a new instance of <see cref="IntelliDivideClient"/>
        /// </summary>
        public IntelliDivideClient(HttpClient client) : base(client) { }
    }
}