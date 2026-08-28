using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.OrderManager.Contracts.OrderItems;

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

    /// <summary>
    /// Searches through the order items and returns the first order item matching the given predicate, 
    /// or <c>null</c> if no matching order item is found. When <paramref name="recursive" /> is <c>true</c>, the
    /// search descends into all nested order items; otherwise only the order items in the collection itself are searched.
    /// </summary>
    public static OrderItems.Base? Find(this IEnumerable<OrderItems.Base?>? items, Func<OrderItems.Base, bool> predicate, bool recursive = false)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        if (items == null)
        {
            return null;
        }

        foreach (var item in items)
        {
            if (item == null)
            {
                continue;
            }

            if (predicate(item))
            {
                return item;
            }

            if (recursive)
            {
                var match = item.Items.Find(predicate, recursive: true);

                if (match != null)
                {
                    return match;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Returns all order items in the collection matching the given predicate. The search is not recursive; only the order
    /// items in the collection itself are considered.
    /// </summary>
    public static IEnumerable<OrderItems.Base> FindAll(this IEnumerable<OrderItems.Base?>? items, Func<OrderItems.Base, bool> predicate)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        if (items == null)
        {
            return Enumerable.Empty<OrderItems.Base>();
        }

        return FindAllIterator(items, predicate);
    }

    private static IEnumerable<OrderItems.Base> FindAllIterator(IEnumerable<OrderItems.Base?> items, Func<OrderItems.Base, bool> predicate)
    {
        foreach (var item in items)
        {
            if (item != null && predicate(item))
            {
                yield return item;
            }
        }
    }

    /// <summary>
    /// Removes all order items matching the given predicate from the collection. When <paramref name="recursive" /> is
    /// <c>true</c>, matching order items are also removed from all nested order items; otherwise only the order items in the
    /// collection itself are considered.
    /// </summary>
    public static void ClearItems(this ICollection<OrderItems.Base>? items, Func<OrderItems.Base, bool> predicate, bool recursive = false)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        if (items == null)
        {
            return;
        }

        foreach (var item in items.ToList())
        {
            if (item == null)
            {
                continue;
            }

            if (predicate(item))
            {
                items.Remove(item);
                continue;
            }

            if (recursive)
            {
                item.Items.ClearItems(predicate, recursive: true);
            }
        }
    }

    /// <summary>
    /// Gets the id of the library the <paramref name="group" /> belongs to. Since a group typically only contains configuration
    /// positions of a single library, the library id of the first configuration position found within the group is returned, or
    /// <c>null</c> if the group does not contain any configuration position with a library id.
    /// </summary>
    public static string? GetLibraryId(this Group? group)
    {
        if (group?.Items == null)
        {
            return null;
        }

        var position = group.Items.Find(item => item is ConfigurationPosition pos && !string.IsNullOrEmpty(pos.LibraryId), recursive: true) as ConfigurationPosition;

        return position?.LibraryId;
    }
}