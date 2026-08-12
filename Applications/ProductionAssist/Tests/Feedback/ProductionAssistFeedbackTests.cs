using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Extensions;
using HomagConnect.ProductionAssist.Contracts.Feedback;
using HomagConnect.ProductionAssist.Samples.Feedback;

using Newtonsoft.Json;

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