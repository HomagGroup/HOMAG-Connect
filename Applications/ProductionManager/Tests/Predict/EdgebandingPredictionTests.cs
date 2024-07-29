using FluentAssertions;

using HomagConnect.Base.Extensions;
using HomagConnect.Base.Tests.Attributes;
using HomagConnect.ProductionManager.Contracts.Predict;
using System;

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
        public async Task Predict_Edgebanding_NoException()
        {
            var productionManager = GetProductionManagerClient();
            var anyException = false;

            EdgebandingPrediction edgebandingPrediction = null;
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