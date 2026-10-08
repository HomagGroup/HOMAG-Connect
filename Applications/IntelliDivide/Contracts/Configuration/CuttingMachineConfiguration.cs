using System.Collections.Generic;

using HomagConnect.IntelliDivide.Contracts.Common;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Configuration;

/// <summary>
/// Describes the machine configuration of a cutting machine.
/// </summary>
public class CuttingMachineConfiguration : MachineConfiguration
{
    /// <inheritdoc />
    public override OptimizationType OptimizationType { get; set; } = OptimizationType.Cutting;

    /// <summary>
    /// Gets or sets the program fences of the saw.
    /// </summary>
    [JsonProperty(Order = 3)]
    public IList<ProgramFence> ProgramFences { get; set; } = new List<ProgramFence>();
}
