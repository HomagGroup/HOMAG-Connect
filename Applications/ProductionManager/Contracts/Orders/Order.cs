using System.Diagnostics;
using System.Runtime.Serialization;
using HomagConnect.Base.Contracts;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Orders
{
    /// <summary>
    /// Order data
    /// </summary>
    [DebuggerDisplay("OrderName={OrderName}")]
    public class Order : IExtensibleDataObject
    {
        #region IExtensibleDataObject Members

        /// <intheritdoc />
        public ExtensionDataObject? ExtensionData { get; set; }

        #endregion

        #region Order Header

        /// <summary>
        /// The unique id of this order
        /// </summary>
        [JsonProperty(Order = 100)]
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the order
        /// </summary>
        [JsonProperty(Order = 110)]
        public string OrderName { get; set; } = null!;

        /// <summary>
        /// The name of the customer of this order
        /// </summary>
        [JsonProperty(Order = 111)]
        public string? CustomerName { get; set; }

        /// <summary>
        /// The number of the customer of this order
        /// </summary>
        [JsonProperty(Order = 112)]
        public string? CustomerNumber { get; set; }

        /// <summary>
        /// The company of the customer of this order
        /// </summary>
        [JsonProperty(Order = 113)]
        public string? Company { get; set; }

        /// <summary>
        /// The description of the order
        /// </summary>
        [JsonProperty(Order = 114)]
        public string? OrderDescription { get; set; }

        /// <summary>
        /// The project of the order
        /// </summary>
        [JsonProperty(Order = 115)]
        public string? Project { get; set; }

        /// <summary>
        /// The description of the order
        /// </summary>
        [JsonProperty(Order = 116)]
        public string? PersonInCharge { get; set; }

        /// <summary>
        /// The date the order was created at
        /// </summary>
        [JsonProperty(Order = 117)]
        public DateTimeOffset OrderDate { get; set; }

        /// <summary>
        /// Gets the planned delivery date of this order.
        /// </summary>
        [JsonProperty(Order = 118)]
        public DateTimeOffset? DeliveryDatePlanned { get; set; }

        /// <summary>
        /// Gets the external system id of the order which can be used as a reference.
        /// </summary>
        [JsonProperty(Order = 119)]
        [Obsolete("Use OrderNumber instead", true)]
        public string? ExternalSystemId { get; set; }

        /// <summary>
        /// Gets or sets the link to the order in productionManager.
        /// </summary>
        [JsonProperty(Order = 120)]
        public Uri? Link { get; set; }

        /// <summary>
        /// The number of the order
        /// </summary>
        [JsonProperty(Order = 121)]
        public string? OrderNumber { get; set; }

        /// <summary>
        /// The order number from the preceding system
        /// </summary>
        [JsonProperty(Order = 122)]
        public string? OrderNumberExternal { get; set; }

        #endregion

        #region Production

        #region Order Status

        /// <summary>
        /// Gets the status of the order.
        /// </summary>
        [JsonProperty(Order = 200)]
        public OrderStatus OrderStatus { get; set; }

        #endregion

        /// <summary>
        /// Gets the timestamp the order was last changed at.
        /// </summary>
        [JsonProperty(Order = 201)]
        public DateTimeOffset ChangedAt { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// Gets the timestamp the order was created at.
        /// </summary>
        [JsonProperty(Order = 202)]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// Gets the timestamp the order was released at for production.
        /// </summary>
        [JsonProperty(Order = 203)]
        public DateTimeOffset? ReleasedAt { get; set; }

        /// <summary>
        /// Gets the timestamp the order was started at, indicating the time when the first part was cut.
        /// </summary>
        [JsonProperty(Order = 204)]
        public DateTimeOffset? StartedAt { get; set; }

        /// <summary>
        /// Gets the timestamp the order was completed at, indicating the time when the last part was produced.
        /// </summary>
        [JsonProperty(Order = 205)]
        public DateTimeOffset? CompletedAt { get; set; }

        /// <summary>
        /// Gets the timestamp the order was archived at.
        /// </summary>
        [JsonProperty(Order = 206)]
        public DateTimeOffset? ArchivedAt { get; set; }

        /// <summary>
        /// Gets the order source.
        /// </summary>
        public string? Source { get; set; }

        #region Production

        /// <summary>
        /// Gets or sets the planned start date of the order.
        /// </summary>
        [JsonProperty(Order = 210)]
        public DateTimeOffset? StartDatePlanned { get; set; }

        /// <summary>
        /// Gets or sets the planned completion date of the order.
        /// </summary>
        [JsonProperty(Order = 220)]
        public DateTimeOffset? CompletionDatePlanned { get; set; }

        /// <summary>
        /// Gets or sets the completion date of the order.
        /// </summary>
        [JsonProperty(Order = 221)]
        public DateTimeOffset? CompletionDate { get; set; }

        /// <summary>
        /// Gets the quantity of articles in this order.
        /// </summary>
        [JsonProperty(Order = 230)]
        public int? QuantityOfArticles { get; set; }

        /// <summary>
        /// Gets the quantity of parts in this order.
        /// </summary>
        [JsonProperty(Order = 231)]
        public int? QuantityOfParts { get; set; }

        /// <summary>
        /// Gets the quantity of parts planned in this order.
        /// </summary>
        [JsonProperty(Order = 232)]
        public int? QuantityOfPartsPlanned { get; set; }

        /// <summary>
        /// Gets the names of the lots.
        /// </summary>
        [JsonProperty(Order = 234)]
        public OrderLotReference[]? Lots { get; set; }

        #endregion

        #endregion

        #region Address

        /// <summary>
        /// Gets or sets the address of the order.
        /// </summary>
        [JsonProperty(Order = 300)]
        public Address? Address { get; set; }

        #endregion

        #region Additional data

        /// <summary>
        /// Gets or sets the notes of the order.
        /// </summary>
        [JsonProperty(Order = 400)]
        public string? Notes { get; set; }

        #endregion
    }
}