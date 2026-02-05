using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.Orders
{
    /// <summary>
        /// Order data
        /// </summary>
        [DebuggerDisplay("OrderName={OrderName}")]
    public class OrderDetails: ISupportsAdditionalData
    {
        #region (100) Header

        /// <summary>
        /// The unique id of this order
        /// </summary>
        [JsonProperty(Order = 100)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets the status of the order.
        /// </summary>
        [JsonProperty(Order = 101)]
        [DefaultValue(OrderState.New)]
        public OrderState State { get; set; } = OrderState.New;

        /// <summary>
        /// The number of the order
        /// </summary>
        [JsonProperty(Order = 110)]
        public string? OrderNumber { get; set; }

        /// <summary>
        /// The order number from the preceding system
        /// </summary>
        [JsonProperty(Order = 111)]
        public string? OrderNumberExternal { get; set; }

        /// <summary>
        /// The name of the order
        /// </summary>
        [JsonProperty(Order = 112)]
        public string OrderName { get; set; } = null!;

        /// <summary>
        /// The description of the order
        /// </summary>
        [JsonProperty(Order = 113)]
        public string? OrderDescription { get; set; }

        /// <summary>
        /// Gets the external system id of the item which can be used as a reference.
        /// </summary>
        [JsonProperty(Order = 114)]
        public string? ExternalSystemId { get; set; }

        /// <summary>
        /// The project of the order
        /// </summary>
        [JsonProperty(Order = 120)]
        public string? Project { get; set; }

        /// <summary>
        /// The description of the order
        /// </summary>
        [JsonProperty(Order = 121)]
        public string? PersonInCharge { get; set; }

        /// <summary>
        /// The date the order was created at
        /// </summary>
        [JsonProperty(Order = 122)]
        public DateTimeOffset OrderDate { get; set; }

        /// <summary>
        /// Gets the planned delivery date of this order.
        /// </summary>
        [JsonProperty(Order = 123)]
        public DateTimeOffset? DeliveryDatePlanned { get; set; }

        /// <summary>
        /// Gets or sets the link to the order in orderManager.
        /// </summary>
        [JsonProperty(Order = 150)]
        public Uri? Link { get; set; }

        /// <summary>
        /// Gets the HasErrors flag indicates whether the order has errors. The definition of what an error is, is up to the implementation of the OrderManager. This is a flag that can be used by the UI to display an error state for the order.
        /// </summary>
        [JsonProperty(Order = 151)]
        public bool HasErrors { get; set; }

        #endregion

        #region (300) Customer

        /// <summary>
        /// The name of the customer of this order
        /// </summary>
        [JsonProperty(Order = 300)]
        public string? CustomerName { get; set; }

        /// <summary>
        /// The number of the customer of this order
        /// </summary>
        [JsonProperty(Order = 310)]
        public string? CustomerNumber { get; set; }

        /// <summary>
        /// The company of the customer of this order
        /// </summary>
        [JsonProperty(Order = 320)]
        public string? Company { get; set; }

        #endregion

        #region (200) Address

        /// <summary>
        /// Gets or sets the addresses of the order.
        /// </summary>
        [JsonProperty(Order = 200)]
        public Collection<Address>? Addresses { get; set; }

        #endregion

        #region (400) Order Details

        /// <summary>
        /// Gets the timestamp the order was last changed at.
        /// </summary>
        [JsonProperty(Order = 490)]
        public DateTimeOffset ChangedAt { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// Gets the user who changed the order last.
        /// </summary>
        [JsonProperty(Order = 491)]
        public string? ChangedBy { get; set; }

        /// <summary>
        /// Gets the timestamp the order was created at.
        /// </summary>
        [JsonProperty(Order = 421)]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// Gets the timestamp the order was released at for production.
        /// </summary>
        [JsonProperty(Order = 422)]
        public DateTimeOffset? ReleasedAt { get; set; }

        /// <summary>
        /// Gets the timestamp the order was started at, indicating the time when the first feedback was received from the
        /// production system.
        /// </summary>
        [JsonProperty(Order = 423)]
        public DateTimeOffset? StartedAt { get; set; }

        /// <summary>
        /// Gets the timestamp the order was completed at, indicating the time when the production system confirmed the order as
        /// completed.
        /// </summary>
        [JsonProperty(Order = 424)]
        public DateTimeOffset? CompletedAt { get; set; }

        /// <summary>
        /// Gets the timestamp the order was archived at.
        /// </summary>
        [JsonProperty(Order = 425)]
        public DateTimeOffset? ArchivedAt { get; set; }

        /// <summary>
        /// Gets the quantity of articles in this order.
        /// </summary>
        [JsonProperty(Order = 420)]
        public int? QuantityOfArticles { get; set; }

        /// <summary>
        /// Gets the quantity of parts in this order.
        /// </summary>
        [JsonProperty(Order = 410)]
        public int? QuantityOfParts { get; set; }

        /// <summary>
        /// Gets the quantity of parts planned in this order.
        /// </summary>
        [JsonProperty(Order = 411)]
        public int? QuantityOfPartsPlanned { get; set; }       

        #endregion

        #region (600) Additional Data

        /// <summary>
        /// Gets or sets the additional data.
        /// </summary>
        [JsonProperty(Order = 600)]
        public Collection<AdditionalDataEntity>? AdditionalData { get; set; }

        /// <summary>
        /// Gets or sets the additional properties configured in the application.
        /// </summary>
        [JsonExtensionData]
        [JsonProperty(Order = 602)]
        public IDictionary<string, object>? AdditionalProperties { get; set; }

        /// <summary>
        /// Gets or sets the notes of the order.
        /// </summary>
        [JsonProperty(Order = 601)]
        public string? Notes { get; set; }

        #endregion

        #region (700) Order items

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        [JsonProperty(Order = 700)]
        public Collection<OrderItems.Base>? Items { get; set; }

        #endregion
    }
}