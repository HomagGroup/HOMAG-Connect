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
    public class EdgebandingPredictionTests : ProductionManagerTestBase
    {
        /// <summary />
        [TestMethod]
        [TemporaryDisabledOnServer(2024,8,15)]
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

            edgebandingPrediction.Should().NotBeNull();

            edgebandingPrediction.DurationMax.Should().BeGreaterOrEqualTo(edgebandingPrediction.DurationMin);
            edgebandingPrediction.DurationMax.Should().BeGreaterOrEqualTo(edgebandingPrediction.Duration);
            edgebandingPrediction.Duration.Should().BeGreaterOrEqualTo(edgebandingPrediction.DurationMin);
            edgebandingPrediction.Duration.Should().BeLessThanOrEqualTo(edgebandingPrediction.DurationMax);

            edgebandingPrediction.LengthByEdgebandCode.Should().NotBeNull();

            edgebandingPrediction.PredictionMethod.Should().NotBe(PredictionMethod.Unknown);
            edgebandingPrediction.PredictionBase.Should().NotBeNull();
        }
    }
}