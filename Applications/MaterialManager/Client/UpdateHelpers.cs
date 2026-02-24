using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Extensions;
using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Client
{
    internal static class UpdateHelpers
    {
        public static (Uri Uri, string Json) PrepareUpdatePayload(string baseRoute, string codeName, string code, object update)
        {
            if (update == null)
            {
                throw new ArgumentNullException(nameof(update));
            }

            var url = $"{baseRoute}?{codeName}={Uri.EscapeDataString(code)}";
            var json = JsonConvert.SerializeObject(update, SerializerSettings.Default);

            return (new Uri(url, UriKind.Relative), json);
        }

        public static async Task<T> SendMultipartPatchAsync<T>(HttpClient client, Uri uri, string json, FileReference[] fileReferences, string jsonPartName = "update")
            where T : new()
        {
            if (fileReferences == null)
            {
                throw new ArgumentNullException(nameof(fileReferences));
            }

            var missingFile = fileReferences.FirstOrDefault(f => !f.FileInfo.Exists);
            if (missingFile != null)
            {
                throw new FileNotFoundException($"File '{missingFile.FileInfo.FullName}' was not found.");
            }

            var missingReference = fileReferences.FirstOrDefault(f => string.IsNullOrWhiteSpace(f.Reference));
            if (missingReference != null)
            {
                throw new ArgumentException($"Reference for file '{missingReference.FileInfo.FullName}' is missing.");
            }

            var request = new HttpRequestMessage(new HttpMethod("PATCH"), uri);

            using var httpContent = new MultipartFormDataContent();

            httpContent.Add(new StringContent(json), jsonPartName);

            foreach (var fileReference in fileReferences)
            {
                var fileStream = fileReference.FileInfo.OpenRead();
                HttpContent streamContent = new StreamContent(fileStream);
                httpContent.Add(streamContent, fileReference.Reference, fileReference.FileInfo.Name);
            }

            request.Content = httpContent;

            var response = await client.SendAsync(request).ConfigureAwait(false);

            await response.EnsureSuccessStatusCodeWithDetailsAsync(request).ConfigureAwait(false);

            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var responseObject = JsonConvert.DeserializeObject<T>(result, SerializerSettings.Default);

            return responseObject ?? new T();
        }
    }
}