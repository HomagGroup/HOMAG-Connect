using System.Xml.Serialization;

namespace HomagConnect.DataExchange.Contracts
{
    /// <summary>
    /// Data exchange entity definition.
    /// </summary>
    public class Entity : ParamImageBase
    {
        /// <summary>
        /// Gets or sets entities.
        /// </summary>
        [XmlArray("entities")]
        [XmlArrayItem("entity")]
        public List<Entity> Entities { get; } = [];
    }
}
