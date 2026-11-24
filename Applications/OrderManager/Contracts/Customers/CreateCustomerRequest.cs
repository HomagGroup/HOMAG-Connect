using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using System.Collections.ObjectModel;

using Newtonsoft.Json;

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
    [JsonProperty(Order = 1)]
    public string? CustomerNumber { get; set; }

    /// <summary>
    /// Customer number from an external system, for example an ERP system. Optional.
    /// </summary>
    /// <example>ERP-4711</example>
    [JsonProperty(Order = 2)]
    public string? CustomerNumberExternal { get; set; }

    /// <summary>
    /// Name of the customer. Optional.
    /// </summary>
    /// <example>Miller Kitchens</example>
    [JsonProperty(Order = 3)]
    public string? CustomerName { get; set; }

    /// <summary>
    /// Email address of the customer. Optional.
    /// </summary>
    /// <example>info@miller.example.com</example>
    [JsonProperty(Order = 4)]
    public string? Email { get; set; }

    /// <summary>
    /// Telephone number of the customer. Optional.
    /// </summary>
    /// <example>+49 7443 12-0</example>
    [JsonProperty(Order = 5)]
    public string? TelephoneNumber { get; set; }

    /// <summary>
    /// Additional notes about the customer. Optional.
    /// </summary>
    [JsonProperty(Order = 6)]
    public string? Notes { get; set; }

    /// <summary>
    /// Language and region of the customer.
    /// </summary>
    /// <example>deDE</example>
    [JsonProperty(Order = 7)]
    public Locale Locale { get; set; }

    /// <summary>
    /// <c>true</c> if the customer is granted API access; otherwise <c>false</c>.
    /// </summary>
    [JsonProperty(Order = 8)]
    public bool ApiAccess { get; set; }

    /// <summary>
    /// Addresses of the customer. Optional.
    /// </summary>
    [JsonProperty(Order = 9)]
    public Collection<Address>? Addresses { get; set; }

    /// <summary>
    /// Additional data entries attached to the customer. Optional.
    /// </summary>
    [JsonProperty(Order = 10)]
    public Collection<AdditionalDataEntity>? AdditionalData { get; set; }

    /// <summary>
    /// Email addresses of the users who are granted sales portal access for the customer. Empty by default.
    /// </summary>
    /// <example>["buyer@miller.example.com"]</example>
    [JsonProperty(Order = 11)]
    public ICollection<string> SalesPortalAccess { get; set; }
}