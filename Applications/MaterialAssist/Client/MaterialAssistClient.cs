using HomagConnect.Base.Services;

namespace HomagConnect.MaterialAssist.Client
{
    /// <summary>
    /// Provides access to the MaterialAssist API.
    /// </summary>
    public class MaterialAssistClient : ServiceBase
    {
        #region Constructors

        /// <inheritdoc />
        public MaterialAssistClient(HttpClient client) : base(client)
        {
            Boards = new MaterialAssistClientBoards(client);
            Edgebands = new MaterialAssistClientEdgebands(client);
        }

        /// <inheritdoc />
        public MaterialAssistClient(Guid subscriptionOrPartnerId, string authorizationKey) : base(subscriptionOrPartnerId, authorizationKey)
        {
            Boards = new MaterialAssistClientBoards(subscriptionOrPartnerId, authorizationKey);
            Edgebands = new MaterialAssistClientEdgebands(subscriptionOrPartnerId, authorizationKey);
        }

        /// <inheritdoc />
        public MaterialAssistClient(Guid subscriptionOrPartnerId, string authorizationKey, Uri? baseUri) : base(subscriptionOrPartnerId, authorizationKey, baseUri)
        {
            Boards = new MaterialAssistClientBoards(subscriptionOrPartnerId, authorizationKey, baseUri);
            Edgebands = new MaterialAssistClientEdgebands(subscriptionOrPartnerId, authorizationKey, baseUri);
        }

        #endregion

        public MaterialAssistClientBoards Boards { get; set; }

        public MaterialAssistClientEdgebands Edgebands { get; }
    }
}