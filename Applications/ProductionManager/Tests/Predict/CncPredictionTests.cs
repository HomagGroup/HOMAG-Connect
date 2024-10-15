using FluentAssertions;

using HomagConnect.Base.Tests.Attributes;
using HomagConnect.ProductionManager.Contracts.Predict;

using HomagConnect.ProductionManager.Samples.Orders.Predict;

namespace HomagConnect.ProductionManager.Tests.Predict
{
    /// <summary />
    [TestClass]
    [TestCategory("ProductionManager")]
    [TestCategory("ProductionManager.Predict")]
    [Ignore("OKR skipped")]
    public class CncPredictionTests : ProductionManagerTestBase
    {
        /// <summary />
        [TestMethod]
        public async Task Predict_Cnc_NoException()
        {
            var productionManager = GetProductionManagerClient();
            var anyException = false;

            CncPrediction? cncPrediction = null;
            try
            {
                cncPrediction = await PredictParts.PredictPartsCncAsync(productionManager);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                anyException = true;
            }

            Assert.IsFalse(anyException);
            Assert.IsNotNull(cncPrediction);

            cncPrediction.Should().NotBeNull();

            cncPrediction.DurationMax.Should().BeGreaterOrEqualTo(cncPrediction.DurationMin);
            cncPrediction.DurationMax.Should().BeGreaterOrEqualTo(cncPrediction.Duration);
            cncPrediction.Duration.Should().BeGreaterOrEqualTo(cncPrediction.DurationMin);
            cncPrediction.Duration.Should().BeLessThanOrEqualTo(cncPrediction.DurationMax);

            cncPrediction.PredictionMethod.Should().NotBe(PredictionMethod.Unknown);
            cncPrediction.PredictionBase.Should().NotBeNull();
        }
    }
}