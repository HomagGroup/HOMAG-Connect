using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Customers
{
    /// <summary>
    /// Customer of the company, including contact data, addresses and sales portal access.
    /// </summary>
    /// <example>
    /// { "id": "5a1c7e90-3b2d-4f68-8a14-9c0e1d2f3a4b", "customerNumber": "C-1001", "customerName": "Miller Kitchens", "email": "info@miller.example.com", "telephoneNumber": "+49 7443 12-0", "locale": "deDE", "apiAccess": false, "state": "Approved", "salesPortalAccess": ["buyer@miller.example.com"] }
    /// </example>
    public class Customer
    {
        /// <summary>
        /// Additional data entries attached to the customer. Optional.
        /// </summary>
        [JsonProperty(Order = 100)]
        public Collection<AdditionalDataEntity>? AdditionalData { get; set; }

        /// <summary>
        /// <c>true</c> if the customer is granted API access; otherwise <c>false</c>.
        /// </summary>
        [JsonProperty(Order = 8)]
        public bool ApiAccess { get; set; }

        /// <summary>
        /// Name of the customer. Optional.
        /// </summary>
        /// <example>Miller Kitchens</example>
        [JsonProperty(Order = 3)]
        [StringLength(100)]
        public string? CustomerName { get; set; }

        /// <summary>
        /// Name of the customer. Obsolete; use <see cref="CustomerName" /> instead.
        /// </summary>
        [Obsolete ("Use CustomerName instead.")]
        [JsonProperty(Order = 3)]
        [StringLength(100)]
        public string? Company { get; set; }

        /// <summary>
        /// Unique customer number.
        /// </summary>
        /// <example>C-1001</example>
        [JsonProperty(Order = 2)]
        [StringLength(100)]
        public string CustomerNumber { get; set; } = null!;

        /// <summary>
        /// Contact email address of the customer. Optional.
        /// </summary>
        /// <example>info@miller.example.com</example>
        [JsonProperty(Order = 4)]
        [StringLength(100)]
        public string? Email { get; set; }

        /// <summary>
        /// Contact telephone number of the customer. Optional.
        /// </summary>
        /// <example>+49 7443 12-0</example>
        [JsonProperty(Order = 5)]
        [StringLength(100)]
        public string? TelephoneNumber { get; set; }

        /// <summary>
        /// Unique identifier of the customer.
        /// </summary>
        /// <example>5a1c7e90-3b2d-4f68-8a14-9c0e1d2f3a4b</example>
        [Key]
        [JsonProperty(Order = 1)]
        public Guid Id { get; set; }

        /// <summary>
        /// Notes about the customer. Optional.
        /// </summary>
        [JsonProperty(Order = 9)]
        [StringLength(1000)]
        public string? Notes { get; set; }

        /// <summary>
        /// Language and region of the customer.
        /// </summary>
        /// <example>deDE</example>
        [JsonProperty(Order = 10)]
        public Locale Locale { get; set; }

        /// <summary>
        /// Addresses of the customer. Optional.
        /// </summary>
        [JsonProperty(Order = 11)]
        public Collection<Address>? Addresses { get; set; }

        /// <summary>
        /// Email addresses of the users who have sales portal access for the customer. Optional.
        /// </summary>
        /// <example>["buyer@miller.example.com"]</example>
        [JsonProperty(Order = 12)]
        public Collection<string>? SalesPortalAccess { get; set; }

        /// <summary>
        /// Current state of the customer.
        /// If not set, the state is considered as <see cref="State.Approved"/>.
        /// </summary>
        /// <example>Approved</example>
        [JsonProperty(Order = 6)]
        public State? State { get; set; }

        /// <summary>
        /// Customer number from an external system, for example an ERP system. Optional.
        /// </summary>
        /// <example>ERP-4711</example>
        [JsonProperty(Order = 7)]
        [StringLength(100)]
        public string? CustomerNumberExternal { get; set; }
    }
}