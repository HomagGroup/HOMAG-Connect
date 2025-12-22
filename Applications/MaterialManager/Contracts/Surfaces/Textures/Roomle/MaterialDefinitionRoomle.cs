using System;
using System.IO;
using System.Text;
using HomagConnect.Base;
using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures.Roomle;

// NOTE: This is preliminary code and is subject to change

/// <summary>
/// Root wrapper for a single Roomle material payload in JSON.
/// Provides helpers for serialization, deserialization, and file/stream operations.
/// </summary>
public class MaterialDefinitionRoomle
{
    #region Properties
    /// <summary>
    /// The contained material definition with textures, shading, and metadata.
    /// </summary>
    public Material? Material { get; set; }
    #endregion

    #region Deserialization
    /// <summary>
    /// Deserialize a <see cref="MaterialDefinitionRoomle"/> from a file.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="fileInfo"/> is null.</exception>
    /// <exception cref="FileNotFoundException">Thrown when the file does not exist.</exception>
    public static MaterialDefinitionRoomle? Deserialize(FileInfo fileInfo)
    {
        if (fileInfo == null) throw new ArgumentNullException(nameof(fileInfo));
        if (!fileInfo.Exists)
        {
            throw new FileNotFoundException($"File not found: {fileInfo.FullName}");
        }
        var json = File.ReadAllText(fileInfo.FullName);
        return FromJson(json);
    }

    /// <summary>
    /// Deserialize a <see cref="MaterialDefinitionRoomle"/> from a JSON string.
    /// </summary>
    public static MaterialDefinitionRoomle? FromJson(string json)
    {
        if (string.IsNullOrWhiteSpace(json)) return null;
        return JsonConvert.DeserializeObject<MaterialDefinitionRoomle>(json, SerializerSettings.Default);
    }

    /// <summary>
    /// Try to deserialize a <see cref="MaterialDefinitionRoomle"/> from a JSON string.
    /// </summary>
    public static bool TryFromJson(string json, out MaterialDefinitionRoomle? result)
    {
        result = null;
        if (string.IsNullOrWhiteSpace(json)) return false;
        try
        {
            result = FromJson(json);
            return result != null;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region Serialization
    /// <summary>
    /// Serialize this instance to a JSON string.
    /// </summary>
    public string ToJson()
    {
        return JsonConvert.SerializeObject(this, SerializerSettings.Default);
    }

    /// <summary>
    /// Serialize this instance to the specified file path (overwrites if exists).
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="fileInfo"/> is null.</exception>
    public void Save(FileInfo fileInfo)
    {
        if (fileInfo == null) throw new ArgumentNullException(nameof(fileInfo));
        var json = ToJson();
        File.WriteAllText(fileInfo.FullName, json);
    }

    /// <summary>
    /// Serializes this instance to a UTF-8 memory stream.
    /// </summary>
    /// <returns>A memory stream containing the JSON representation of this instance.</returns>
    public Stream ToStream()
    {
        var json = ToJson();
        var bytes = Encoding.UTF8.GetBytes(json);
        var stream = new MemoryStream(bytes);
        stream.Position = 0;
        return stream;
    }
    #endregion
}