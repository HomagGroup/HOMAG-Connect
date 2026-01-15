using System.Collections.Generic;

namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures.Roomle;

// NOTE: This is preliminary code and is subject to change

/// <summary>
/// Root container for a collection of Roomle <see cref="Material"/> entries.
/// </summary>
public class MaterialDefinitionsRoomle
{
    /// <summary>
    /// List of material entries returned by the Roomle API.
    /// </summary>
    public List<Material>? Materials { get; set; }
}