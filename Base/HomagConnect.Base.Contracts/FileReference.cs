using System.Diagnostics;

namespace HomagConnect.Base.Contracts
{
    /// <summary>
    /// Represents a file together with the logical reference name used when submitting or processing the file.
    /// </summary>
    [DebuggerDisplay("{Reference}: {FileInfo}")]
    public class FileReference
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileReference" /> class.
        /// </summary>
        public FileReference(string reference, string filePath) : this(reference, new FileInfo(filePath)) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileReference" /> class.
        /// </summary>
        public FileReference(string reference, FileInfo fileInfo)
        {
            Reference = reference;
            FileInfo = fileInfo;
        }

        /// <summary>
        /// Gets or sets the file metadata and local file system path.
        /// </summary>
        /// <example>C:\Data\Project.zip</example>
        public FileInfo FileInfo { get; set; }

        /// <summary>
        /// Gets or sets the logical reference name used for the file in the request or response payload.
        /// </summary>
        /// <example>Project.zip</example>
        public string Reference { get; set; }

        /// <summary>
        /// Opens the referenced file and returns its content as a memory stream.
        /// </summary>
        public async Task<MemoryStream> GetStream()
        {
            if (!FileInfo.Exists)
            {
                throw new FileNotFoundException("File does not exist.", FileInfo.FullName);
            }

            using var fileStream = new FileStream(FileInfo.FullName, FileMode.Open, FileAccess.Read);
            var memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}