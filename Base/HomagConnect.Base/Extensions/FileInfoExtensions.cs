using System;
using System.IO;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Extensions;

using Newtonsoft.Json;

namespace HomagConnect.Base.Extensions;

/// <summary>
/// Extensions for <see cref="FileInfo" />.
/// </summary>
public static class FileInfoExtensions
{
    /// <summary>
    /// Deserializes the content of a file into an object of type <typeparamref name="T" />.
    /// </summary>
    public static T? DeserializeContent<T>(this FileInfo fileInfo)
    {
        if (!fileInfo.Exists)
        {
            throw new FileNotFoundException($"File {fileInfo.FullName} not found.");
        }

        var fileContent = fileInfo.ReadFileContent();

        if (string.IsNullOrWhiteSpace(fileContent))
        {
            throw new InvalidOperationException($"File {fileInfo.FullName} is empty.");
        }

        var deserializedObject = JsonConvert.DeserializeObject<T>(fileContent, SerializerSettings.Default);

        return deserializedObject ?? default;
    }
}