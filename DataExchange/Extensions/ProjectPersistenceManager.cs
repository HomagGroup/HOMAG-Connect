using System.IO.Compression;
using System.Text;
using System.Xml.Serialization;

using HomagConnect.DataExchange.Contracts;

namespace HomagConnect.DataExchange.Extensions
{
    /// <summary>
    /// Provides methods to load and save project data.
    /// </summary>
    public static class ProjectPersistenceManager
    {
        private const string _ProjectXmlFileName = "project.xml";

        #region Load

        /// <summary>
        /// Load project from project.xml file.
        /// </summary>
        public static Project Load(FileInfo projectXmlFileInfo, bool migrateToLatestVersion = true)
        {
            if (projectXmlFileInfo == null) throw new ArgumentNullException(nameof(projectXmlFileInfo));
            if (!projectXmlFileInfo.Exists) throw new FileNotFoundException("File not found", projectXmlFileInfo.FullName);

            return Load(projectXmlFileInfo.OpenRead(), migrateToLatestVersion);
        }

        /// <summary>
        /// Load project from project.xml file.
        /// </summary>
        public static Project Load(string projectXmlPath, bool migrateToLatestVersion = true)
        {
            return Load(new FileInfo(projectXmlPath), migrateToLatestVersion);
        }

        /// <summary>
        /// Load project from project.xml stream.
        /// </summary>
        public static Project Load(Stream projectXmlStream, bool migrateToLatestVersion = true)
        {
            projectXmlStream.Position = 0;

            var r = new StreamReader(projectXmlStream, Encoding.UTF8);
            var ser = new XmlSerializer(typeof(Project));
            var o = ser.Deserialize(r);

            if (o is Project project)
            {
                return migrateToLatestVersion ? project.MigrateToLatestVersion() : project;
            }

            throw new InvalidOperationException();
        }

        /// <summary>
        /// Load project from project.zip archive.
        /// </summary>
        public static (Project Project, Dictionary<string, FileInfo>? ProjectFiles) Load(ZipArchive projectZipArchive, bool migrateToLatestVersion = true)
        {
            var projectDirectory = new DirectoryInfo(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()));

            projectZipArchive.ExtractToDirectory(projectDirectory.FullName);

            var projectXmlFile = projectDirectory.EnumerateFiles(_ProjectXmlFileName, SearchOption.AllDirectories).FirstOrDefault();

            if (projectXmlFile == null)
            {
                throw new FileNotFoundException("Project XML file not found in archive", _ProjectXmlFileName);
            }

            if (projectXmlFile.Directory == null)
            {
                throw new InvalidOperationException("Project XML file directory is null");
            }

            var project = Load(projectXmlFile, migrateToLatestVersion);

            var projectFiles = projectDirectory.EnumerateFiles("*.*", SearchOption.AllDirectories)
                .Where(f => f.Name != _ProjectXmlFileName)
                .ToDictionary(f => f.FullName.Replace(projectXmlFile.Directory.FullName, "").Replace('\\', '/').Trim('/'), f => f);

            return (project, projectFiles);
        }

        /// <summary>
        /// Load project from project.xml string.
        /// </summary>
        public static Project LoadFromString(string projectXml, bool migrateToLatestVersion = true)
        {
            var r = new StringReader(projectXml);
            var ser = new XmlSerializer(typeof(Project));
            var o = ser.Deserialize(r);

            if (o is Project project)
            {
                return migrateToLatestVersion ? project.MigrateToLatestVersion() : project;
            }

            throw new InvalidOperationException();
        }

        #endregion

        #region Save

        /// <summary>
        /// Save project to project.xml file.
        /// </summary>
        public static void SaveToXmlFile(this Project project, FileInfo fileInfo)
        {
            using var s = new FileStream(fileInfo.FullName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);

            SaveToXmlStream(project, s);
        }

        /// <summary>
        /// Save project to project.xml file.
        /// </summary>
        public static void SaveToZipArchive(this Project project, FileInfo fileInfo, Dictionary<string, FileInfo>? projectFiles)
        {
            using var memoryStream = new MemoryStream();
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                var projectXml = archive.CreateEntry(_ProjectXmlFileName);

                using (var entryStream = projectXml.Open())
                {
                    SaveToXmlStream(project, entryStream);
                }

                if (projectFiles != null)
                {
                    foreach (var projectFile in projectFiles)
                    {
                        var projectFileInfo = projectFile.Value;

                        if (!projectFileInfo.Exists)
                        {
                            throw new FileNotFoundException("File not found", projectFile.Value.FullName);
                        }

                        var entryStream = archive.CreateEntry(projectFile.Key);

                        using var fileStream = projectFileInfo.OpenRead();
                        using var es = entryStream.Open();
                        fileStream.CopyTo(es);
                    }
                }
            }

            using (var fileStream = new FileStream(fileInfo.FullName, FileMode.Create))
            {
                memoryStream.Seek(0, SeekOrigin.Begin);
                memoryStream.CopyTo(fileStream);
            }
        }

        /// <summary>
        /// Save project to project.xml file.
        /// </summary>
        public static void SaveToXmlFile(this Project project, string path)
        {
            using var s = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);

            SaveToXmlStream(project, s);
        }

        /// <summary>
        /// Save project to project.xml stream.
        /// </summary>
        public static void SaveToXmlStream(this Project project, Stream stream)
        {
            var w = new StreamWriter(stream, Encoding.UTF8);
            var ser = new XmlSerializer(typeof(Project));
            ser.Serialize(w, project);
        }

        /// <summary>
        /// Save project to project.xml string.
        /// </summary>
        public static string SaveToString(this Project project)
        {
            using var w = new StringWriter();
            var ser = new XmlSerializer(typeof(Project));
            ser.Serialize(w, project);
            w.Flush();
            return w.ToString();
        }

        #endregion
    }
}