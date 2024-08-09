namespace HomagConnect.Base.Contracts;

/// <summary>
/// Represents a reference to another object.
/// </summary>
/// <typeparam name="T"></typeparam>
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
    /// Gets or sets the id of the referenced object.
    /// </summary>
    public T Id { get; set; } = default!;

    /// <summary>
    /// Gets or sets the name of the referenced object.
    /// </summary>
    public string? Name { get; set; }
}