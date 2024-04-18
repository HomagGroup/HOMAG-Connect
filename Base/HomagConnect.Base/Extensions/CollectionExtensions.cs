using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HomagConnect.Base.Extensions;

/// <summary>
/// Extensions for <see cref="Collection{T}" />
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    /// Adds a range of items to the collection.
    /// </summary>
    public static void AddRange<T>(this Collection<T> collection, IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            collection.Add(item);
        }
    }
}