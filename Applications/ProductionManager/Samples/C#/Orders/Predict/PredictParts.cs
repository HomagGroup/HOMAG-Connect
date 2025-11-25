using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Contracts;
using HomagConnect.ProductionManager.Contracts.Predict;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.ProductionManager.Samples.Orders.Predict
{
    /// <summary>
    /// Sample class which shows how to get a prediction of productiontime
    /// </summary>
    public static class PredictParts
    {
        /// <summary>
        /// Predicts a list of parts for edgebanding
        /// </summary>
        /// <param name="productionManager"></param>
        public static async Task<EdgebandingPrediction> PredictPartsEdgebandingAsync(IProductionManagerClient productionManager)
        {
            var edgebandingPredictionRequest = new EdgebandingPredictionRequest
            {
                MachineNumber = "",
                PredictionParts = new List<EdgebandingPredictionPart>{
                    new EdgebandingPredictionPart
                    {
                        Quantity = 10,
                        Length = 700,
                        Width = 300,
                        Thickness = 18,
                        EdgeFront = "Oak",
                        EdgeBack = null,
                        EdgeLeft = "white",
                        EdgeRight = "white"
                    },
                    new EdgebandingPredictionPart
                    {
                        Quantity = 5,
                        Length = 800,
                        Width = 200,
                        Thickness = 18,
                        EdgeFront = "Oak"
                    }
                }
            };

            var response = await productionManager.Predict(edgebandingPredictionRequest);

            Assert.IsNotNull(response);

            response.Trace();

            return response;
        }

        /// <summary>
        /// Predicts a list of parts for cutting
        /// </summary>
        /// <param name="productionManager"></param>
        public static async Task<CuttingPrediction> PredictPartsCuttingAsync(IProductionManagerClient productionManager)
        {
            var cuttingPredictionRequest = new CuttingPredictionRequest
            {
                MachineNumber = "",
                PredictionParts = new List<CuttingPredictionPart>{
                    new CuttingPredictionPart
                    {
                        Quantity = 10,
                        Length = 700,
                        Width = 300,
                        Thickness = 18,
                    },
                    new CuttingPredictionPart
                    {
                        Quantity = 5,
                        Length = 800,
                        Width = 200,
                        Thickness = 18,
                    }
                }
            };

            var response = await productionManager.Predict(cuttingPredictionRequest);

            Assert.IsNotNull(response);

            response.Trace();

            return response;
        }
        /// <summary>
        /// Predicts a list of parts for Cnc
        /// </summary>
        /// <param name="productionManager"></param>
        public static async Task<CncPrediction> PredictPartsCncAsync(IProductionManagerClient productionManager)
        {
            var cncPredictionRequest = new CncPredictionRequest
            {
                MachineNumber = "",
                PredictionParts = new List<CncPredictionPart>{
                    new CncPredictionPart
                    {
                        Quantity = 10,
                        Length = 700,
                        Width = 300,
                        Thickness = 18,
                        CncProgramName1 = "a"
                    },
                    new CncPredictionPart
                    {
                        Quantity = 5,
                        Length = 800,
                        Width = 200,
                        Thickness = 18,
                        CncProgramName2 = "b"
                    }
                }
            };

            var response = await productionManager.Predict(cncPredictionRequest);

            Assert.IsNotNull(response);

            response.Trace();

            return response;
        }

    }
}
