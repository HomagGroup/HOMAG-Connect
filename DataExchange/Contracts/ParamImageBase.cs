using System.Xml.Serialization;

namespace HomagConnect.Base.Contracts
{
    public abstract class ParamImageBase : ParamBase
    {
        [XmlArray("images")]
        [XmlArrayItem("image")]
        public List<Image> Images { get; } = new List<Image>();
    }
}
