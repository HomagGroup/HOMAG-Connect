using System.Collections.ObjectModel;
using System.Diagnostics;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.OrderManager.Contracts.Items;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts
{
    /// <summary>
    /// Order data
    /// </summary>
    [DebuggerDisplay("OrderName={OrderName}")]
    public class Order
    {
        #region Order Header

        /// <summary>
        /// The unique id of this order
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the order
        /// </summary>
        public string OrderName { get; set; } = null!;

        /// <summary>
        /// The name of the customer of this order
        /// </summary>
        public string? CustomerName { get; set; }

        /// <summary>
        /// The number of the customer of this order
        /// </summary>
        public string? CustomerNumber { get; set; }

        /// <summary>
        /// The company of the customer of this order
        /// </summary>
        public string? Company { get; set; }

        /// <summary>
        /// The description of the order
        /// </summary>
        public string? OrderDescription { get; set; }

        /// <summary>
        /// The project of the order
        /// </summary>
        public string? Project { get; set; }

        /// <summary>
        /// The description of the order
        /// </summary>
        public string? PersonInCharge { get; set; }

        /// <summary>
        /// The date the order was created at
        /// </summary>
        public DateTimeOffset OrderDate { get; set; }

        /// <summary>
        /// Gets the planned delivery date of this order.
        /// </summary>
        public DateTimeOffset? DeliveryDatePlanned { get; set; }

        /// <summary>
        /// Gets or sets the link to the order in orderManager.
        /// </summary>
        public Uri? Link { get; set; }

        /// <summary>
        /// The number of the order
        /// </summary>
        public string? OrderNumber { get; set; }

        #endregion

        #region Order Status

        /// <summary>
        /// Gets the status of the order.
        /// </summary>
        public OrderState OrderStatus { get; set; }

        #endregion

        #region Address

        /// <summary>
        /// Gets or sets the addresses of the order.
        /// </summary>
        public Collection<Address>? Addresses { get; set; }

        #endregion

        #region Date/Times

        /// <summary>
        /// Gets the timestamp the order was last changed at.
        /// </summary>
        public DateTimeOffset ChangedAt { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// Gets the user who changed the order last.
        /// </summary>
        public string? ChangedBy { get; set; }

        /// <summary>
        /// Gets the timestamp the order was created at.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// Gets the timestamp the order was released at for production.
        /// </summary>
        public DateTimeOffset? ReleasedAt { get; set; }

        /// <summary>
        /// Gets the timestamp the order was started at, indicating the time when the first feedback was received from the
        /// production system.
        /// </summary>
        public DateTimeOffset? StartedAt { get; set; }

        /// <summary>
        /// Gets the timestamp the order was completed at, indicating the time when the production system confirmed the order as
        /// completed.
        /// </summary>
        public DateTimeOffset? CompletedAt { get; set; }

        /// <summary>
        /// Gets the timestamp the order was archived at.
        /// </summary>
        public DateTimeOffset? ArchivedAt { get; set; }

        #endregion

        #region Production

        /// <summary>
        /// Gets or sets the planned start date of the order.
        /// </summary>
        public DateTimeOffset? StartDatePlanned { get; set; }

        /// <summary>
        /// Gets or sets the planned completion date of the order.
        /// </summary>
        public DateTimeOffset? CompletionDatePlanned { get; set; }

        /// <summary>
        /// Gets or sets the completion date of the order.
        /// </summary>
        public DateTimeOffset? CompletionDate { get; set; }

        /// <summary>
        /// Gets the quantity of articles in this order.
        /// </summary>
        public int? QuantityOfArticles { get; set; }

        /// <summary>
        /// Gets the quantity of parts in this order.
        /// </summary>
        public int? QuantityOfParts { get; set; }

        /// <summary>
        /// Gets the quantity of parts planned in this order.
        /// </summary>
        public int? QuantityOfPartsPlanned { get; set; }

        /// <summary>
        /// Gets or sets the notes of the order.
        /// </summary>
        public string? Notes { get; set; }

        #endregion

        #region Additional Data

        /// <summary>
        /// Gets or sets the additional data.
        /// </summary>
        public Collection<AdditionalDataEntity>? AdditionalData { get; set; }

        /// <summary>
        /// Gets or sets the additional properties configured in the application.
        /// </summary>
        [JsonProperty(Order = 90)]
        [JsonExtensionData]
        public IDictionary<string, object>? AdditionalProperties { get; set; }

        #endregion

        #region Items

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public Collection<ItemBase>? Items { get; set; }

      

        #endregion
    }
}