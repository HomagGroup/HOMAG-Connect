using System.Xml.Serialization;

namespace HomagConnect.DataExchange.Contracts
{
    /// <summary>
    /// Data exchange project definition.
    /// </summary>
    [XmlRoot("project")]
    public class Project : ParamBase
    {
        /// <summary>
        /// The version of the content
        /// </summary>
        [XmlAttribute("contentVersion")]
        public string? ContentVersion { get; set; }

        /// <summary>
        /// Gets or sets the materials.
        /// </summary>
        [XmlArray("materials")]
        [XmlArrayItem("material")]
        public List<Material>? Materials { get; } = [];

        /// <summary>
        /// Gets or sets the orders.
        /// </summary>
        [XmlArray("orders")]
        [XmlArrayItem("order")]
        public List<Order>? Orders { get; } = [];
    }
}