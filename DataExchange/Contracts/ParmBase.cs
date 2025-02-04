using System.Xml.Serialization;

namespace HomagConnect.DataExchange.Contracts
{
    /// <summary>
    /// Data exchange param base definition.
    /// </summary>
    public abstract class ParamBase
    {
        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        [XmlArray("properties")]
        [XmlArrayItem("param")]
        public List<Param> Properties { get; } = new List<Param>();
    }
}
