using HomagConnect.Base.Contracts.AdditionalData;

namespace HomagConnect.OrderManager.Contracts.Extensions;

/// <summary>
/// Extensions for <see cref="OrderItems.Base" />.
/// </summary>
public static class OrderItemsBaseExtensions
{
    /// <summary>
    /// Gets the additional data entities from the order item and contained order items.
    /// </summary>
    public static IEnumerable<AdditionalDataEntity> GetAdditionalDataEntities(this OrderItems.Base? item)
    {
        if (item != null)
        {
            if (item.AdditionalData != null)
            {
                foreach (var additionalData in item.AdditionalData)
                {
                    yield return additionalData;
                }
            }

            if (item.Items != null)
            {
                foreach (var itemItem in item.Items)
                {
                    foreach (var additionalData in GetAdditionalDataEntities(itemItem))
                    {
                        yield return additionalData;
                    }
                }
            }
        }
    }
}