using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace HomagConnect.OrderManager.Contracts.Customers;

/// <summary>
/// Request to create a new customer.
/// </summary>
public class CreateCustomerRequest
{
    /// <summary>
    /// Gets or sets the customer number. Optional. If omitted, the backend generates a unique customer number.
    /// </summary>
    public string? CustomerNumber { get; set; }

    /// <summary>
    /// Gets or sets the external customer number.
    /// </summary>
    public string? CustomerNumberExternal { get; set; }

    /// <summary>
    /// Gets or sets the customer name.
    /// </summary>
    public string? CustomerName { get; set; }

    /// <summary>
    /// Gets or sets the email address of the customer.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the telephone number of the customer.
    /// </summary>
    public string? TelephoneNumber { get; set; }

    /// <summary>
    /// Gets or sets additional notes for the customer.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the locale of the customer.
    /// </summary>
    public Locale Locale { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the customer has API access.
    /// </summary>
    public bool ApiAccess { get; set; }

    /// <summary>
    /// Gets or sets the collection of addresses associated with the customer.
    /// </summary>
    public Collection<Address>? Addresses { get; set; }

    /// <summary>
    /// Gets or sets the collection of additional data entities for the customer.
    /// </summary>
    public Collection<AdditionalDataEntity>? AdditionalData { get; set; }
}