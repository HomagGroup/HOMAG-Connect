using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace HomagConnect.OrderManager.Contracts.Customers;

/// <summary>
/// Request to create a new customer.
/// </summary>
/// <example>
/// { "customerNumber": "C-1001", "customerName": "Miller Kitchens", "email": "info@miller.example.com", "telephoneNumber": "+49 7443 12-0", "locale": "deDE", "apiAccess": false, "salesPortalAccess": ["buyer@miller.example.com"] }
/// </example>
public class CreateCustomerRequest
{
    /// <summary>
    /// Unique customer number. Optional. If omitted, a unique customer number is generated.
    /// </summary>
    /// <example>C-1001</example>
    public string? CustomerNumber { get; set; }

    /// <summary>
    /// Customer number from an external system, for example an ERP system. Optional.
    /// </summary>
    /// <example>ERP-4711</example>
    public string? CustomerNumberExternal { get; set; }

    /// <summary>
    /// Name of the customer. Optional.
    /// </summary>
    /// <example>Miller Kitchens</example>
    public string? CustomerName { get; set; }

    /// <summary>
    /// Email address of the customer. Optional.
    /// </summary>
    /// <example>info@miller.example.com</example>
    public string? Email { get; set; }

    /// <summary>
    /// Telephone number of the customer. Optional.
    /// </summary>
    /// <example>+49 7443 12-0</example>
    public string? TelephoneNumber { get; set; }

    /// <summary>
    /// Additional notes about the customer. Optional.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Language and region of the customer.
    /// </summary>
    /// <example>deDE</example>
    public Locale Locale { get; set; }

    /// <summary>
    /// <c>true</c> if the customer is granted API access; otherwise <c>false</c>.
    /// </summary>
    public bool ApiAccess { get; set; }

    /// <summary>
    /// Addresses of the customer. Optional.
    /// </summary>
    public Collection<Address>? Addresses { get; set; }

    /// <summary>
    /// Additional data entries attached to the customer. Optional.
    /// </summary>
    public Collection<AdditionalDataEntity>? AdditionalData { get; set; }

    /// <summary>
    /// Email addresses of the users who are granted sales portal access for the customer. Empty by default.
    /// </summary>
    /// <example>["buyer@miller.example.com"]</example>
    public ICollection<string> SalesPortalAccess { get; set; } = new List<string>();
}