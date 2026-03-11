namespace HomagConnect.Base.Contracts;

/// <summary>
/// Represents a lightweight reference to another object using an identifier and display name.
/// </summary>
/// <typeparam name="T">The type of the referenced identifier.</typeparam>
public class NamedReference<T>
{
    /// <summary>
    /// Creates a new instance of the <see cref="NamedReference{T}" /> class.
    /// </summary>
    public NamedReference() { }

    /// <summary>
    /// Creates a new instance of the <see cref="NamedReference{T}" /> class.
    /// </summary>
    public NamedReference(T id, string name)
    {
        Id = id;
        Name = name;
    }

    /// <summary>
    /// Gets or sets the identifier of the referenced object.
    /// </summary>
    /// <example>42</example>
    public T Id { get; set; } = default!;

    /// <summary>
    /// Gets or sets the display name of the referenced object.
    /// </summary>
    /// <example>Default</example>
    public string? Name { get; set; }
}