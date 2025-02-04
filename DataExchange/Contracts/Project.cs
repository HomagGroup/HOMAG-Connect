using System.IO.Compression;
using System.Text;
using System.Xml.Serialization;

namespace HomagConnect.DataExchange.Contracts
{
    /// <summary>
    /// Data exchange project definition.
    /// </summary>
    [XmlRoot("project")]
    public class Project : ParamBase
    {
        #region Load

        /// <summary>
        /// Load project from project.xml file.
        /// </summary>
        public static Project Load(FileInfo projectXml)
        {
            if (projectXml == null) throw new ArgumentNullException(nameof(projectXml));
            if (!projectXml.Exists) throw new FileNotFoundException("File not found", projectXml.FullName);

            return Load(projectXml.FullName);
        }

        /// <summary>
        /// Load project from project.zip archive.
        /// </summary>
        public static Project Load(ZipArchive projectZip)
        {
            var projectXmlFileName = "project.xml";
            var extractDirectory = new DirectoryInfo(Path.Combine(Path.GetTempPath(), "Project", Guid.NewGuid().ToString()));

            projectZip.ExtractToDirectory(extractDirectory.FullName);

            var projectXml = extractDirectory.EnumerateFiles(projectXmlFileName, SearchOption.AllDirectories).FirstOrDefault();

            if (projectXml == null)
            {
                throw new FileNotFoundException("File not found in archive.", projectXmlFileName);
            }

            return Load(projectXml.FullName);
        }

        /// <summary>
        /// Load project from project.xml file.
        /// </summary>
        public static Project Load(string projectXml)
        {
            using var s = new FileStream(projectXml, FileMode.Open, FileAccess.Read, FileShare.Read);

            return Load(s);
        }

        /// <summary>
        /// Load project from project.xml stream.
        /// </summary>
        public static Project Load(Stream projectXml)
        {
            projectXml.Position = 0;

            var r = new StreamReader(projectXml, Encoding.UTF8);
            var ser = new XmlSerializer(typeof(Project));
            var o = ser.Deserialize(r);

            return o as Project ?? throw new InvalidOperationException();
        }

        /// <summary>
        /// Load project from project.xml string.
        /// </summary>
        public static Project LoadFromString(string text)
        {
            var r = new StringReader(text);
            var ser = new XmlSerializer(typeof(Project));
            var o = ser.Deserialize(r);

            return o as Project ?? throw new InvalidOperationException();
        }

        #endregion

        #region Save

        /// <summary>
        /// Save project to project.xml file.
        /// </summary>
        public void Save(string projectXml)
        {
            using var s = new FileStream(projectXml, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);

            Save(s);
        }

        /// <summary>
        /// Save project to project.xml stream.
        /// </summary>
        public void Save(Stream projectXml)
        {
            var w = new StreamWriter(projectXml, Encoding.UTF8);
            var ser = new XmlSerializer(typeof(Project));
            ser.Serialize(w, this);
        }

        /// <summary>
        /// Save project to project.xml string.
        /// </summary>
        public string SaveToString()
        {
            using var w = new StringWriter();
            var ser = new XmlSerializer(typeof(Project));
            ser.Serialize(w, this);
            w.Flush();
            return w.ToString();
        }

        #endregion

        /// <summary>
        /// The version of the content
        /// </summary>
        [XmlAttribute("contentVersion")]
        public string? ContentVersion { get; set; }

        /// <summary>
        /// Gets or sets the orders.
        /// </summary>
        [XmlArray("orders")]
        [XmlArrayItem("order")]
        public List<Order>? Orders { get; } = [];

        /// <summary>
        /// Gets or sets the materials.
        /// </summary>
        [XmlArray("materials")]
        [XmlArrayItem("material")]
        public List<Material>? Materials { get; } = [];
    }
}