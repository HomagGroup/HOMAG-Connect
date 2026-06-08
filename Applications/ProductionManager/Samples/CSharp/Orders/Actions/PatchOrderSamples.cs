using HomagConnect.ProductionManager.Contracts;
using HomagConnect.ProductionManager.Contracts.Orders;
using Newtonsoft.Json.Linq;

namespace HomagConnect.ProductionManager.Samples.Orders.Actions
{
    /// <summary>
    /// Patch order samples
    /// </summary>
    public static class PatchOrderSamples
    {
        /// <summary>
        /// Patch order
        /// </summary>
        /// <param name="productionManagerClient"></param>
        /// <returns></returns>
        public static async Task PatchOrder(IProductionManagerClient productionManagerClient)
        {
            string identifier = Guid.NewGuid().ToString(); // set existing order identifier (e.g. order number/ order id/ order externalNumber)

            var patchData = new JObject
            {
                [nameof(OrderDetails.CustomerName)] = "Muster GmbH",
                [nameof(OrderDetails.DeliveryDatePlanned)] = "2026-09-15T00:00:00Z",
                [nameof(OrderDetails.Email)] = null // cleared
            };

            //or use directly the name of the property
            //patchData = new JObject
            //{
            //    ["customerName"] = "Muster GmbH",
            //    ["deliveryDatePlanned"] = "2026-09-15T00:00:00Z",
            //    ["email"] = null // cleared
            //};

            await productionManagerClient.PatchOrder(identifier, patchData);
        }
    }
}
