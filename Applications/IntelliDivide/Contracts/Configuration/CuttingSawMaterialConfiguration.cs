using HomagConnect.IntelliDivide.Contracts.Common;

namespace HomagConnect.IntelliDivide.Contracts.Configuration;

/// <summary>
/// Describes the saw material configuration of a cutting machine.
/// </summary>
public class CuttingSawMaterialConfiguration : SawMaterialConfiguration
{
    /// <inheritdoc />
    public override OptimizationType OptimizationType { get; set; } = OptimizationType.Cutting;
}
