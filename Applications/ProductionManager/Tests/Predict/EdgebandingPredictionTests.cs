using FluentAssertions;

using HomagConnect.Base.Extensions;
using HomagConnect.Base.Tests.Attributes;
using HomagConnect.ProductionManager.Contracts;
using HomagConnect.ProductionManager.Contracts.Predict;

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
            var materialManager = GetMaterialManagerClient();
            var edgebandCodes = await materialManager.Material.Edgebands.GetEdgebandTypes(2, 0).ToListAsync();

            if (edgebandCodes == null || edgebandCodes.Count < 2)
            {
                Assert.Inconclusive("Not enough edgeband types found.");
            }

            var edgebandingPredictionRequest = new EdgebandingPredictionRequest([
                new EdgebandingPredictionPart
                {
                    Quantity = 10,
                    Length = 700,
                    Width = 300,
                    EdgeFront = edgebandCodes[0].EdgebandCode,
                    EdgeBack = edgebandCodes[0].EdgebandCode,
                    EdgeLeft = edgebandCodes[0].EdgebandCode,
                    EdgeRight = edgebandCodes[0].EdgebandCode
                },
                new EdgebandingPredictionPart
                {
                    Quantity = 5,
                    Length = 800,
                    Width = 200,
                    EdgeFront = edgebandCodes[1].EdgebandCode,
                    EdgeBack = edgebandCodes[1].EdgebandCode
                }
            ]);

            var edgebandingPrediction = await productionManager.Predict(edgebandingPredictionRequest);

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