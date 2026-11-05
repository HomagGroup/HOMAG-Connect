using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace HomagConnect.OrderManager.Contracts.OrderItems
{
    /// <summary>
    /// Defines a contract for objects that can contain an optional collection of items.
    /// </summary>
    public interface ISupportsItems
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public Collection<Base>? Items { get; set; }
    }
}