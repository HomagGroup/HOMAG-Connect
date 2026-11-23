using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Interfaces;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.Orders
{
    /// <summary>
    /// Order with its header data, customer, addresses, additional data and order items.
    /// </summary>
    /// <example>
    /// { "id": "3f2b8c1e-5a47-4d6e-9b0a-7c1d2e3f4a5b", "state": "ReadyForProduction", "type": "CustomerOrder", "orderNumber": "ORD-2025-0042", "orderName": "Kitchen Miller", "orderDate": "2025-04-01T08:30:00+00:00",
    /// "customerName": "Miller Kitchens", "customerNumber": "C-1001", "quantityOfParts": 96, "createdAt": "2025-04-01T08:30:00+00:00", "changedAt": "2025-04-02T09:00:00+00:00", "items": [] }
    /// </example>
    [DebuggerDisplay("OrderName={OrderName}")]
    public class OrderDetails : ISupportsAdditionalData, OrderItems.ISupportsItems
    {
        #region (100) Header

        /// <summary>
        /// Unique identifier of the order.
        /// </summary>
        /// <example>3f2b8c1e-5a47-4d6e-9b0a-7c1d2e3f4a5b</example>
        [Key]
        [JsonProperty(Order = 100)]
        public Guid Id { get; set; }

        /// <summary>
        /// Current state of the order. Defaults to <c>New</c>.
        /// </summary>
        /// <example>ReadyForProduction</example>
        [JsonProperty(Order = 101)]
        [DefaultValue(OrderState.New)]
        public OrderState State { get; set; } = OrderState.New;

        /// <summary>
        /// Type of the order. Defaults to <c>CustomerOrder</c>.
        /// </summary>
        /// <example>CustomerOrder</example>
        [JsonProperty(Order = 102)]
        [DefaultValue(OrderType.CustomerOrder)]
        public OrderType Type { get; set; } = OrderType.CustomerOrder;

        /// <summary>
        /// Number of the order. Optional.
        /// </summary>
        /// <example>ORD-2025-0042</example>
        [JsonProperty(Order = 110)]
        public string? OrderNumber { get; set; }

        /// <summary>
        /// Order number from the preceding system, for example an ERP system. Optional.
        /// </summary>
        /// <example>ERP-90017</example>
        [JsonProperty(Order = 111)]
        public string? OrderNumberExternal { get; set; }

        /// <summary>
        /// Name of the order.
        /// </summary>
        /// <example>Kitchen Miller</example>
        [JsonProperty(Order = 112)]
        public string OrderName { get; set; } = null!;

        /// <summary>
        /// Description of the order. Optional.
        /// </summary>
        /// <example>Kitchen with oak fronts</example>
        [JsonProperty(Order = 113)]
        public string? OrderDescription { get; set; }

        /// <summary>
        /// Identifier of the order in an external system, which can be used as a reference. Optional.
        /// </summary>
        /// <example>ext-7781</example>
        [JsonProperty(Order = 114)]
        public string? ExternalSystemId { get; set; }

        /// <summary>
        /// System the order originates from. Optional.
        /// </summary>
        /// <example>ERP</example>
        [JsonProperty(Order = 115)]
        public string? Source { get; set; }

        /// <summary>
        /// Project the order belongs to. Optional.
        /// </summary>
        /// <example>Miller House</example>
        [JsonProperty(Order = 120)]
        public string? Project { get; set; }

        /// <summary>
        /// Person in charge of the order. Optional.
        /// </summary>
        /// <example>J. Smith</example>
        [JsonProperty(Order = 121)]
        public string? PersonInCharge { get; set; }

        /// <summary>
        /// Date and time of the order.
        /// </summary>
        /// <example>2025-04-01T08:30:00+00:00</example>
        [JsonProperty(Order = 122)]
        public DateTimeOffset OrderDate { get; set; }

        /// <summary>
        /// Planned delivery date and time of the order. Optional.
        /// </summary>
        /// <example>2025-05-15T00:00:00+00:00</example>
        [JsonProperty(Order = 123)]
        public DateTimeOffset? DeliveryDatePlanned { get; set; }

        /// <summary>
        /// Planned completion date and time of the order. Optional.
        /// </summary>
        /// <example>2025-05-10T00:00:00+00:00</example>
        [JsonProperty(Order = 124)]
        public DateTimeOffset? CompletionDatePlanned { get; set; }

        /// <summary>
        /// Link to the order in orderManager. Optional.
        /// </summary>
        /// <example>https://example.com/orders/3f2b8c1e-5a47-4d6e-9b0a-7c1d2e3f4a5b</example>
        [JsonProperty(Order = 150)]
        public Uri? Link { get; set; }

        /// <summary>
        /// <c>true</c> if the order has errors; otherwise <c>false</c>. What counts as an error is defined by orderManager; clients can use the flag to display an error state.
        /// </summary>
        /// <example>false</example>
        [JsonProperty(Order = 151)]
        public bool HasErrors { get; set; }

        /// <summary>
        /// Identifier of the order this order refers to, for example the original order of a reclaim order. Optional.
        /// </summary>
        /// <example>5a1c7e90-3b2d-4f68-8a14-9c0e1d2f3a4b</example>
        [JsonProperty(Order = 152)]
        public Guid? ReferencedOrderId { get; set; }

        #endregion

        #region (300) Customer

        /// <summary>
        /// Name of the customer of the order. Optional.
        /// </summary>
        /// <example>Miller Kitchens</example>
        [JsonProperty(Order = 300)]
        public string? CustomerName { get; set; }

        /// <summary>
        /// Number of the customer of the order. Optional.
        /// </summary>
        /// <example>C-1001</example>
        [JsonProperty(Order = 310)]
        public string? CustomerNumber { get; set; }

        /// <summary>
        /// Number of the customer in an external system, for example an ERP system. Optional.
        /// </summary>
        /// <example>ERP-4711</example>
        [JsonProperty(Order = 315)]
        public string? CustomerNumberExternal { get; set; }

        /// <summary>
        /// Company of the customer. Obsolete; use <see cref="CustomerName" /> instead.
        /// </summary>
        [JsonProperty(Order = 320)]
        [Obsolete("Use CustomerName instead.")]
        public string? Company { get; set; }

        /// <summary>
        /// Telephone number of the customer. Optional.
        /// </summary>
        /// <example>+49 7443 12-0</example>
        [JsonProperty(Order = 321)]
        public string? TelephoneNumber { get; set; }

        /// <summary>
        /// Email address of the customer. Optional.
        /// </summary>
        /// <example>info@miller.example.com</example>
        [JsonProperty(Order = 322)]
        public string? Email { get; set; }

        /// <summary>
        /// Country and language of the customer as an IETF language tag. Optional.
        /// </summary>
        /// <example>de-CH</example>
        public string? CustomerCulture { get; set; }

        /// <summary>
        /// Preferred language of the customer as a two-letter ISO code. Optional.
        /// </summary>
        /// <example>EN</example>
        public string? CustomerLanguage { get; set; }

        /// <summary>
        /// Currency of the customer as an ISO 4217 currency code. Optional.
        /// </summary>
        /// <example>EUR</example>
        public string? CustomerCurrency { get; set; }

        #endregion

        #region (200) Address

        /// <summary>
        /// Addresses of the order. Optional.
        /// </summary>
        [JsonProperty(Order = 200)]
        public Collection<Address>? Addresses { get; set; }

        #endregion

        #region (400) Order Details

        /// <summary>
        /// Date and time the order was last changed.
        /// </summary>
        /// <example>2025-04-02T09:00:00+00:00</example>
        [JsonProperty(Order = 490)]
        public DateTimeOffset ChangedAt { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// User who last changed the order. Optional.
        /// </summary>
        /// <example>planner@example.com</example>
        [JsonProperty(Order = 491)]
        public string? ChangedBy { get; set; }

        /// <summary>
        /// User who created the order. Optional.
        /// </summary>
        /// <example>planner@example.com</example>
        [JsonProperty(Order = 492)]
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Date and time the order was created.
        /// </summary>
        /// <example>2025-04-01T08:30:00+00:00</example>
        [JsonProperty(Order = 421)]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// Date and time the order was released for production. Optional.
        /// </summary>
        /// <example>2025-04-03T07:00:00+00:00</example>
        [JsonProperty(Order = 422)]
        public DateTimeOffset? ReleasedAt { get; set; }

        /// <summary>
        /// Date and time the order was started, which is when the first feedback was received from production. Optional.
        /// </summary>
        /// <example>2025-04-04T06:15:00+00:00</example>
        [JsonProperty(Order = 423)]
        public DateTimeOffset? StartedAt { get; set; }

        /// <summary>
        /// Date and time the production system confirmed the order as completed. Optional.
        /// </summary>
        /// <example>2025-04-08T15:40:00+00:00</example>
        [JsonProperty(Order = 424)]
        public DateTimeOffset? CompletedAt { get; set; }

        /// <summary>
        /// Date and time the order was archived. Optional.
        /// </summary>
        /// <example>2025-04-20T10:00:00+00:00</example>
        [JsonProperty(Order = 425)]
        public DateTimeOffset? ArchivedAt { get; set; }

        /// <summary>
        /// Number of articles in the order. Optional.
        /// </summary>
        /// <example>12</example>
        [JsonProperty(Order = 420)]
        public int? QuantityOfArticles { get; set; }

        /// <summary>
        /// Number of parts in the order. Optional.
        /// </summary>
        /// <example>96</example>
        [JsonProperty(Order = 410)]
        public int? QuantityOfParts { get; set; }

        /// <summary>
        /// Number of parts planned in the order. Optional.
        /// </summary>
        /// <example>96</example>
        [JsonProperty(Order = 411)]
        public int? QuantityOfPartsPlanned { get; set; }

        /// <summary>
        /// <c>true</c> if offcuts are shipped with the order. Optional.
        /// </summary>
        /// <example>false</example>
        [JsonProperty(Order = 450)]
        public bool? IncludeOffcuts { get; set; }

        /// <summary>
        /// <c>true</c> if edgebands are included in the order. Optional.
        /// </summary>
        /// <example>true</example>
        [JsonProperty(Order = 451)]
        public bool? IncludeEdgeBands { get; set; }

        #endregion

        #region (600) Additional Data

        /// <summary>
        /// Additional data entries attached to the order. Optional.
        /// </summary>
        [JsonProperty(Order = 600)]
        public Collection<AdditionalDataEntity>? AdditionalData { get; set; }

        /// <summary>
        /// Additional properties configured in the application that are not part of the standard contract. Optional.
        /// </summary>
        /// <example>{ "externalSource": "ERP" }</example>
        [JsonExtensionData]
        [JsonProperty(Order = 602)]
        public IDictionary<string, object>? AdditionalProperties { get; set; }

        /// <summary>
        /// Notes about the order. Optional.
        /// </summary>
        /// <example>Deliver on pallet</example>
        [JsonProperty(Order = 601)]
        public string? Notes { get; set; }

        #endregion

        #region (700) Order items

        /// <summary>
        /// Items of the order, such as groups, positions, parts and components. Optional.
        /// </summary>
        [JsonProperty(Order = 700)]
        public Collection<OrderItems.Base>? Items { get; set; }

        #endregion
    }
}