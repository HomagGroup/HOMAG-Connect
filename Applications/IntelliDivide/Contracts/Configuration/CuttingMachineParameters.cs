using HomagConnect.IntelliDivide.Contracts.Common;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Configuration;

/// <summary>
/// Contains the parameters of a cutting machine as shown in the sections of the intelliDivide "Machine parameters" dialog.
/// </summary>
/// <remarks>
/// A section is <c>null</c> when it is not available for the machine or the subscription, for example the book height section
/// for circular saws or the allocations section when materialManager does not support allocations.
/// </remarks>
/// <example>
/// {
///   "optimizationType": "Cutting",
///   "general": { "maxCuttingLength": 4300, "maxTravelProgramFence": 4300, "ripCutBladeThickness": 4.8, "crossCutBladeThickness": 4.8, "downloadOption": "Download" },
///   "offcuts": { "smallOffcutsEnabled": true, "smallOffcutPercentage": 30, "largeOffcutsEnabled": true, "largeOffcutMinimumLength": 2000, "largeOffcutMinimumWidth": 800, "largeOffcutMinimumArea": 1.6, "largeOffcutPercentage": 80 },
///   "trimCuts": { "trimFront": 15, "trimBack": 15, "trimLeft": 15, "trimRight": 15, "reducibleTrims": true },
///   "tensionTrims": { "useTensionTrims": false, "tensionTrimWidth": 30, "tensionTrimMinDistance": 150, "tensionTrimMinCutLength": 2000, "tensionTrimSlotDistance": 20, "tensionTrimMiddleBridge": 30 },
///   "bookHeight": { "maximumBookHeight": 95 },
///   "allocations": { "edgebandAllocations": false, "boardAllocations": true, "offcutAllocations": true }
/// }
/// </example>
public class CuttingMachineParameters : MachineParameters
{
    /// <inheritdoc />
    public override OptimizationType OptimizationType { get; set; } = OptimizationType.Cutting;

    /// <summary>
    /// The parameters of the "In general" section.
    /// </summary>
    [JsonProperty(Order = 3)]
    public CuttingGeneralParameters? General { get; set; }

    /// <summary>
    /// The parameters of the "Offcuts" section.
    /// </summary>
    [JsonProperty(Order = 4)]
    public CuttingOffcutParameters? Offcuts { get; set; }

    /// <summary>
    /// The parameters of the "Trim cuts" section.
    /// </summary>
    [JsonProperty(Order = 5)]
    public CuttingTrimCutParameters? TrimCuts { get; set; }

    /// <summary>
    /// The parameters of the "Tension trims" section. <c>null</c> when the user is not allowed to edit tension trims.
    /// </summary>
    [JsonProperty(Order = 6)]
    public CuttingTensionTrimParameters? TensionTrims { get; set; }

    /// <summary>
    /// The parameters of the "Book height" section. <c>null</c> for circular saws, TKS machines and batch size one saws which are not operated unmanned.
    /// </summary>
    [JsonProperty(Order = 7)]
    public CuttingBookHeightParameters? BookHeight { get; set; }

    /// <summary>
    /// The parameters of the "Allocations" section. <c>null</c> when materialManager supports neither board nor edgeband type allocations.
    /// </summary>
    [JsonProperty(Order = 8)]
    public CuttingAllocationParameters? Allocations { get; set; }
}
