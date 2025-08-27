using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;

using Newtonsoft.Json;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace HomagConnect.Base.Contracts.Events;

/// <summary>
/// Represents a base class for application events.
/// </summary>
public class AppEvent
{
    /// <summary>
    /// Creates a new instance of the <see cref="AppEvent" /> class.
    /// </summary>
    public AppEvent()
    {
        Id = Guid.NewGuid();
        Timestamp = DateTimeOffset.Now;

        if (GetType() != typeof(AppEvent))
        {
            var appEventAttribute = this.GetType().GetCustomAttributes(typeof(AppEventAttribute), false).OfType<AppEventAttribute>().FirstOrDefault();

            if (appEventAttribute == null)
            {
                throw new ArgumentNullException("AppEventAttribute missing on type " + GetType());
            }

            Provider = appEventAttribute.Provider;
            Key = appEventAttribute.Key;
        }
    }

    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 90)]
    [JsonExtensionData]
    public IDictionary<string, object> CustomProperties { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the event.
    /// </summary>
    [JsonProperty(Order = 0)]
    [Required]
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the current event instance is valid.
    /// </summary>
    [JsonIgnore]
    public bool IsValid
    {
        get
        {
            var context = new ValidationContext(this);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(this, context, results, true);

            return isValid;
        }
    }

    /// <summary>
    /// Get or sets the key of the event.
    /// </summary>
    [JsonProperty(Order = 4)]
    [Required]
    public string Key { get; private set; }

    /// <summary>
    /// Gets or sets the provider of the event.
    /// </summary>
    [JsonProperty(Order = 3)]
    [Required]
    public string Provider { get; private set; }

    /// <summary>
    /// Gets or sets the subscription ID of the event.
    /// </summary>
    [JsonProperty(Order = 2)]
    [Required]
    public Guid SubscriptionId { get; set; }

    /// <summary>
    /// Gets or sets the time offset of the event.
    /// </summary>
    [JsonProperty(Order = 1)]
    [Required]
    public DateTimeOffset Timestamp { get; set; }
}