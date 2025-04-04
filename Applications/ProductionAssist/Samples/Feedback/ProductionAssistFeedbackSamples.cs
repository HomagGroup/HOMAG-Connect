using HomagConnect.Base.Extensions;
using HomagConnect.ProductionAssist.Contracts.Feedback.Interfaces;

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
            var response = await client.GetWorkstations();

            response.Trace();
        }

        /// <summary>
        /// Sample showing how to report a production entity as finished.
        /// </summary>
        public static async Task ReportAsFinished(IProductionAssistFeedbackClient client)
        {
            var workstationId = Guid.NewGuid(); // should be replaced with an existing workstationId
            var identification = "123456"; // should be replaced with an existing id/barcode
            var quantity = 1;
            DateTimeOffset? timespan = null;

            await client.ReportAsFinished(workstationId, identification, quantity, timespan);
        }
    }
}