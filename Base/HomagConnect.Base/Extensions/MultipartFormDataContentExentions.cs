using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Extensions;

namespace HomagConnect.Base.Extensions;

/// <summary>
/// Extensions for <see cref="MultipartFormDataContent" />.
/// </summary>
public static class MultipartFormDataContentExtensions
{
    /// <summary>
    /// Adds a file reference to the <see cref="MultipartFormDataContent" />.
    /// </summary>
    public static void Add(this MultipartFormDataContent httpContent, FileReference fileReference)
    {
        var fileStream = fileReference.FileInfo.OpenRead();

        if (ShouldBeCompressed(fileReference.FileInfo.GetExtension()))
        {
            var compressedStream = new MemoryStream();

            using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Compress, true))
            {
                fileStream.CopyTo(gzipStream);
            }

            compressedStream.Position = 0;

            HttpContent streamContent = new StreamContent(compressedStream);

            streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            streamContent.Headers.ContentEncoding.Add("gzip");

            httpContent.Add(streamContent, fileReference.Reference, fileReference.FileInfo.Name);
        }
        else
        {
            HttpContent streamContent = new StreamContent(fileStream);

            streamContent.Headers.ContentType = new MediaTypeHeaderValue(MimeTypes.GetMimeType(fileReference.FileInfo));

            httpContent.Add(streamContent, fileReference.Reference, fileReference.FileInfo.Name);
        }
    }


    private static bool ShouldBeCompressed(string extension)
    {
        var extensionsToCompress = new[]
        {
            ".txt",
            ".json",
            ".xml",
            ".bmp",
            ".pdf",
            ".mpr",
            ".mprx",
            ".3ds",
            ".glb"
        };

        return extensionsToCompress.Any(e => string.Equals(e, extension, StringComparison.OrdinalIgnoreCase));
    }
}