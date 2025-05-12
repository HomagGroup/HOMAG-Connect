using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

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
        public static async Task DownloadFileAsync(this Uri uri, FileInfo targetFileInfo)
        {
            if (!uri.IsAbsoluteUri)
            {
                throw new ArgumentException(@"URI must be absolute.", nameof(uri));
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

            var httpClient = new HttpClient();

            var fileBytes = await httpClient.GetByteArrayAsync(uri);

            // ReSharper disable once MethodHasAsyncOverload .Net Standard 2.0
            File.WriteAllBytes(targetFileInfo.FullName, fileBytes);
        }
    }
}