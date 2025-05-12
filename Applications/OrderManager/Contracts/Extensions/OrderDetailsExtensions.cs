using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.OrderManager.Contracts.Orders;

namespace HomagConnect.OrderManager.Contracts.Extensions
{
    /// <summary>
    /// Extensions for <see cref="OrderDetails" />.
    /// </summary>
    public static class OrderDetailsExtensions
    {
        /// <summary>
        /// Gets the additional data entities from the order details and contained order items.
        /// </summary>
        public static IEnumerable<AdditionalDataEntity> GetAdditionalDataEntities(this OrderDetails? orderDetails)
        {
            if (orderDetails != null)
            {
                if (orderDetails.AdditionalData != null)
                {
                    foreach (var additionalData in orderDetails.AdditionalData)
                    {
                        yield return additionalData;
                    }
                }

                if (orderDetails.Items != null)
                {
                    foreach (var orderDetailsItem in orderDetails.Items)
                    {
                        foreach (var additionalData in orderDetailsItem.GetAdditionalDataEntities())
                        {
                            yield return additionalData;
                        }
                    }
                }
            }
        }
    }
}