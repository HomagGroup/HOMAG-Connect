namespace HomagConnect.Base.Contracts.Interfaces;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Defines the common properties of an application event.
/// Application events provide a stable event identifier, event key, timestamp, and optional custom payload values.
/// </summary>
public interface IAppEvent
{
    /// <summary>
    /// Gets or sets additional event-specific properties.
    /// Values not covered by the typed event contract can be provided through this dictionary.
    /// </summary>
    /// <example>{ "machine": "productionAssist Cutting", "state": "Started" }</example>
    public IDictionary<string, object> CustomProperties { get; set; }

    /// <summary>
    /// Gets the unique identifier of the event.
    /// </summary>
    /// <example>6f9619ff-8b86-d011-b42d-00cf4fc964ff</example>
    public Guid Id { get; }

    /// <summary>
    /// Gets the event key that identifies the event type.
    /// </summary>
    /// <example>optimization.started</example>
    public string Key { get; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    /// <example>2025-01-20T14:32:10+00:00</example>
    public DateTimeOffset Timestamp { get; }
}