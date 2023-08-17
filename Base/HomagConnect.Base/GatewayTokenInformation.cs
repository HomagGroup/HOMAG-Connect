namespace HomagConnect.Base
{
    /// <summary>
    /// The information for access token
    /// </summary>
    public class GatewayTokenInformation
    {
        /// <summary>
        /// Subscription id
        /// </summary>
        public string SubscriptionId { get; set; }

        /// <summary>
        /// Subscription name
        /// </summary>
        public string SubscriptionName { get; set; }

        /// <summary>
        /// The application id in tapio
        /// </summary>
        public string ApplicationId { get; set; }

        /// <summary>
        /// The application name
        /// </summary>
        public string ApplicationName { get; set; }
    }
}