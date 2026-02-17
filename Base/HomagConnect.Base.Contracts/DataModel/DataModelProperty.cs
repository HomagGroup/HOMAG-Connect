namespace HomagConnect.Base.Contracts.DataModel;

public class DataModelProperty
{
    /// <summary>
    /// Gets the id which can be used in the API and in the Project.xml file to reference the property.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets the display name in the culture set in the subscription.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets the description in the culture set in the subscription.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets the data type of the property.
    /// </summary>
    public Type DataType { get; set; }

    /// <summary>
    /// Gets the property type.
    /// </summary>
    public DataModelPropertyType PropertyType { get; set; } = DataModelPropertyType.Standard;
}