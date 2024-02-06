using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace HomagConnect.IntelliDivide.Contracts.Common
{
    /// <summary>
    /// Import file
    /// </summary>
    [DebuggerDisplay("Name = {Name}")]
    public class ImportFile
    {
        /// <summary>
        /// Gets or sets the name which is used as reference.
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// File content as stream
        /// </summary>
        [Required]
        public Stream Stream { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="ImportFile" />.
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static async Task<ImportFile> CreateAsync(FileInfo fileInfo)
        {
            if (!fileInfo.Exists) throw new FileNotFoundException("Import file does not exist.", fileInfo.FullName);

            var memoryStream = new MemoryStream();

            using (var fileStream = fileInfo.OpenRead())
            {
                var bytes = new byte[fileStream.Length];
                _ = await fileStream.ReadAsync(bytes, 0, (int)fileStream.Length).ConfigureAwait(false);
                await memoryStream.WriteAsync(bytes, 0, (int)fileStream.Length).ConfigureAwait(false);
            }

            return new ImportFile
            {
                Name = fileInfo.Name,
                Stream = memoryStream,
            };
        }
    }
}