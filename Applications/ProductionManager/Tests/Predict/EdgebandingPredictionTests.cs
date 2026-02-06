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
    public class EdgebandingPredictionTests : ProductionManagerTestBase
    {
        /// <summary />
        [TestMethod]
        public async Task Predict_Edgebanding_NoException()
        {
            var productionManager = GetProductionManagerClient();
            var anyException = false;

            EdgebandingPrediction? edgebandingPrediction = null;
            try
            {
                edgebandingPrediction = await PredictParts.PredictPartsEdgebandingAsync(productionManager);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                anyException = true;
            }

            Assert.IsFalse(anyException);
            Assert.IsNotNull(edgebandingPrediction);

            edgebandingPrediction.ShouldNotBeNull();

            edgebandingPrediction!.DurationMax.ShouldBeGreaterThanOrEqualTo(edgebandingPrediction.DurationMin);
            edgebandingPrediction.DurationMax.ShouldBeGreaterThanOrEqualTo(edgebandingPrediction.Duration);
            edgebandingPrediction.Duration.ShouldBeGreaterThanOrEqualTo(edgebandingPrediction.DurationMin);
            edgebandingPrediction.Duration.ShouldBeLessThanOrEqualTo(edgebandingPrediction.DurationMax);

            edgebandingPrediction.LengthByEdgebandCode.ShouldNotBeNull();

            edgebandingPrediction.PredictionMethod.ShouldNotBe(PredictionMethod.Unknown);
            edgebandingPrediction.PredictionBase.ShouldNotBeNull();
        }
    }
}