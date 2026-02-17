#nullable enable

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Describes the material used in the solution.
/// </summary>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionMaterialEdgeband : IHasMaterialCode
{
    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Gets or sets the total cost.
    /// </summary>
    [JsonProperty(Order = 5)]
    [Range(0, double.MaxValue)]
    public double? Costs { get; set; }

    /// <summary>
    /// Gets or sets the length of edge bands used.
    /// </summary>
    [JsonProperty(Order = 4)]
    [Range(0, double.MaxValue)]
    public double Demand { get; set; }

    /// <summary>
    /// Gets or sets the height.
    /// </summary>
    [JsonProperty(Order = 2)]
    [Range(0, double.MaxValue)]
    public double Height { get; set; }

    /// <summary>
    /// Gets or sets the thickness.
    /// </summary>
    [JsonProperty(Order = 3)]
    [Range(0, double.MaxValue)]
    public double Thickness { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 1)]
    public string MaterialCode
    {
        get;
        set
        {
            field = value.Trimmed();
        }
    } = string.Empty;
}