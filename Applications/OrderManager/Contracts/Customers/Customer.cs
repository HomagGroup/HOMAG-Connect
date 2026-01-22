using System.Collections.ObjectModel;

using HomagConnect.Base.Contracts.AdditionalData;

namespace HomagConnect.OrderManager.Contracts.Customers
{
    /// <summary>
    /// Customer data
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets or sets the additional data.
        /// </summary>
        public Collection<AdditionalDataEntity>? AdditionalData { get; set; }

        /// <summary>
        /// Gets or sets the API access status.
        /// </summary>
        public bool ApiAccess { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        public string? Company { get; set; }

        /// <summary>
        /// Gets or sets the address of the customer.
        /// </summary>
        public string CustomerNumber { get; set; } = null!;

        /// <summary>
        /// Gets or sets the contact email of the customer.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the customer.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the contact phone number of the customer.
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Gets or sets the contact telephone number of the customer.
        /// </summary>
        public string? TelephoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the customer language and region
        /// </summary>
        public Locale Locale { get; set; }
    }
}