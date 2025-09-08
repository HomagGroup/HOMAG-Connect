namespace HomagConnect.Base.Contracts.Interfaces;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Represents an interface for application events.
/// </summary>
public interface IAppEvent
{
    /// <summary>
    /// Gets the custom properties of the event.
    /// </summary>
    public IDictionary<string, object> CustomProperties { get; set; }

    /// <summary>
    /// Gets the unique identifier of the event.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Gets the key of the event.
    /// </summary>
    public string Key { get; }

    /// <summary>
    /// Gets the timestamp of the event.
    /// </summary>
    public DateTimeOffset Timestamp { get; }
}