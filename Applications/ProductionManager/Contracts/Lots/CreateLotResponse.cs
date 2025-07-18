using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Lots
{
    /// <summary>
    /// Lot creation operation response
    /// </summary>
    public class CreateLotResponse
    {
        /// <summary>
        /// Gets or sets the correlation id for the import job
        /// </summary>
        [JsonProperty(Order = 1)]
        public Guid LotId { get; set; }
    }
}
