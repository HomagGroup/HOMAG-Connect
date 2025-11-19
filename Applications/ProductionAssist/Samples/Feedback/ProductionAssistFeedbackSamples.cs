using HomagConnect.ProductionAssist.Contracts.Feedback.Interfaces;

namespace HomagConnect.ProductionAssist.Samples.Feedback
{
    /// <summary>
    /// ProductionAssist feedback samples.
    /// </summary>
    public static class ProductionAssistFeedbackSamples
    {
        /// <summary>
        /// Sample showing how to report a production entity as finished.
        /// </summary>
        public static async Task ReportAsFinished(IProductionAssistFeedbackClient client)
        {
            var workstationId = Guid.Empty; // should be replaced with an existing workstationId
            var identification = ""; // should be replaced with an existing id/barcode
            var quantity = 1;
            DateTimeOffset? timespan = null;
            var source = "HomagConnect User"; // should be replaced with an existing userName/tapio userName/partner name

            await client.ReportAsFinished(workstationId, identification, quantity, timespan, source);
        }
    }
}