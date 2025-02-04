using System.Xml.Serialization;

namespace HomagConnect.Base.Contracts
{
    public class Entity : ParamImageBase
    {
        [XmlArray("entities")]
        [XmlArrayItem("entity")]
        public List<Entity> Entities { get; } = new List<Entity>();
    }
}
