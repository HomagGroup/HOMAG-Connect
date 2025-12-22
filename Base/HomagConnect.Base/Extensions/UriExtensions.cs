using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
// ReSharper disable LocalizableElement

namespace HomagConnect.Base.Extensions
{
    /// <summary>
    /// Extensions for <see cref="Uri" />.
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Downloads a file from the given URI and saves it to the specified target file.
        /// </summary>
        /// <param name="uri">The absolute URI to download from.</param>
        public static async Task DownloadFileAsync(this Uri uri, FileInfo targetFileInfo)
        {
            if (!uri.IsAbsoluteUri)
            {
                throw new ArgumentException("URI must be absolute.", nameof(uri));
            }

            if (targetFileInfo == null)
            {
                throw new ArgumentNullException(nameof(targetFileInfo));
            }

            if (targetFileInfo.Directory == null)
            {
                throw new DirectoryNotFoundException($"Directory not found for target file: {targetFileInfo.FullName}");
            }

            if (!targetFileInfo.Directory.Exists)
            {
                targetFileInfo.Directory.Create();
            }

            using var httpClient = new HttpClient();
            var fileBytes = await httpClient.GetByteArrayAsync(uri).ConfigureAwait(false);

            // ReSharper disable once MethodHasAsyncOverload
            File.WriteAllBytes(targetFileInfo.FullName, fileBytes);
        }

        /// <summary>
        /// Downloads the content from the given URI and returns it as a readable stream.
        /// </summary>
        /// <param name="uri">The absolute URI to download from.</param>
        /// <returns>A readable stream containing the downloaded content.</returns>
        public static async Task<Stream> DownloadStreamAsync(this Uri uri)
        {
            if (!uri.IsAbsoluteUri)
            {
                throw new ArgumentException("URI must be absolute.", nameof(uri));
            }

            using var httpClient = new HttpClient();
            var stream = await httpClient.GetStreamAsync(uri).ConfigureAwait(false);
            // Copy to a MemoryStream so caller can dispose independently
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream).ConfigureAwait(false);
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}