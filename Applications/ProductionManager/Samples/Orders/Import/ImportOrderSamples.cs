using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Contracts;
using HomagConnect.ProductionManager.Contracts.Import;

namespace HomagConnect.ProductionManager.Samples.Orders.Import
{
    /// <summary>
    /// Sample class which shows how to import an order.
    /// </summary>
    public static class ImportOrderSamples
    {
        /// <summary>
        /// Imports an order using a project zip file and waits for the completion of the import operation.
        /// </summary>
        /// <param name="productionManager"></param>
        public static async Task<Contracts.Orders.Order> ImportOrderUsingProjectZipAndWaitForCompletion(IProductionManagerClient productionManager)
        {
            var projectFile = new FileInfo(@"Orders\Project.zip");
            
            var request = new ImportOrderRequest
            {
                Action = ImportOrderRequestAction.ImportOnly
            };

            var response = await productionManager.ImportOrderRequest(request, projectFile);
            var orderDetails = await productionManager.WaitForImportOrderCompletion(response.CorrelationId, TimeSpan.FromMinutes(1));

            orderDetails.Trace();

            return orderDetails;
        }
    }
}
