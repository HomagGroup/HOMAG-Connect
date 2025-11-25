using System.Diagnostics;
using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Contracts;
using HomagConnect.ProductionManager.Contracts.Orders;

namespace HomagConnect.ProductionManager.Samples.Orders.Actions
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
            var response = await productionManager.GetOrders(5).ToListAsync();

            response.Trace();

            var orderNames = response.Select(x => x.OrderName).ToList();

            orderNames.Trace(nameof(orderNames));
        }

        /// <summary />
        public static async Task GetAllOrdersHavingStatusNew(IProductionManagerClient productionManager)
        {
            var response = await productionManager.GetOrders(OrderStatus.New, 5);

            response.Trace();
        }

        /// <summary />
        public static async Task GetAllOrdersHavingStatusNewOrInProduction(IProductionManagerClient productionManager)
        {
            var response = await productionManager.GetOrders([OrderStatus.New, OrderStatus.InProduction], 5);

            response.Trace();
        }

        /// <summary />
        public static async Task GetCompletionDatesPlanned(IProductionManagerClient productionManager, string[] orderNumbers)
        {
            var orders = await productionManager.GetOrders(orderNumbers).ToListAsync();

            foreach (var order in orders)
            {
                Trace.WriteLine($"OrderName:\t{order.OrderName}");
                Trace.WriteLine($"OrderNumber:\t{order.OrderNumber}");
                Trace.WriteLine($"QuantityOfParts:\t{order.QuantityOfParts}");
                Trace.WriteLine($"CompletionDatePlanned:\t{order.CompletionDatePlanned}"); // Once all parts have been assigned to a lot, the planned completion date is provided.

                if (order.Lots != null)
                {
                    Trace.WriteLine($"Lots:\t");

                    foreach (var lot in order.Lots)
                    {
                        Trace.WriteLine($"\tLotName:\t{lot.LotName}");
                        Trace.WriteLine($"\tStartDatePlanned:\t{lot.StartDatePlanned}");
                        Trace.WriteLine($"\tCompletionDatePlanned:\t{lot.CompletionDatePlanned}");
                    }
                }

                Trace.WriteLine("");
            }
        }

        /// <summary />
        public static async Task<Order?> GetOrder(IProductionManagerClient productionManager, Guid orderId)
        {
            var order = await productionManager.GetOrder(orderId);

            Trace.WriteLine($"OrderName:\t{order.OrderName}");
            Trace.WriteLine($"OrderNumber:\t{order.OrderNumber}");
            Trace.WriteLine($"QuantityOfParts:\t{order.QuantityOfParts}");
            Trace.WriteLine($"CompletionDatePlanned:\t{order.CompletionDatePlanned}"); // Once all parts have been assigned to a lot, the planned completion date is provided.

            if (order.Lots != null)
            {
                Trace.WriteLine($"Lots:\t");

                foreach (var lot in order.Lots)
                {
                    Trace.WriteLine($"\tLotName:\t{lot.LotName}");
                    Trace.WriteLine($"\tStartDatePlanned:\t{lot.StartDatePlanned}");
                    Trace.WriteLine($"\tCompletionDatePlanned:\t{lot.CompletionDatePlanned}");
                }
            }

            return order;
        }

        /// <summary />
        public static async Task GetOrderItem(IProductionManagerClient productionManager, string [] identifiers)
        {
            var response = await productionManager.GetOrderItems(identifiers);
            response.Trace();
        }
    }
}