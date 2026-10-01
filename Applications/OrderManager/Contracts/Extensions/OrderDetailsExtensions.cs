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

        #region Errors and Warnings

        /// <summary>
        /// Determines whether the <paramref name="orderDetails" /> contain an error (recursively).
        /// </summary>
        public static bool HasErrors(this OrderDetails? orderDetails)
        {
            return orderDetails?.Items.HasErrors(recursive: true) ?? false;
        }

        /// <summary>
        /// Determines whether the <paramref name="orderDetails" /> contain a warning (recursively).
        /// </summary>
        public static bool HasWarnings(this OrderDetails? orderDetails)
        {
            return orderDetails?.Items.HasWarnings(recursive: true) ?? false;
        }

        /// <summary>
        /// Counts the errors contained in the <paramref name="orderDetails" /> (recursively).
        /// </summary>
        public static int CountErrors(this OrderDetails? orderDetails)
        {
            return orderDetails?.Items.CountErrors(recursive: true) ?? 0;
        }

        /// <summary>
        /// Counts the warnings contained in the <paramref name="orderDetails" /> (recursively).
        /// </summary>
        public static int CountWarnings(this OrderDetails? orderDetails)
        {
            return orderDetails?.Items.CountWarnings(recursive: true) ?? 0;
        }

        #endregion
    }
}