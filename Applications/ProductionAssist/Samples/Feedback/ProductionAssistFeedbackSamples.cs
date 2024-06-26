using HomagConnect.Base.Extensions;
using HomagConnect.ProductionAssist.Contracts;

namespace HomagConnect.ProductionAssist.Samples.Feedback
{
    /// <summary>
    /// ProductionAssist feedback samples.
    /// </summary>
    public static class ProductionAssistFeedbackSamples
    {
        /// <summary>
        /// Sample showing how to retrieve the list of configured feedback workstations.
        /// </summary>
        public static async Task GetWorkstations(IProductionAssistFeedbackClient client)
        {
            var response = await client.GetWorkstationsAsync();

            response.Trace();
        }

        /// <summary>
        /// Sample showing how to report a production entity as finished.
        /// </summary>
        public static async Task ReportAsFinished(IProductionAssistFeedbackClient client)
        {
            await client.ReportAsFinishedAsync(Guid.NewGuid(), Guid.NewGuid().ToString(), 1);
        }
    }
}