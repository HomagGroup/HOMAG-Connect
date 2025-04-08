using System.Diagnostics;

namespace HomagConnect.Base.Contracts
{
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
        /// Gets or sets the file info.
        /// </summary>
        public FileInfo FileInfo { get; set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets file stream.
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