using HomagConnect.Base.Contracts;
using HomagConnect.ProductionManager.Contracts;
using HomagConnect.ProductionManager.Contracts.Orders;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

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

            var patchData = PatchBuilder<OrderDetails>.For()
                .Set(o => o.CustomerName, "Muster GmbH")
                .Set(o => o.DeliveryDatePlanned, DateTime.Parse("2026-09-15T00:00:00Z"))
                .Set(o => o.Email, null)
                .Build();

            await productionManagerClient.PatchOrder(identifier, patchData);
        }
    }
}
