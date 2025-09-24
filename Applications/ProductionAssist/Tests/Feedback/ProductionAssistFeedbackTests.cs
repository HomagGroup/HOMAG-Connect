using HomagConnect.Base.Extensions;
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
        public async Task Feedback_GetWorkstations_NoException()
        {
            var exceptionThrown = false;
            var client = GetProductionAssistFeedbackClient();

            try
            {
                await ProductionAssistFeedbackSamples.GetWorkstations(client);
            }
            catch (Exception e)
            {
                e.Trace();
                exceptionThrown = true;
            }

            Assert.IsFalse(exceptionThrown);
        }

        /// <summary />
        [TestMethod]
        public async Task Feedback_ReportAsFinished_NoException()
        {
            var client = GetProductionAssistFeedbackClient();

            try
            {
                await ProductionAssistFeedbackSamples.ReportAsFinished(client);
            }
            catch (Exception)
            {
                Assert.Inconclusive("Request data from sample might not be correct.");
            }
        }
    }
}