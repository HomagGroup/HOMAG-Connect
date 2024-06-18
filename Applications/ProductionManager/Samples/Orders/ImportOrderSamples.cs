using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Contracts;
using HomagConnect.ProductionManager.Contracts.Import;

namespace HomagConnect.ProductionManager.Samples.Orders
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

            var response = await productionManager.ImportOrderAsync(request, projectFile);
            
            response.Trace();
        }

        /// <summary>
        /// Get the status of an order import operation.
        /// </summary>
        /// <param name="productionManager"></param>
        public static async Task GetImportOrderStateAsync(IProductionManagerClient productionManager)
        {
            var correlationId = Guid.NewGuid();

            var response = await productionManager.GetImportOrderStateAsync(correlationId);

            response.Trace();
        }
    }
}
