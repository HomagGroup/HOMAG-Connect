namespace HomagConnect.Base.Contracts.Attributes;

/// <summary>
/// Represents an attribute used to associate a specific event with a provider and key.
/// </summary>
public class AppEventAttribute : Attribute
{
    /// <summary>
    /// Creates a new instance of the <see cref="AppEventAttribute" /> class.
    /// </summary>
    public AppEventAttribute(string provider, string key)
    {
        Provider = provider;
        Key = key;
    }

    /// <summary>
    /// Gets or sets the key of the event.
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// Gets or sets the name of the service provider.
    /// </summary>
    public string Provider { get; set; }
}