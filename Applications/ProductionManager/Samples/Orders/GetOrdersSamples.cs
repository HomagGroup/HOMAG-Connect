﻿using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Contracts;

namespace HomagConnect.ProductionManager.Samples.Orders
{
    /// <summary>
    /// Sample class which shows how to get orders.
    /// </summary>
    public static class GetOrderSamples
    {
        /// <summary>
        /// Gets all the available orders for a customer.
        /// </summary>
        public static async Task GetAllOrdersAsync(IProductionManagerClient productionManager)
        {
            var response = await productionManager.GetOrders();
            
            response.Trace();
        }
    }
}