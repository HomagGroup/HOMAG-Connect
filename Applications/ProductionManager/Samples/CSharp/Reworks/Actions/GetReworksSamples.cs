using System.Diagnostics;
using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Contracts;

namespace HomagConnect.ProductionManager.Samples.Reworks.Actions
{
    /// <summary>
    /// Sample class which shows how to get reworks.
    /// </summary>
    public static class GetReworksSamples
    {
        /// <summary>
        /// Gets all reworks for a customer.
        /// </summary>
        public static async Task GetCompletedReworksAsync(IProductionManagerClient productionManager)
        {
            var response = await productionManager.GetCompletedReworks().ToListAsync();
            response.Trace();
            var reworkIds = response.Select(x => x.Id).ToList();
            reworkIds.Trace(nameof(reworkIds));
        }
    }
}
