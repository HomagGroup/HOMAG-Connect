using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Lots
{
    /// <summary>
    /// Lot creation operation response
    /// </summary>
    public class CreateLotResponse
    {
        /// <summary>
        /// Gets or sets the newly created lot id
        /// </summary>
        [JsonProperty(Order = 1)]
        public Guid LotId { get; set; }
    }
}
