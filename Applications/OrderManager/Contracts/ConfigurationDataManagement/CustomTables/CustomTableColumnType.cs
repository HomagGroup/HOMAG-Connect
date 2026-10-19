using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.ConfigurationDataManagement.CustomTables;

/// <summary>
/// The data type of a custom table column.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum CustomTableColumnType
{
    /// <summary>
    /// A free text value.
    /// </summary>
    Text,

    /// <summary>
    /// A numeric value.
    /// </summary>
    Number,

    /// <summary>
    /// A boolean value.
    /// </summary>
    Boolean,

    /// <summary>
    /// A date or date-time value.
    /// </summary>
    Date,

    /// <summary>
    /// A value restricted to a predefined list of values.
    /// </summary>
    ValueList,

    /// <summary>
    /// A reference to a row in another custom table.
    /// </summary>
    Reference,

    /// <summary>
    /// A reference to an existing image.
    /// </summary>
    Image,

    /// <summary>
    /// A reference to an existing attachment.
    /// </summary>
    Attachment,

    /// <summary>
    /// A reference to an existing 3D model.
    /// </summary>
    Model3D
}
