using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Configuration;

/// <summary>
/// Contains the parameters of the "Allocations" section of a cutting machine. Allocations are created automatically in
/// materialManager when the optimization is transferred.
/// </summary>
/// <remarks>
/// The section is available when materialManager supports board or edgeband type allocations for the subscription.
/// </remarks>
/// <example>
/// { "edgebandAllocations": false, "boardAllocations": true, "offcutAllocations": true }
/// </example>
public class CuttingAllocationParameters
{
    /// <summary>
    /// Indicates whether edgeband allocations are created.
    /// Available when materialManager supports edgeband type allocations. Defaults to <c>false</c>.
    /// </summary>
    /// <example>false</example>
    [JsonProperty(Order = 1)]
    public bool? EdgebandAllocations { get; set; }

    /// <summary>
    /// Indicates whether board allocations are created.
    /// Available when materialManager supports board type allocations. Always <c>true</c> and read-only for productionAssist machines;
    /// otherwise defaults to <c>false</c>.
    /// </summary>
    /// <example>true</example>
    [JsonProperty(Order = 2)]
    public bool? BoardAllocations { get; set; }

    /// <summary>
    /// Indicates whether offcut allocations are created.
    /// Available when materialManager supports board type allocations. Always <c>true</c> and read-only for productionAssist machines;
    /// otherwise defaults to <c>false</c>.
    /// </summary>
    /// <example>true</example>
    [JsonProperty(Order = 3)]
    public bool? OffcutAllocations { get; set; }
}
