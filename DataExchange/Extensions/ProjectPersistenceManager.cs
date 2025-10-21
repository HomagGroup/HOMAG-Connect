using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.DataExchange.Contracts;
using HomagConnect.DataExchange.Extensions.Wrapper;

namespace HomagConnect.DataExchange.Extensions
{
    using System;
    using System.IO;

    /// <summary>
    /// Provides methods to load and save project data.
    /// </summary>
    public static class ProjectPersistenceManager
    {
        private const string _ProjectXmlFileName = "project.xml";
        private const string _ProjectZipFileName = "project.zip";

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

            var xmlReader = new StreamReader(projectXmlStream, Encoding.UTF8);
            var xmlString = xmlReader.ReadToEnd();

            if (xmlString.Contains("HomagGroup.ProductionManager.Core.Logic.Import"))
            {
                // Remove declarations which are no longer valid.

                xmlString = Regex.Replace(xmlString, @"\s*xsi:schemaLocation\s*=\s*""[^""]*""", string.Empty);
                xmlString = Regex.Replace(xmlString, @"\s*xsi:noNamespaceSchemaLocation\s*=\s*""[^""]*""", string.Empty);
                xmlString = Regex.Replace(xmlString, @"\s+xmlns=""[^""]*""", string.Empty);
                xmlString = Regex.Replace(xmlString, @"\s+xmlns:xsi=""[^""]*""", string.Empty);
            }

            var project = LoadFromString(xmlString, migrateToLatestVersion);

            return migrateToLatestVersion ? project.MigrateToLatestVersion() : project;
        }

        /// <summary>
        /// Load project from project.zip archive.
        /// </summary>
        public static (Project Project, FileReference[] ProjectFiles) Load(ZipArchive projectZipArchive, DirectoryInfo projectDirectory, bool migrateToLatestVersion = true)
        {
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
            var projectFiles = new List<FileReference>();
            var projectBaseDirectory = projectXmlFile.Directory.FullName;

            project.TrimAdditionalDataReferences();

            foreach (var projectFile in projectDirectory.EnumerateFiles("*.*", SearchOption.AllDirectories).Where(f => f.Name != _ProjectXmlFileName))
            {
                // TODO: Add only files that are referenced in the project

                var reference = TrimAdditionalDataReference(projectFile.FullName.Replace(projectBaseDirectory, ""));

                var fileReference = new FileReference(reference, projectFile);

                projectFiles.Add(fileReference);
            }

            return (project, projectFiles.ToArray());
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
        public static FileInfo SaveToZipArchive(this Project project, FileReference[]? projectFiles)
        {
            var fileInfo = new FileInfo(Path.Combine(Path.GetTempPath(), nameof(ProjectPersistenceManager), Guid.NewGuid().ToString(), _ProjectZipFileName));

            if (fileInfo.Directory == null)
            {
                throw new InvalidOperationException("Project zip file directory is null");
            }

            if (!fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }

            project.SaveToZipArchive(fileInfo, projectFiles);

            return fileInfo;
        }

        /// <summary>
        /// Save project to project.xml file.
        /// </summary>
        public static void SaveToZipArchive(this Project project, FileInfo fileInfo, FileReference[]? projectFiles)
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
                        var projectFileInfo = projectFile.FileInfo;

                        if (!projectFileInfo.Exists)
                        {
                            throw new FileNotFoundException("File not found", projectFile.FileInfo.FullName);
                        }

                        var entryStream = archive.CreateEntry(projectFile.Reference);

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
            using var w = new StreamWriter(stream, Encoding.UTF8);
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

        #region Private Members

        /// <summary>
        /// Trims the additional data references in the project.
        /// </summary>
        public static FileReference Trim(this FileReference fileReference)
        {
            return new FileReference(TrimAdditionalDataReference(fileReference.Reference), fileReference.FileInfo);
        }

        /// <summary>
        /// Trims the additional data references in the project.
        /// </summary>
        private static void TrimAdditionalDataReferences(this Project project)
        {
            if (project.Orders != null)
            {
                foreach (var projectOrder in project.Orders)
                {
                    TrimAdditionalDataReferences(projectOrder);
                }
            }
        }

        private static void TrimAdditionalDataReferences(List<Image> images)
        {
            foreach (var projectOrderImage in images)
            {
                var imageWrapper = new ImageWrapper(projectOrderImage);

                imageWrapper.ImageLinkPicture = TrimAdditionalDataReference(imageWrapper.ImageLinkPicture);
                imageWrapper.ImageLinkBinary = TrimAdditionalDataReference(imageWrapper.ImageLinkBinary);
            }
        }

        /// <summary>
        /// Checks if the additional data entity reference equals the file reference.
        /// </summary>
        public static bool ReferenceEquals(this AdditionalDataEntity additionalDataEntity, FileReference fileReference)
        {
            var additionalDataEntityReference = TrimAdditionalDataReference(additionalDataEntity.DownloadUri);
            var fileReferenceReference = fileReference.Trim().Reference;

            if (additionalDataEntityReference == null)
            {
                return false;
            }

            return string.Equals(additionalDataEntityReference.OriginalString, fileReferenceReference, StringComparison.OrdinalIgnoreCase);
        }

        private static Uri? TrimAdditionalDataReference(Uri? reference)
        {
            if (reference == null)
            {
                return null;
            }

            if (reference.IsAbsoluteUri)
            {
                return reference;
            }

            var originalString = TrimAdditionalDataReference(reference.OriginalString);

            return new Uri(originalString, UriKind.Relative);
        }

        private static string TrimAdditionalDataReference(string reference)
        {
            reference = reference.TrimStart('/');
            reference = reference.TrimStart('\\');
            reference = reference.Replace("/", "\\");

            return reference;
        }

        private static void TrimAdditionalDataReferences(Order order)
        {
            TrimAdditionalDataReferences(order.Images);

            foreach (var projectOrderEntity in order.Entities)
            {
                TrimAdditionalDataReferences(projectOrderEntity);
            }
        }

        private static void TrimAdditionalDataReferences(Entity entity)
        {
            TrimAdditionalDataReferences(entity.Images);

            foreach (var e in entity.Entities)
            {
                TrimAdditionalDataReferences(e);
            }
        }

        #endregion
    }
}