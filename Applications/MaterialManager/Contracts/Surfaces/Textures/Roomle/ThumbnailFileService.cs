#if NET10_0_OR_GREATER

using System;
using System.Buffers.Binary;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures.Roomle
{

    public static class ThumbnaMaterialDefinitionRoomleExtensions
    {
        private const int ThumbnailWidth = 1600;
        private const int ThumbnailHeight = 900;

        private static readonly HttpClient DefaultHttpClient = new();
        private static readonly Dictionary<string, FileInfo> ThumbnailFiles = new(StringComparer.OrdinalIgnoreCase);

        public static async Task<Stream?> CreateThumbnailStreamAsync(
            this MaterialDefinitionRoomle materialDefinition,
            HttpClient? httpClient = null,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(materialDefinition);

            try
            {
                var thumbnailBytes = await CreateThumbnailBytesAsync(materialDefinition, httpClient ?? DefaultHttpClient, cancellationToken);
                return thumbnailBytes == null
                    ? null
                    : new MemoryStream(thumbnailBytes, writable: false);
            }
            catch (Exception exception) when (exception is HttpRequestException or TaskCanceledException or IOException or InvalidOperationException)
            {
                Debug.WriteLine($"Roomle thumbnail stream could not be created: {exception.Message}");
                return null;
            }
        }

        public static async Task<FileInfo?> GetOrCreateThumbnailFileAsync(
            this MaterialDefinitionRoomle materialDefinition,
            HttpClient? httpClient = null,
            DirectoryInfo? workingDirectoryInfo = null,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(materialDefinition);

            var thumbnailHttpClient = httpClient ?? DefaultHttpClient;
            var thumbnailDirectoryInfo = workingDirectoryInfo ?? new DirectoryInfo(Environment.CurrentDirectory);

            var materialId = materialDefinition.Material?.Id;
            if (string.IsNullOrWhiteSpace(materialId))
            {
                return null;
            }

            var cacheKey = CreateThumbnailCacheKey(thumbnailDirectoryInfo, materialId);
            if (ThumbnailFiles.TryGetValue(cacheKey, out var cachedThumbnailFile) && cachedThumbnailFile.Exists)
            {
                return cachedThumbnailFile;
            }

            try
            {
                var thumbnailFileInfo = await GetOrCreateThumbnailFileCoreAsync(materialDefinition, thumbnailHttpClient, thumbnailDirectoryInfo, materialId, cancellationToken);
                if (thumbnailFileInfo == null)
                {
                    return null;
                }

                ThumbnailFiles[cacheKey] = thumbnailFileInfo;
                return thumbnailFileInfo;
            }
            catch (Exception exception) when (exception is HttpRequestException or TaskCanceledException or IOException or InvalidOperationException)
            {
                Debug.WriteLine($"Roomle thumbnail for material {materialId} could not be created: {exception.Message}");
                return null;
            }
        }

        private static async Task<FileInfo?> GetOrCreateThumbnailFileCoreAsync(
            MaterialDefinitionRoomle materialDefinition,
            HttpClient httpClient,
            DirectoryInfo workingDirectoryInfo,
            string materialId,
            CancellationToken cancellationToken)
        {
            var thumbnailFileInfo = CreateRoomleThumbnailFile(workingDirectoryInfo, materialId);
            if (!thumbnailFileInfo.Exists)
            {
                var thumbnailBytes = await CreateThumbnailBytesAsync(materialDefinition, httpClient, cancellationToken);
                if (thumbnailBytes == null)
                {
                    return null;
                }

                thumbnailFileInfo.Directory?.Create();
                await File.WriteAllBytesAsync(thumbnailFileInfo.FullName, thumbnailBytes, cancellationToken);
                thumbnailFileInfo.Refresh();
            }

            return thumbnailFileInfo;
        }

        private static async Task<byte[]?> CreateThumbnailBytesAsync(MaterialDefinitionRoomle materialDefinition, HttpClient httpClient, CancellationToken cancellationToken)
        {
            if (materialDefinition.Material?.Thumbnail != null
                && Uri.TryCreate(materialDefinition.Material.Thumbnail, UriKind.Absolute, out var thumbnailUri))
            {
                var thumbnailBytes = await httpClient.GetByteArrayAsync(thumbnailUri, cancellationToken);
                return thumbnailBytes;
            }

            return TryGetMaterialColor(materialDefinition, out var color)
                ? CreateSolidColorPng(color, ThumbnailWidth, ThumbnailHeight)
                : null;
        }

        private static FileInfo CreateRoomleThumbnailFile(DirectoryInfo workingDirectoryInfo, string materialId)
        {
            var fileName = string.Join("_", materialId.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries));
            return new FileInfo(Path.Combine(workingDirectoryInfo.FullName, "roomle-thumbnails", $"{fileName}_{ThumbnailWidth}x{ThumbnailHeight}.png"));
        }

        private static string CreateThumbnailCacheKey(DirectoryInfo workingDirectoryInfo, string materialId)
        {
            return Path.Combine(workingDirectoryInfo.FullName, materialId.Trim());
        }

        private static bool TryGetMaterialColor(MaterialDefinitionRoomle materialDefinition, out RgbColor color)
        {
            var shading = materialDefinition.Material?.Shading;

            return TryGetRgbColor(shading?.Basecolor, out color)
                || TryGetRgbColor(shading?.AttenuationColor, out color)
                || TryGetRgbColor(shading?.EmissiveColor, out color);
        }

        private static bool TryGetRgbColor(object? value, out RgbColor color)
        {
            color = default;

            switch (value)
            {
                case null:
                    return false;
                case string text:
                    return TryGetRgbColor(text, out color);
                case ColorRgb rgbColor:
                    return TryGetRgbColor(rgbColor, out color);
                case JToken jsonToken:
                    return TryGetRgbColor(jsonToken, out color);
                case IEnumerable values:
                    return TryGetRgbColor(values, out color);
                default:
                    return false;
            }
        }

        private static bool TryGetRgbColor(ColorRgb value, out RgbColor color)
        {
            return TryGetRgbColor(
                new List<double> { value.R, value.G, value.B },
                out color);
        }

        private static bool TryGetRgbColor(string value, out RgbColor color)
        {
            color = default;

            var hexColor = value.Trim().TrimStart('#');
            if (hexColor.Length < 6)
            {
                return false;
            }

            return byte.TryParse(hexColor[..2], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var red)
                && byte.TryParse(hexColor[2..4], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var green)
                && byte.TryParse(hexColor[4..6], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var blue)
                && SetColor(red, green, blue, out color);
        }

        private static bool TryGetRgbColor(JToken value, out RgbColor color)
        {
            color = default;

            if (value.Type == JTokenType.String)
            {
                return TryGetRgbColor(value.Value<string>() ?? string.Empty, out color);
            }

            if (value.Type == JTokenType.Object)
            {
                return TryGetRgbColor((JObject)value, out color);
            }

            if (value.Type != JTokenType.Array)
            {
                return false;
            }

            var colorComponents = value.Children()
                .Where(static component => component.Type is JTokenType.Integer or JTokenType.Float)
                .Select(static component => component.Value<double>())
                .ToList();

            return TryGetRgbColor(colorComponents, out color);
        }

        private static bool TryGetRgbColor(JObject value, out RgbColor color)
        {
            color = default;

            return TryGetColorComponent(value, nameof(ColorRgb.R), out var red)
                && TryGetColorComponent(value, nameof(ColorRgb.G), out var green)
                && TryGetColorComponent(value, nameof(ColorRgb.B), out var blue)
                && TryGetRgbColor(new List<double> { red, green, blue }, out color);
        }

        private static bool TryGetColorComponent(JObject value, string propertyName, out double component)
        {
            component = default;

            if (!value.TryGetValue(propertyName, StringComparison.OrdinalIgnoreCase, out var token)
                || token.Type is not JTokenType.Integer and not JTokenType.Float)
            {
                return false;
            }

            component = token.Value<double>();
            return true;
        }

        private static bool TryGetRgbColor(IEnumerable values, out RgbColor color)
        {
            color = default;

            var colorComponents = new List<double>();

            foreach (var value in values)
            {
                if (value == null)
                {
                    continue;
                }

                try
                {
                    colorComponents.Add(Convert.ToDouble(value, CultureInfo.InvariantCulture));
                }
                catch (Exception exception) when (exception is FormatException or InvalidCastException or OverflowException)
                {
                    return false;
                }
            }

            return TryGetRgbColor(colorComponents, out color);
        }

        private static bool TryGetRgbColor(IReadOnlyList<double> colorComponents, out RgbColor color)
        {
            color = default;

            if (colorComponents.Count < 3)
            {
                return false;
            }

            var factor = colorComponents.Take(3).All(static component => component is >= 0 and <= 1)
                ? 255
                : 1;

            return SetColor(
                ToColorByte(colorComponents[0], factor),
                ToColorByte(colorComponents[1], factor),
                ToColorByte(colorComponents[2], factor),
                out color);
        }

        private static bool SetColor(byte red, byte green, byte blue, out RgbColor color)
        {
            color = new RgbColor(red, green, blue);
            return true;
        }

        private static byte ToColorByte(double value, int factor)
        {
            return (byte)Math.Clamp(Math.Round(value * factor), byte.MinValue, byte.MaxValue);
        }

        private static byte[] CreateSolidColorPng(RgbColor color, int width, int height)
        {
            using var stream = new MemoryStream();

            stream.Write([137, 80, 78, 71, 13, 10, 26, 10]);
            WritePngChunk(stream, "IHDR", CreatePngHeader(width, height));
            WritePngChunk(stream, "IDAT", CompressPngImageData(CreatePngImageData(color, width, height)));
            WritePngChunk(stream, "IEND", []);

            return stream.ToArray();
        }

        private static byte[] CreatePngHeader(int width, int height)
        {
            var header = new byte[13];

            BinaryPrimitives.WriteInt32BigEndian(header.AsSpan(0, 4), width);
            BinaryPrimitives.WriteInt32BigEndian(header.AsSpan(4, 4), height);
            header[8] = 8;
            header[9] = 2;

            return header;
        }

        private static byte[] CreatePngImageData(RgbColor color, int width, int height)
        {
            var imageData = new byte[height * ((width * 3) + 1)];
            var offset = 0;

            for (var row = 0; row < height; row++)
            {
                imageData[offset++] = 0;

                for (var column = 0; column < width; column++)
                {
                    imageData[offset++] = color.Red;
                    imageData[offset++] = color.Green;
                    imageData[offset++] = color.Blue;
                }
            }

            return imageData;
        }

        private static byte[] CompressPngImageData(byte[] imageData)
        {
            using var compressedStream = new MemoryStream();

            using (var zlibStream = new ZLibStream(compressedStream, CompressionLevel.SmallestSize, leaveOpen: true))
            {
                zlibStream.Write(imageData);
            }

            return compressedStream.ToArray();
        }

        private static void WritePngChunk(Stream stream, string chunkType, byte[] data)
        {
            Span<byte> length = stackalloc byte[4];
            BinaryPrimitives.WriteInt32BigEndian(length, data.Length);
            stream.Write(length);

            var chunkTypeBytes = Encoding.ASCII.GetBytes(chunkType);

            stream.Write(chunkTypeBytes);
            stream.Write(data);

            Span<byte> crc = stackalloc byte[4];
            BinaryPrimitives.WriteUInt32BigEndian(crc, CalculateCrc32(chunkTypeBytes, data));
            stream.Write(crc);
        }

        private static uint CalculateCrc32(byte[] chunkType, byte[] data)
        {
            var crc = 0xffffffffu;

            foreach (var value in chunkType.Concat(data))
            {
                crc ^= value;

                for (var bit = 0; bit < 8; bit++)
                {
                    crc = (crc & 1) == 1
                        ? (crc >> 1) ^ 0xedb88320u
                        : crc >> 1;
                }
            }

            return ~crc;
        }

        private readonly record struct RgbColor(byte Red, byte Green, byte Blue);
    }
}
#endif