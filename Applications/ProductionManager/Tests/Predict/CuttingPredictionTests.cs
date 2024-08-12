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
    [TemporaryDisabledOnServer(2024, 9, 1)]
    public class CuttingPredictionTests : ProductionManagerTestBase
    {
        /// <summary />
        [TestMethod]
        [TemporaryDisabledOnServer(2024,8,15)]
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

            cuttingPrediction.Should().NotBeNull();

            cuttingPrediction.DurationMax.Should().BeGreaterOrEqualTo(cuttingPrediction.DurationMin);
            cuttingPrediction.DurationMax.Should().BeGreaterOrEqualTo(cuttingPrediction.Duration);
            cuttingPrediction.Duration.Should().BeGreaterOrEqualTo(cuttingPrediction.DurationMin);
            cuttingPrediction.Duration.Should().BeLessThanOrEqualTo(cuttingPrediction.DurationMax);

            cuttingPrediction.PredictionMethod.Should().NotBe(PredictionMethod.Unknown);
            cuttingPrediction.PredictionBase.Should().NotBeNull();
        }
    }
}