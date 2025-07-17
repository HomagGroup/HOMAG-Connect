using System.Runtime.Serialization;

using HomagConnect.Base.Contracts.Enumerations;

using JsonSubTypes;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Base class for board and offcut.
/// </summary>
[JsonConverter(typeof(JsonSubtypes), nameof(Type))]
[JsonSubtypes.KnownSubType(typeof(SolutionMaterialBoard), BoardTypeType.Board)]
[JsonSubtypes.KnownSubType(typeof(SolutionMaterialOffcut), BoardTypeType.Offcut)]
[JsonSubtypes.KnownSubType(typeof(SolutionMateriaTemplate), BoardTypeType.Template)]
public class SolutionMaterialBoardBase : IExtensibleDataObject
{
    /// <summary>
    /// Gets or sets the board type.
    /// </summary>
    [JsonProperty(Order = 0)]
    public virtual BoardTypeType Type { get; set; }

    /// <summary>
    /// Gets or sets the board code.
    /// </summary>
    [JsonProperty(Order = 2)]
    public string BoardCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total costs.
    /// </summary>
    [JsonProperty(Order = 7)]
    public double Costs { get; set; }

    /// <summary>
    /// Gets or sets the demand.
    /// </summary>
    [JsonProperty(Order = 6)]
    public int Demand { get; set; }

    /// <summary>
    /// Gets or sets the length.
    /// </summary>
    [JsonProperty(Order = 3)]
    public double Length { get; set; }

    /// <summary>
    /// Gets or sets the material code.
    /// </summary>
    [JsonProperty(Order = 1)]
    public string MaterialCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the thickness.
    /// </summary>
    [JsonProperty(Order = 5)]
    public double Thickness { get; set; }

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    [JsonProperty(Order = 4)]
    public double Width { get; set; }

    #region IExtensibleDataObject members

    /// <inheritdoc />
    [JsonProperty(Order = 99)]
    public ExtensionDataObject ExtensionData { get; set; }

    #endregion
}