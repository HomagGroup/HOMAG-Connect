using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Contracts;
using HomagConnect.ProductionManager.Contracts.Lots;

namespace HomagConnect.ProductionManager.Samples.Lots.Actions
{
    /// <summary>
    /// Sample class which shows how to create a lot.
    /// </summary>
    public static class CreateLotSamples
    {
        /// <summary>
        /// Create lot sample
        /// </summary>
        /// <param name="productionManager"></param>
        public static async Task CreateLotSample(IProductionManagerClient productionManager)
        {
            var request = new CreateLotRequest
            {
                StartDatePlanned = DateTime.Now,
                CompletionDatePlanned = DateTime.Now.AddDays(10),
                Name = "Test Lot",
                PartIds = new List<string>()
                {
                    // Add existing part ids from an order
                    // This can be replaced with actual part ids you want to add in the lot
                    "1043898",
                    "1043899",
                }
            };

            var response = await productionManager.CreateLotRequest(request);

            response.Trace();
        }
    }
}
