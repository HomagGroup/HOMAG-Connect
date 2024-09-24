namespace HomagConnect.Base.DataModel;

/// <summary>
/// Defines the type of the data model property.
/// </summary>
public enum DataModelPropertyType
{
    /// <summary>
    /// The property is part of the standard data model.
    /// </summary>
    Standard,

    /// <summary>
    /// The property was added to the data model of the subscription
    /// </summary>
    Additional
}