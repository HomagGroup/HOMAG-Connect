using HomagConnect.Base.Tests.Attributes;
using HomagConnect.ProductionAssist.Samples.Feedback;

namespace HomagConnect.ProductionAssist.Tests.Feedback
{
    /// <summary />
    [TestClass]
    [TestCategory("ProductionAssist")]
    [TestCategory("ProductionAssist.Feedback")]
    [TemporaryDisabledOnServer(2024, 9, 1)]
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
                Trace(e);
                exceptionThrown = true;
            }

            Assert.IsFalse(exceptionThrown);
        }

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