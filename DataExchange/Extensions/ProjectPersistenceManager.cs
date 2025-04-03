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

            return Load(projectXmlFileInfo.FullName, migrateToLatestVersion);
        }

        /// <summary>
        /// Load project from project.zip archive.
        /// </summary>
        public static Project Load(ZipArchive projectZipArchive, bool migrateToLatestVersion = true)
        {
            var extractDirectory = new DirectoryInfo(Path.Combine(Path.GetTempPath(), "Project", Guid.NewGuid().ToString()));

            return Load(projectZipArchive, extractDirectory, migrateToLatestVersion);
        }

        /// <summary>
        /// Load project from project.zip archive.
        /// </summary>
        public static Project Load(ZipArchive projectZipArchive, DirectoryInfo extractDirectory, bool migrateToLatestVersion = true)
        {
            projectZipArchive.ExtractToDirectory(extractDirectory.FullName);

            var projectXml = extractDirectory.EnumerateFiles(_ProjectXmlFileName, SearchOption.AllDirectories).FirstOrDefault();

            if (projectXml == null)
            {
                throw new FileNotFoundException("File not found in archive.", _ProjectXmlFileName);
            }

            return Load(projectXml.FullName, migrateToLatestVersion);
        }

        /// <summary>
        /// Load project from project.xml file.
        /// </summary>
        public static Project Load(string projectXmlPath, bool migrateToLatestVersion = true)
        {
            using var s = new FileStream(projectXmlPath, FileMode.Open, FileAccess.Read, FileShare.Read);

            return Load(s, migrateToLatestVersion);
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
        /// Load project from project.xml string.
        /// </summary>
        public static Project LoadFromString(string text, bool migrateToLatestVersion = true)
        {
            var r = new StringReader(text);
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
        public static void SaveToZipArchive(this Project project, FileInfo fileInfo, DirectoryInfo projectFilesDirectory)
        {
            using var memoryStream = new MemoryStream();
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                var projectXml = archive.CreateEntry(_ProjectXmlFileName);

                using (var entryStream = projectXml.Open())
                {
                    SaveToXmlStream(project, entryStream);
                }

                var projectFiles = projectFilesDirectory.EnumerateFiles("*.*", SearchOption.AllDirectories).Where(f => f.Name != _ProjectXmlFileName);

                foreach (var projectFile in projectFiles)
                {
                    var key = projectFile.FullName.Replace(projectFilesDirectory.FullName, "").Replace('\\', '/').Trim('/');

                    var entryStream = archive.CreateEntry(key);

                    using var fileStream = new FileStream(projectFile.FullName, FileMode.Open);
                    using var es = entryStream.Open();
                    fileStream.CopyTo(es);
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
        public static void SaveToZipArchive(this Project project, FileInfo fileInfo, Dictionary<string, string>? projectFiles)
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
                        if (!File.Exists(projectFile.Value))
                        {
                            throw new FileNotFoundException("File not found", projectFile.Value);
                        }

                        var entryStream = archive.CreateEntry(projectFile.Key);

                        using var fileStream = new FileStream(projectFile.Value, FileMode.Open);
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