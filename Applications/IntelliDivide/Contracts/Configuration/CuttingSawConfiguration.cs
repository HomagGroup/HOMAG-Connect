using HomagConnect.IntelliDivide.Contracts.Common;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Configuration;

/// <summary>
/// Describes the saw configuration of a cutting machine.
/// </summary>
public class CuttingSawConfiguration : SawConfiguration
{
    /// <inheritdoc />
    public override OptimizationType OptimizationType { get; set; } = OptimizationType.Cutting;

    /// <summary>
    /// Gets or sets the additional parameters of a FlexTec (HPS) saw.
    /// </summary>
    [JsonProperty(Order = 3)]
    public FlexTecParameter FlexTecParameter { get; set; }
}
