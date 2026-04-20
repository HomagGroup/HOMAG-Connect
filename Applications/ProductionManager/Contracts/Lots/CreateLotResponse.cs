using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Lots
{
    /// <summary>
    /// Represents the response returned after a lot has been created.
    /// </summary>
    /// <example>
    /// { "lotId": "7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c" }
    /// </example>
    public class CreateLotResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier of the newly created lot.
        /// </summary>
        /// <example>7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c</example>
        [JsonProperty(Order = 1)]
        public Guid LotId { get; set; }
    }
}
