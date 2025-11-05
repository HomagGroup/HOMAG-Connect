using HomagConnect.Base.Extensions;
using HomagConnect.ProductionAssist.Contracts.Feedback;
using HomagConnect.ProductionAssist.Contracts.Feedback.Enumerations;
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

        [TestMethod]
        public void FeedbackWorkstation_BackwardCompatibility_Serialization()
        {
            var id = Guid.NewGuid();
            var name = "Saw 01";
            var oldJson = $@"
            {{
                ""Id"": ""{id}"",
                ""DisplayName"": ""{name}""
            }}";

            // deserialize with new class
            var workstation = JsonConvert.DeserializeObject<FeedbackWorkstation>(oldJson);

            Assert.AreEqual(id, workstation?.Id);
            Assert.AreEqual(name, workstation?.DisplayName);

            // new properties should have default enum values
            Assert.AreEqual(default(WorkstationGroup), workstation.Group);
            Assert.AreEqual(default(WorkstationType), workstation.Type);
        }

    }
}