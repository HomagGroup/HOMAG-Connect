using HomagConnect.ProductionAssist.Samples.Feedback;

namespace HomagConnect.ProductionAssist.Tests.Feedback
{
    /// <summary />
    [TestClass]
    [TestCategory("ProductionAssist")]
    [TestCategory("ProductionAssist.Feedback")]
    public class ProductionAssistFeedbackTests : ProductionAssistTestBase
    {
        /// <summary />
        [TestMethod]
        public async Task Feedback_ReportAsFinished_NoException()
        {
            var exceptionThrown = false;
            var client = GetProductionAssistFeedbackClient();

            try
            {
                await ProductionAssistFeedbackSamples.ReportAsFinished(client);
            }
            catch (Exception e)
            {
                Trace(e);
                exceptionThrown = true;
            }

            Assert.IsFalse(exceptionThrown);
        }
    }
}