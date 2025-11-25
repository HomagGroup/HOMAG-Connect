using HomagConnect.Base.Extensions;
using HomagConnect.ProductionAssist.Contracts;

namespace HomagConnect.ProductionAssist.Samples
{
    /// <summary>
    /// ProductionAssist samples.
    /// </summary>
    public static class ProductionAssistSamples
    {
        /// <summary>
        /// Sample showing how to retrieve the list of all workstations.
        /// </summary>
        public static async Task GetWorkstations(IProductionAssistClient client)
        {
            var response = await client.GetWorkstations();

            response.Trace();
        }
    }
}