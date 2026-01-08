using Shouldly;

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

            cncPrediction.ShouldNotBeNull();

            cncPrediction!.DurationMax.ShouldBeGreaterThanOrEqualTo(cncPrediction.DurationMin);
            cncPrediction.DurationMax.ShouldBeGreaterThanOrEqualTo(cncPrediction.Duration);
            cncPrediction.Duration.ShouldBeGreaterThanOrEqualTo(cncPrediction.DurationMin);
            cncPrediction.Duration.ShouldBeLessThanOrEqualTo(cncPrediction.DurationMax);

            cncPrediction.PredictionMethod.ShouldNotBe(PredictionMethod.Unknown);
            cncPrediction.PredictionBase.ShouldNotBeNull();
        }
    }
}