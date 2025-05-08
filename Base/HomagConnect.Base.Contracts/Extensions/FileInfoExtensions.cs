using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.Base.Contracts.Extensions
{
    /// <summary>
    /// Extensions for <see cref="FileInfo" />.
    /// </summary>
    public static class FileInfoExtensions
    {
        /// <summary>
        /// Gets the file extension of a file.
        /// </summary>
        public static string GetExtension(this FileInfo fileInfo)
        {
            return Path.GetExtension(fileInfo.FullName);
        }

        /// <summary>
        /// Gets the file name without extension of a file.
        /// </summary>
        public static string GetFileNameWithoutExtension(this FileInfo fileInfo)
        {
            return Path.GetFileNameWithoutExtension(fileInfo.Name);
        }

        /// <summary>
        /// Gets the mimetype of a file based on its extension.
        /// </summary>
        public static string GetMimeType(this FileInfo fileInfo)
        {
            return MimeTypes.GetMimeType(fileInfo);
        }

        /// <summary>
        /// Reads the content of a file.
        /// </summary>
        public static string ReadFileContent(this FileInfo fileInfo)
        {
            using var reader = fileInfo.OpenText();

            return reader.ReadToEnd();
        }
    }
}