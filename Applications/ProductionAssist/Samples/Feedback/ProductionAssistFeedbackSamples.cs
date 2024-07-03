using HomagConnect.Base.Extensions;
using HomagConnect.ProductionAssist.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

            Assert.IsTrue(response.Any());

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