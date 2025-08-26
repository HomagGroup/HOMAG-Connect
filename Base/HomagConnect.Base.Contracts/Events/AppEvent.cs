using Newtonsoft.Json;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace HomagConnect.Base.Contracts.Events;

/// <summary>
/// Represents a base class for application events.
/// </summary>
public class AppEvent
{
    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 90)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Get or sets the key of the event.
    /// </summary>
    [JsonProperty(Order = 4)]
    public virtual string Key { get; set; }

    /// <summary>
    /// Gets or sets the provider of the event.
    /// </summary>
    [JsonProperty(Order = 3)]
    public virtual string Provider { get; set; }

    /// <summary>
    /// Gets or sets the subscription ID of the event.
    /// </summary>
    [JsonProperty(Order = 2)]
    public Guid SubscriptionId { get; set; }

    /// <summary>
    /// Gets or sets the time offset of the event.
    /// </summary>
    [JsonProperty(Order = 1)]
    public DateTimeOffset TimeOffset { get; set; }
}