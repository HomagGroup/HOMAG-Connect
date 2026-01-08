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
    public class CuttingPredictionTests : ProductionManagerTestBase
    {
        /// <summary />
        [TestMethod]
        public async Task Predict_Cutting_NoException()
        {
            var productionManager = GetProductionManagerClient();
            var anyException = false;

            CuttingPrediction? cuttingPrediction = null;
            try
            {
                cuttingPrediction = await PredictParts.PredictPartsCuttingAsync(productionManager);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                anyException = true;
            }

            Assert.IsFalse(anyException);
            Assert.IsNotNull(cuttingPrediction);

            cuttingPrediction.ShouldNotBeNull();

            cuttingPrediction!.DurationMax.ShouldBeGreaterThanOrEqualTo(cuttingPrediction.DurationMin);
            cuttingPrediction.DurationMax.ShouldBeGreaterThanOrEqualTo(cuttingPrediction.Duration);
            cuttingPrediction.Duration.ShouldBeGreaterThanOrEqualTo(cuttingPrediction.DurationMin);
            cuttingPrediction.Duration.ShouldBeLessThanOrEqualTo(cuttingPrediction.DurationMax);

            cuttingPrediction.PredictionMethod.ShouldNotBe(PredictionMethod.Unknown);
            cuttingPrediction.PredictionBase.ShouldNotBeNull();
        }
    }
}