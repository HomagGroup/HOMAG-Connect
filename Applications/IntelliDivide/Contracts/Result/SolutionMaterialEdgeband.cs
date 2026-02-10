#nullable enable
using HomagConnect.Base.Contracts.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Describes the material used in the solution.
/// </summary>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class SolutionMaterialEdgeband 
{
    /// <summary>
    /// Gets or sets the material code.
    /// </summary>
    [JsonProperty(Order = 1)]
    public string MaterialCode
    {
        get;
        set
        {
            field = value.Trimmed();
        }
    } = string.Empty;

    /// <summary>
    /// Gets or sets the height.
    /// </summary>
    [JsonProperty(Order = 2)]
    public double Height { get; set; }

    /// <summary>
    /// Gets or sets the thickness.
    /// </summary>
    [JsonProperty(Order = 3)]
    public double Thickness { get; set; }

    /// <summary>
    /// Gets or sets the length of edge bands used.
    /// </summary>
    [JsonProperty(Order = 4)]
    public double Demand { get; set; }

    /// <summary>
    /// Gets or sets the total cost.
    /// </summary>
    [JsonProperty(Order = 5)]
    public double Costs { get; set; }

    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }
}