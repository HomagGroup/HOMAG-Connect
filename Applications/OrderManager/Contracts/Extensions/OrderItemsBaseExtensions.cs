using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.OrderManager.Contracts.OrderItems;

namespace HomagConnect.OrderManager.Contracts.Extensions;

/// <summary>
/// Extensions for <see cref="OrderItems.Base" />.
/// </summary>
public static class OrderItemsBaseExtensions
{
    #region Find and count items

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
    /// Counts all order items matching the given predicate. When <paramref name="recursive" /> is <c>true</c>, the count
    /// includes all nested order items; otherwise only the order items in the collection itself are considered.
    /// </summary>
    public static int Count(this IEnumerable<OrderItems.Base?>? items, Func<OrderItems.Base, bool> predicate, bool recursive = false)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        if (items == null)
        {
            return 0;
        }

        var count = 0;

        foreach (var item in items)
        {
            if (item == null)
            {
                continue;
            }

            if (predicate(item))
            {
                count++;
            }

            if (recursive)
            {
                count += item.Items.Count(predicate, recursive: true);
            }
        }

        return count;
    }

    /// <summary>
    /// Returns all order items matching the given predicate. When <paramref name="recursive" /> is <c>true</c>, the search
    /// descends into all nested order items; otherwise only the order items in the collection itself are considered.
    /// </summary>
    public static IEnumerable<OrderItems.Base> FindAll(this IEnumerable<OrderItems.Base?>? items, Func<OrderItems.Base, bool> predicate, bool recursive = false)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        if (items == null)
        {
            return Enumerable.Empty<OrderItems.Base>();
        }

        return FindAllIterator(items, predicate, recursive);
    }

    /// <summary>
    /// Searches through the order items and returns the parent of the first order item matching the given 
    /// predicate, or <c>null</c> if no matching order item is found or the matching item has no parent. 
    /// The search always descends into all nested order items.
    /// </summary>
    public static OrderItems.Base? FindParent(this IEnumerable<OrderItems.Base?>? items, Func<OrderItems.Base, bool> predicate)
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

            if (item.Items.Find(predicate) != null)
            {
                return item;
            }

            var parent = item.Items.FindParent(predicate);
            if (parent != null)
            {
                return parent;
            }
        }

        return null;
    }

    private static IEnumerable<OrderItems.Base> FindAllIterator(IEnumerable<OrderItems.Base?> items, Func<OrderItems.Base, bool> predicate, bool recursive)
    {
        foreach (var item in items)
        {
            if (item == null)
            {
                continue;
            }

            if (predicate(item))
            {
                yield return item;
            }

            if (recursive)
            {
                foreach (var match in item.Items.FindAll(predicate, recursive: true))
                {
                    yield return match;
                }
            }
        }
    }

    #endregion

    #region Get and modify data

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

    #endregion

    #region Errors and Warnings

    private const string ErrorCategory = "Error";
    private const string WarningCategory = "Warning";

    /// <summary>
    /// Determines whether the <paramref name="item" /> contains an <see cref="ErrorInfo" /> with category "Error" (case-insensitive).
    /// When <paramref name="recursive" /> is <c>true</c>, all nested order items are also considered; otherwise only the
    /// items directly contained in <paramref name="item" /> are considered.
    /// </summary>
    public static bool HasErrors(this OrderItems.Base? item, bool recursive = false)
    {
        return item?.Items.HasErrors(recursive) ?? false;
    }

    /// <summary>
    /// Determines whether the <paramref name="items" /> contain an <see cref="ErrorInfo" /> with category "Error" (case-insensitive).
    /// When <paramref name="recursive" /> is <c>true</c>, all nested order items are also considered; otherwise only the
    /// order items in the collection itself are considered.
    /// </summary>
    public static bool HasErrors(this IEnumerable<OrderItems.Base?>? items, bool recursive = false)
    {
        return items.HasErrorInfoWithCategory(ErrorCategory, recursive);
    }

    /// <summary>
    /// Determines whether the <paramref name="item" /> contains an <see cref="ErrorInfo" /> with category "Warning" (case-insensitive).
    /// When <paramref name="recursive" /> is <c>true</c>, all nested order items are also considered; otherwise only the
    /// items directly contained in <paramref name="item" /> are considered.
    /// </summary>
    public static bool HasWarnings(this OrderItems.Base? item, bool recursive = false)
    {
        return item?.Items.HasWarnings(recursive) ?? false;
    }

    /// <summary>
    /// Determines whether the <paramref name="items" /> contain an <see cref="ErrorInfo" /> with category "Warning" (case-insensitive).
    /// When <paramref name="recursive" /> is <c>true</c>, all nested order items are also considered; otherwise only the
    /// order items in the collection itself are considered.
    /// </summary>
    public static bool HasWarnings(this IEnumerable<OrderItems.Base?>? items, bool recursive = false)
    {
        return items.HasErrorInfoWithCategory(WarningCategory, recursive);
    }

    /// <summary>
    /// Counts the <see cref="ErrorInfo" /> items with category "Error" (case-insensitive) contained in the <paramref name="item" />.
    /// When <paramref name="recursive" /> is <c>true</c>, all nested order items are also considered; otherwise only the
    /// items directly contained in <paramref name="item" /> are considered.
    /// </summary>
    public static int CountErrors(this OrderItems.Base? item, bool recursive = false)
    {
        return item?.Items.CountErrors(recursive) ?? 0;
    }

    /// <summary>
    /// Counts the <see cref="ErrorInfo" /> items with category "Error" (case-insensitive) contained in the <paramref name="items" />.
    /// When <paramref name="recursive" /> is <c>true</c>, all nested order items are also considered; otherwise only the
    /// order items in the collection itself are considered.
    /// </summary>
    public static int CountErrors(this IEnumerable<OrderItems.Base?>? items, bool recursive = false)
    {
        return items.CountErrorInfoWithCategory(ErrorCategory, recursive);
    }

    /// <summary>
    /// Counts the <see cref="ErrorInfo" /> items with category "Warning" (case-insensitive) contained in the <paramref name="item" />.
    /// When <paramref name="recursive" /> is <c>true</c>, all nested order items are also considered; otherwise only the
    /// items directly contained in <paramref name="item" /> are considered.
    /// </summary>
    public static int CountWarnings(this OrderItems.Base? item, bool recursive = false)
    {
        return item?.Items.CountWarnings(recursive) ?? 0;
    }

    /// <summary>
    /// Counts the <see cref="ErrorInfo" /> items with category "Warning" (case-insensitive) contained in the <paramref name="items" />.
    /// When <paramref name="recursive" /> is <c>true</c>, all nested order items are also considered; otherwise only the
    /// order items in the collection itself are considered.
    /// </summary>
    public static int CountWarnings(this IEnumerable<OrderItems.Base?>? items, bool recursive = false)
    {
        return items.CountErrorInfoWithCategory(WarningCategory, recursive);
    }

    private static bool HasErrorInfoWithCategory(this IEnumerable<OrderItems.Base?>? items, string category, bool recursive)
    {
        return items.Find(item => IsErrorInfoWithCategory(item, category), recursive: recursive) != null;
    }

    private static int CountErrorInfoWithCategory(this IEnumerable<OrderItems.Base?>? items, string category, bool recursive)
    {
        return items.Count(item => IsErrorInfoWithCategory(item, category), recursive: recursive);
    }

    private static bool IsErrorInfoWithCategory(OrderItems.Base item, string category)
    {
        return item is ErrorInfo errorInfo && string.Equals(errorInfo.Category, category, StringComparison.OrdinalIgnoreCase);
    }

    #endregion
}