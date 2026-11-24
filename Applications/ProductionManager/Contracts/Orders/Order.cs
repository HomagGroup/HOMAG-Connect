using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.Serialization;
using HomagConnect.Base.Contracts;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Orders
{
    /// <summary>
    /// An order in productionManager with its header data, schedule, production progress, and notes.
    /// <see href="https://docs.homag.cloud/docs/en/productionmanager-in-a-nutshell-order-details">Learn more about order </see>.
    /// </summary>
    /// <example>
    /// { "id": "3f2b8c1e-5a47-4d6e-9b0a-7c1d2e3f4a5b", "orderName": "Kitchen Miller", "customerName": "Miller",
    /// "customerNumber": "10025", "orderDate": "2025-04-01T08:30:00+00:00", "deliveryDatePlanned": "2025-05-15T00:00:00+00:00",
    /// "orderNumber": "A-2025-0042", "orderStatus": "ReadyForProduction", "changedAt": "2025-04-02T09:00:00+00:00",
    /// "createdAt": "2025-04-01T08:30:00+00:00", "releasedAt": "2025-04-02T09:00:00+00:00", "quantityOfParts": 120,
    /// "notes": "Handle with care" }
    /// </example>
    [DebuggerDisplay("OrderName={OrderName}")]
    public class Order
    {
        #region IExtensibleDataObject Members

        /// <inheritdoc />
        public ExtensionDataObject? ExtensionData { get; set; }

        #endregion

        #region Order Header

        /// <summary>
        /// Unique identifier of the order, assigned by productionManager.
        /// </summary>
        /// <example>3f2b8c1e-5a47-4d6e-9b0a-7c1d2e3f4a5b</example>
        [JsonProperty(Order = 100)]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the order as shown in productionManager. Always provide a value; the order cannot be identified without it.
        /// </summary>
        /// <example>Kitchen Miller</example>
        [JsonProperty(Order = 110)]
        public string OrderName { get; set; } = null!;

        /// <summary>
        /// Name of the customer the order is produced for. Optional.
        /// </summary>
        /// <example>Miller</example>
        [JsonProperty(Order = 111)]
        public string? CustomerName { get; set; }

        /// <summary>
        /// Customer number from the preceding system. Optional.
        /// </summary>
        /// <example>10025</example>
        [JsonProperty(Order = 112)]
        public string? CustomerNumber { get; set; }

        /// <summary>
        /// Telephone number of the customer. Optional.
        /// </summary>
        /// <example>+49 7453 12345</example>
        [JsonProperty(Order = 113)]
        public string? TelephoneNumber { get; set; }

        /// <summary>
        /// Email address of the customer. Optional.
        /// </summary>
        /// <example>info@miller.example</example>
        [JsonProperty(Order = 114)]
        public string? Email { get; set; }

        /// <summary>
        /// Company of the customer. Optional.
        /// </summary>
        /// <example>Miller Woodworks</example>
        [JsonProperty(Order = 115)]
        public string? Company { get; set; }

        /// <summary>
        /// Free-text description of the order. Optional.
        /// </summary>
        /// <example>Kitchen with island</example>
        [JsonProperty(Order = 116)]
        public string? OrderDescription { get; set; }

        /// <summary>
        /// Project the order belongs to. Optional.
        /// </summary>
        /// <example>Residential Building 7</example>
        [JsonProperty(Order = 117)]
        public string? Project { get; set; }

        /// <summary>
        /// Name of the person responsible for the order. Optional.
        /// </summary>
        /// <example>Jane Doe</example>
        [JsonProperty(Order = 118)]
        public string? PersonInCharge { get; set; }

        /// <summary>
        /// Date of the order as entered in the source system.
        /// </summary>
        /// <example>2025-04-01T08:30:00+00:00</example>
        [JsonProperty(Order = 119)]
        [DisplayFormat(DataFormatString = "{0:g}")]
        [DataType(DataType.DateTime)]
        public DateTimeOffset OrderDate { get; set; }

        /// <summary>
        /// Date by which the order is planned to be delivered to the customer. Optional.
        /// </summary>
        /// <example>2025-05-15T00:00:00+00:00</example>
        [JsonProperty(Order = 120)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTimeOffset? DeliveryDatePlanned { get; set; }

        /// <summary>
        /// Identifier of the order in an external system, usable as a reference. Optional.
        /// </summary>
        /// <example>ERP-48213</example>
        [JsonProperty(Order = 121)]
        public string? ExternalSystemId { get; set; }

        /// <summary>
        /// Link that opens the order in productionManager. Optional.
        /// </summary>
        /// <example>https://productionmanager.example/orders/3f2b8c1e-5a47-4d6e-9b0a-7c1d2e3f4a5b</example>
        [JsonProperty(Order = 122)]
        public Uri? Link { get; set; }

        /// <summary>
        /// Order number used to identify the order. Optional.
        /// </summary>
        /// <example>A-2025-0042</example>
        [JsonProperty(Order = 123)]
        public string? OrderNumber { get; set; }

        /// <summary>
        /// Order number from the preceding system, for example an ERP. Optional.
        /// </summary>
        /// <example>SO-778412</example>
        [JsonProperty(Order = 124)]
        public string? OrderNumberExternal { get; set; }

        #endregion

        #region Production

        #region Order Status

        /// <summary>
        /// Current production status of the order.
        /// </summary>
        /// <example>ReadyForProduction</example>
        [JsonProperty(Order = 200)]
        public OrderStatus OrderStatus { get; set; }

        #endregion

        /// <summary>
        /// Date and time the order was last changed.
        /// </summary>
        /// <example>2025-04-02T09:00:00+00:00</example>
        [JsonProperty(Order = 201)]
        [DisplayFormat(DataFormatString = "{0:g}")]
        [DataType(DataType.DateTime)]
        public DateTimeOffset ChangedAt { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// Date and time the order was created in productionManager.
        /// </summary>
        /// <example>2025-04-01T08:30:00+00:00</example>
        [JsonProperty(Order = 202)]
        [DisplayFormat(DataFormatString = "{0:g}")]
        [DataType(DataType.DateTime)]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// Date and time the order was released for production. Empty until the order is released.
        /// </summary>
        /// <example>2025-04-02T09:00:00+00:00</example>
        [JsonProperty(Order = 203)]
        [DisplayFormat(DataFormatString = "{0:g}")]
        [DataType(DataType.DateTime)]
        public DateTimeOffset? ReleasedAt { get; set; }

        /// <summary>
        /// Date and time production started, which is when the first part was cut. Empty until production starts.
        /// </summary>
        /// <example>2025-04-03T07:15:00+00:00</example>
        [JsonProperty(Order = 204)]
        [DisplayFormat(DataFormatString = "{0:g}")]
        [DataType(DataType.DateTime)]
        public DateTimeOffset? StartedAt { get; set; }

        /// <summary>
        /// Date and time production was completed, which is when the last part was produced. Empty until completion.
        /// </summary>
        /// <example>2025-04-05T15:40:00+00:00</example>
        [JsonProperty(Order = 205)]
        [DisplayFormat(DataFormatString = "{0:g}")]
        [DataType(DataType.DateTime)]
        public DateTimeOffset? CompletedAt { get; set; }

        /// <summary>
        /// Date and time the order was archived. Empty while the order is not archived.
        /// </summary>
        /// <example>2025-06-01T10:00:00+00:00</example>
        [JsonProperty(Order = 206)]
        [DisplayFormat(DataFormatString = "{0:g}")]
        [DataType(DataType.DateTime)]
        public DateTimeOffset? ArchivedAt { get; set; }

        /// <summary>
        /// Source the order originates from, for example the system or import it was created by. Optional.
        /// </summary>
        /// <example>Import</example>
        public string? Source { get; set; }

        #region Production

        /// <summary>
        /// Date on which production of the order is planned to start. Optional.
        /// </summary>
        /// <example>2025-04-03T00:00:00+00:00</example>
        [JsonProperty(Order = 210)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTimeOffset? StartDatePlanned { get; set; }

        /// <summary>
        /// Date on which production of the order is planned to be completed. Optional.
        /// </summary>
        /// <example>2025-04-10T00:00:00+00:00</example>
        [JsonProperty(Order = 220)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTimeOffset? CompletionDatePlanned { get; set; }

        /// <summary>
        /// Date on which the order was actually completed. Empty until completion.
        /// </summary>
        /// <example>2025-04-05T00:00:00+00:00</example>
        [JsonProperty(Order = 221)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTimeOffset? CompletionDate { get; set; }

        /// <summary>
        /// Number of articles in the order.
        /// </summary>
        /// <example>12</example>
        [JsonProperty(Order = 230)]
        [Range(0, int.MaxValue)]
        public int? QuantityOfArticles { get; set; }

        /// <summary>
        /// Total number of parts in the order.
        /// </summary>
        /// <example>120</example>
        [JsonProperty(Order = 231)]
        [Range(0, int.MaxValue)]
        public int? QuantityOfParts { get; set; }

        /// <summary>
        /// Number of parts of the order that are planned for production, for example in lots.
        /// </summary>
        /// <example>120</example>
        [JsonProperty(Order = 232)]
        [Range(0, int.MaxValue)]
        public int? QuantityOfPartsPlanned { get; set; }

        /// <summary>
        /// References to the lots that contain parts of this order.
        /// </summary>
        [JsonProperty(Order = 234)]
        public OrderLotReference[]? Lots { get; set; }

        #endregion

        #endregion

        #region Address

        /// <summary>
        /// Address of the customer or delivery address of the order. Optional.
        /// </summary>
        [JsonProperty(Order = 300)]
        public Address? Address { get; set; }

        #endregion

        #region Additional data

        /// <summary>
        /// Free-text notes about the order. Optional.
        /// </summary>
        /// <example>Handle with care</example>
        [JsonProperty(Order = 400)]
        public string? Notes { get; set; }

        #endregion
    }
}