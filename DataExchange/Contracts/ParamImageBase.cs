using System.Xml.Serialization;

namespace HomagConnect.DataExchange.Contracts
{
    /// <summary>
    /// Data exchange param image base definition.
    /// </summary>
    public abstract class ParamImageBase : ParamBase
    {
        /// <summary>
        /// Gets or sets the images.
        /// </summary>
        [XmlArray("images")]
        [XmlArrayItem("image")]
        public List<Image> Images { get; } = [];
    }
}
