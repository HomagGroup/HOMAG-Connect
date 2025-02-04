using System.Text;
using System.Xml.Serialization;

namespace HomagConnect.DataExchange.Contracts
{
    [XmlRoot("project")]
    public class Project : ParamBase
    {
        #region Load/Save

        public static Project Load(string path)
        {
            using (var s = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return Load(s);
            }
        }

        public static Project Load(Stream stream)
        {
            stream.Position = 0;
            var r = new StreamReader(stream, Encoding.UTF8);
            var ser = new XmlSerializer(typeof(Project));
            return (Project) ser.Deserialize(r);
        }

        public static Project LoadFromString(string text)
        {
            var r = new StringReader(text);
            var ser = new XmlSerializer(typeof(Project));
            return (Project) ser.Deserialize(r);
        }

        public void Save(string path)
        {
            using (var s = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read))
            {
                Save(s);
            }
        }

        public void Save(Stream stream)
        {
            var w = new StreamWriter(stream, Encoding.UTF8);
            var ser = new XmlSerializer(typeof(Project));
            ser.Serialize(w, this);
        }

        public string SaveToString()
        {
            using (var w = new StringWriter())
            {
                var ser = new XmlSerializer(typeof(Project));
                ser.Serialize(w, this);
                w.Flush();
                return w.ToString();
            }
        }

        #endregion

        /// <summary>
        /// The version of the content
        /// </summary>
        [XmlAttribute("contentVersion")]
        public string ContentVersion { get; set; }

        [XmlArray("orders")]
        [XmlArrayItem("order")]
        public List<Order> Orders { get; } = new List<Order>();

        [XmlArray("materials")]
        [XmlArrayItem("material")]
        public List<Material> Materials { get; } = new List<Material>();
    }
}
