using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts.AdditionalData;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Gets or sets the additional data type.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
[ResourceManager(typeof(AdditionalDataTypeDisplayNames))]
public enum AdditionalDataType
{
    /// <summary />
    Unknown,

    /// <summary />
    Image,

    /// <summary />
    Texture,

    /// <summary />
    [Display(Description = "Surface texture")]
    SurfaceTexture,

    /// <summary />
    CNCProgram,

    /// <summary />
    ThreeD,

    /// <summary />
    Pdf,

    /// <summary />
    Zip
}