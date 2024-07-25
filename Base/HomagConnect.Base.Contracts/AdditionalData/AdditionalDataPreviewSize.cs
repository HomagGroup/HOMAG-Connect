using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts.AdditionalData;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Additional data preview size.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum AdditionalDataPreviewSize
{
    /// <summary />
    Unknown,

    /// <summary />
    Thumbnail,

    /// <summary />
    Small,

    /// <summary />
    Medium,

    /// <summary />
    Large
}