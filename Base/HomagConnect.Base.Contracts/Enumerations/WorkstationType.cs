using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts.Enumerations;

/// <summary>
/// The type of the workstation.
/// </summary>
[ResourceManager(typeof(WorkstationTypeDisplayNames))]
[JsonConverter(typeof(TolerantEnumConverter))]
public enum WorkstationType
{
    /// <summary>
    /// The workstation is an Assembly one
    /// </summary>
    Assembly,

    /// <summary>
    /// The workstation is a Boards one
    /// </summary>
    Boards,

    /// <summary>
    /// The workstation is a CNC one
    /// </summary>
    CNC,

    /// <summary>
    /// The workstation is a Cutting one
    /// </summary>
    Cutting,

    /// <summary>
    /// The workstation is a Drilling one
    /// </summary>
    Drilling,

    /// <summary>
    /// The workstation is an Edgebanding one
    /// </summary>
    Edgebanding,

    /// <summary>
    /// The workstation is a FeedbackAssembly one
    /// </summary>
    FeedbackAssembly,

    /// <summary>
    /// The workstation is a FeedbackAutomaticSorting one
    /// </summary>
    FeedbackAutomaticSorting,

    /// <summary>
    /// The workstation is a FeedbackCNC one
    /// </summary>
    FeedbackCNC,

    /// <summary>
    /// The workstation is a FeedbackCutting one
    /// </summary>
    FeedbackCutting,

    /// <summary>
    /// The workstation is a FeedbackDrilling one
    /// </summary>
    FeedbackDrilling,

    /// <summary>
    /// The workstation is a FeedbackEdgebanding one
    /// </summary>
    FeedbackEdgebanding,

    /// <summary>
    /// The workstation is a FeedbackInsertFittings one
    /// </summary>
    FeedbackInsertFittings,

    /// <summary>
    /// The workstation is a FeedbackLaminating one
    /// </summary>
    FeedbackLaminating,

    /// <summary>
    /// The workstation is a FeedbackMoulding one
    /// </summary>
    FeedbackMoulding,

    /// <summary>
    /// The workstation is a FeedbackPacking one
    /// </summary>
    FeedbackPacking,

    /// <summary>
    /// The workstation is a FeedbackPreassembly one
    /// </summary>
    FeedbackPreassembly,

    /// <summary>
    /// The workstation is a FeedbackRollerCoating one
    /// </summary>
    FeedbackRollerCoating,

    /// <summary>
    /// The workstation is a FeedbackSanding one
    /// </summary>
    FeedbackSanding,

    /// <summary>
    /// The workstation is a FeedbackShipping one
    /// </summary>
    FeedbackShipping,

    /// <summary>
    /// The workstation is a FeedbackSorting one
    /// </summary>
    FeedbackSorting,

    /// <summary>
    /// The workstation is a FeedbackSprayCoating one
    /// </summary>
    FeedbackSprayCoating,

    /// <summary>
    /// The workstation is a Nesting one
    /// </summary>
    Nesting,

    /// <summary>
    /// The workstation is a Preassembly one
    /// </summary>
    Preassembly,

    /// <summary>
    /// The workstation is a Rework one
    /// </summary>
    Rework,

    /// <summary>
    /// The workstation is a Sanding one
    /// </summary>
    Sanding,

    /// <summary>
    /// The workstation is a Shipping one
    /// </summary>
    Shipping,

    /// <summary>
    /// The workstation is a Sorting before assembly one
    /// </summary>
    Sorting,

    /// <summary>
    /// The workstation is a SortingAfterCutting one
    /// </summary>
    SortingAfterCutting,

    /// <summary>
    /// The workstation is a Storage one
    /// </summary>
    Storage
}
