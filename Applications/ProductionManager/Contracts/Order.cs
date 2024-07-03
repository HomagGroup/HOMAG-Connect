using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts
{
    /// <summary>
    /// Order data
    /// </summary>
    public class Order
    {
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
        public string OrderName { get; set; } = string.Empty;

        /// <summary>
        /// The name of the customer of this order
        /// </summary>
        [JsonProperty(Order = 120)]
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// The date the order was created at
        /// </summary>
        [JsonProperty(Order = 130)]
        public DateTimeOffset OrderDate { get; set; }

        /// <summary>
        /// Gets the planned delivery date of this order.
        /// </summary>
        [JsonProperty(Order = 140)]

        public DateTimeOffset? DeliveryDatePlanned { get; set; }

        /// <summary>
        /// Gets the status of the order.
        /// </summary>
        [JsonProperty(Order = 150)]
        public OrderStatus OrderStatus { get; set; }

        /// <summary>
        /// Gets the external system id of the order which can be used as a reference.
        /// </summary>
        [JsonProperty(Order = 160)]
        public string ExternalSystemId { get; set; } = string.Empty;

        #endregion

        #region Production

        #region Planning

        /// <summary>
        /// Gets or sets the planned start date of the order.
        /// </summary>
        [JsonProperty(Order = 200)]
        public DateTime? StartDatePlanned { get; set; }

        /// <summary>
        /// Gets or sets the planned completion date of the order.
        /// </summary>
        [JsonProperty(Order = 201)]
        public DateTime? CompletionDatePlanned { get; set; }

        #endregion

        #region Tracking

        /// <summary>
        /// Gets or sets the start date of the order.
        /// </summary>
        [JsonProperty(Order = 210)]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the completion date of the order.
        /// </summary>
        [JsonProperty(Order = 211)]
        public DateTime? CompletionDate { get; set; }

        #endregion

        #endregion

        #region

        /// <summary />
        [Obsolete("Use OrderName instead.", true)]
        [JsonProperty(Order = 999)]
        public string Name
        {
            get
            {
                return OrderName;
            }
            set
            {
                OrderName = value;
            }
        }

        /// <summary />
        [JsonProperty(Order = 999)]
        [Obsolete("Use DeliveryDatePlanned instead.", true)]
        public DateTimeOffset? DeliveryDate
        {
            get
            {
                return DeliveryDatePlanned;
            }
            set
            {
                DeliveryDatePlanned = value;
            }
        }

        /// <summary />
        [Obsolete("Use OrderStatus instead.", true)]
        [JsonProperty(Order = 999)]
        public OrderStatus State
        {
            get
            {
                return OrderStatus;
            }
            set
            {
                OrderStatus = value;
            }
        }

        #endregion
    }
}