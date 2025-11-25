using HomagConnect.Base.Extensions;
using HomagConnect.ProductionAssist.Contracts;

namespace HomagConnect.ProductionAssist.Samples.OrderItems
{
    /// <summary>
    /// ProductionAssist orderItems samples.
    /// </summary>
    public static class ProductionAssistOrderItemSamples
    {
        /// <summary>
        /// Sample showing how to retrieve order items.
        /// </summary>
        public static async Task GetOrderItemsWorkstations(IProductionAssistClient client)
        {
            var identifiers =new string[]
            {
                "orderItem1Identifier",
                "orderItem2Identifier"
            };

            //Get the data  
            var response = await client.GetOrderItems(identifiers);

            //use the retrieved data
            response.Trace();
        }
    }
}