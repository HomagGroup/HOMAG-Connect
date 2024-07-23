using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Contracts;
using HomagConnect.ProductionManager.Contracts.Import;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.ProductionManager.Samples.Orders.Import
{
    /// <summary>
    /// Sample class which shows how to import an order.
    /// </summary>
    public static class ImportOrderSamples
    {
        /// <summary>
        /// Imports an order using a project zip file.
        /// </summary>
        /// <param name="productionManager"></param>
        public static async Task ImportOrderUsingProjectZipAsync(IProductionManagerClient productionManager)
        {
            var projectFile = new FileInfo(@"Orders\Project.zip");
            
            var request = new ImportOrderRequest
            {
                Action = ImportOrderRequestAction.ImportOnly
            };

            var response = await productionManager.ImportOrder(request, projectFile);

            Assert.AreNotEqual(Guid.Empty, response.CorrelationId);
            
            response.Trace();
        }

        /// <summary>
        /// Get the status of an order import operation.
        /// </summary>
        /// <param name="productionManager"></param>
        public static async Task GetImportOrderStateAsync(IProductionManagerClient productionManager)
        {
            //First trigger an import job to get a correlation Id
            var projectFile = new FileInfo(@"Orders\Project.zip");

            var request = new ImportOrderRequest
            {
                Action = ImportOrderRequestAction.ImportOnly
            };

            var importOrderJob = await productionManager.ImportOrder(request, projectFile);

            var correlationId = importOrderJob.CorrelationId;

            var response = await productionManager.GetImportOrderState(correlationId);

            response.Trace();
        }
    }
}
