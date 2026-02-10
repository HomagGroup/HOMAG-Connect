#nullable enable

using System.Collections.Generic;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Represents the material used in a solution.
/// </summary>
[JsonObject(ItemNullValueHandling = NullValueHandling.Include)]
public class SolutionMaterial : IContainsUnitSystemDependentProperties
{
    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Gets or sets the boards used in the solution.
    /// </summary>
    [JsonProperty(Order = 1)]
    public IReadOnlyCollection<SolutionMaterialBoard> Boards { get; set; } = new List<SolutionMaterialBoard>();

    /// <summary>
    /// Gets or sets the edgebands used in the solution.
    /// </summary>
    [JsonProperty(Order = 4)]
    public IReadOnlyCollection<SolutionMaterialEdgeband> Edgebands { get; set; } = new List<SolutionMaterialEdgeband>();

    /// <summary>
    /// Gets or sets the offcuts used in the solution.
    /// </summary>
    [JsonProperty(Order = 2)]
    public IReadOnlyCollection<SolutionMaterialOffcut> Offcuts { get; set; } = new List<SolutionMaterialOffcut>();

    /// <summary>
    /// Gets or sets the offcuts produced in the solution.
    /// </summary>
    [JsonProperty(Order = 3)]
    public IReadOnlyCollection<SolutionMaterialOffcutProduced> OffcutsProduced { get; set; } = new List<SolutionMaterialOffcutProduced>();

    /// <summary>
    /// Gets or sets the templates used in the solution.
    /// </summary>
    [JsonProperty(Order = 5)]
    public IReadOnlyCollection<SolutionMaterialTemplate>? Templates { get; set; }

    #region IContainsUnitSystemDependentProperties Members

    /// <inheritdoc />
    [JsonProperty(Order = 99)]
    public UnitSystem UnitSystem { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Solution" /> class.
    /// </summary>
    public SolutionMaterial() : this(UnitSystem.Metric) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Solution" /> class with the specified unit system.
    /// </summary>
    public SolutionMaterial(UnitSystem unitSystem)
    {
        UnitSystem = unitSystem;
    }

    #endregion
}