using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

using HomagConnect.Base.Contracts;

namespace HomagConnect.Base.Extensions;

/// <summary>
/// Extensions for <see cref="MultipartFormDataContent" />.
/// </summary>
public static class MultipartFormDataContentExtensions
{
    private static readonly Dictionary<string, string> _MimeTypes = new(StringComparer.InvariantCultureIgnoreCase)
    {
        { ".txt", "text/plain" },
        { ".html", "text/html" },
        { ".htm", "text/html" },
        { ".css", "text/css" },
        { ".js", "application/javascript" },
        { ".json", "application/json" },
        { ".xml", "application/xml" },
        { ".jpg", "image/jpeg" },
        { ".jpeg", "image/jpeg" },
        { ".png", "image/png" },
        { ".gif", "image/gif" },
        { ".bmp", "image/bmp" },
        { ".pdf", "application/pdf" },
    };

    /// <summary>
    /// Adds a file reference to the <see cref="MultipartFormDataContent" />.
    /// </summary>
    public static void Add(this MultipartFormDataContent httpContent, FileReference fileReference)
    {
        var extension = Path.GetExtension(fileReference.FileInfo.FullName);
        var fileStream = fileReference.FileInfo.OpenRead();

        if (ShouldBeCompressed(extension))
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

            streamContent.Headers.ContentType = ResolveMediaTypeHeaderValue(extension);

            httpContent.Add(streamContent, fileReference.Reference, fileReference.FileInfo.Name);
        }
    }

    private static MediaTypeHeaderValue ResolveMediaTypeHeaderValue(string extension)
    {
        return _MimeTypes.TryGetValue(extension, out var type) ? new MediaTypeHeaderValue(type) : new MediaTypeHeaderValue("application/octet-stream");
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
            ".3ds"
        };

        return extensionsToCompress.Any(e => string.Equals(e, extension, StringComparison.OrdinalIgnoreCase));
    }
}