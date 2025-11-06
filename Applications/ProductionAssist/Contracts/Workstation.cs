using HomagConnect.Base.Contracts.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.ProductionAssist.Contracts;

/// <summary>
/// Workstation
/// </summary>
public class Workstation
{
    /// <summary>
    /// Gets or sets the tapio machine ID
    /// </summary>
    [JsonProperty(Order = 20)]
    public string AssignedTapioMachineId { get; set; }

    /// <summary>
    /// Gets or sets the display name
    /// </summary>
    [JsonProperty(Order = 2)]
    public string DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the tapio machine ID
    /// </summary>
    [JsonProperty(Order = 3)]
    public WorkstationGroup Group { get; set; } = WorkstationGroup.None;

    /// <summary>
    /// Gets or sets the Workstation Id
    /// </summary>
    [JsonProperty(Order = 1)]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the tapio machine ID
    /// </summary>
    [JsonProperty(Order = 4)]
    public WorkstationType Type { get; set; } = WorkstationType.None;
}