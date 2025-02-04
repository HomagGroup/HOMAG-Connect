using System.Xml.Serialization;

namespace HomagConnect.DataExchange.Contracts
{
    public abstract class ParamBase
    {
        [XmlArray("properties")]
        [XmlArrayItem("param")]
        public List<Param> Properties { get; } = new List<Param>();
    }
}
