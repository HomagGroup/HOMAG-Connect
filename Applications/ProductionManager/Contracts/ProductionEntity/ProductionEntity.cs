using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.Serialization;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

using JsonSubTypes;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionEntity;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Production entity
/// </summary>
[JsonConverter(typeof(JsonSubtypes), nameof(Type))]
[JsonSubtypes.KnownSubType(typeof(ProductionEntityOrderItem), ProductionEntityType.OrderItem)]
[JsonSubtypes.KnownSubType(typeof(ProductionEntityProductionOrder), ProductionEntityType.ProductionOrder)]
[JsonSubtypes.KnownSubType(typeof(ProductionEntityAssemblyUnit), ProductionEntityType.AssemblyUnit)]
[JsonSubtypes.KnownSubType(typeof(ProductionEntityResource), ProductionEntityType.Resource)]
[DebuggerDisplay("Id={Id}, Number={ArticleNumber}")]
public class ProductionEntity
{
    #region (10) Article

    /// <summary>
    /// Gets or sets the type of the production entity.
    /// </summary>
    [JsonProperty(Order = 0)]
    public virtual ProductionEntityType Type { get; set; }

    #endregion

    #region (20) Production

    /// <summary>
    /// Gets or sets the id
    /// </summary>
    [JsonProperty(Order = 1)]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the article number.
    /// </summary>
    [JsonProperty(Order = 3)]
    public string? ArticleNumber { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    [JsonProperty(Order = 4)]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the group.
    /// </summary>
    [JsonProperty(Order = 3)]
    public string? ArticleGroup { get; set; }

    /// <summary>
    /// Gets or sets the procurement type.
    /// </summary>
    [JsonProperty(Order = 3)]
    public string? ProcurementType { get; set; }

    /// <summary>
    /// Gets or sets the length.
    /// </summary>
    [JsonProperty(Order = 14)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Length { get; set; }

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    [JsonProperty(Order = 15)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Width { get; set; }

    /// <summary>
    /// Barcode used to identify a production entity.
    /// </summary>
    [JsonProperty(Order = 21)]
    public string? Barcode { get; set; }

    /// <summary>
    /// Gets or sets the status of the production entity.
    /// </summary>
    [JsonProperty(Order = 22)]
    public ProductionEntityStatus ProductionStatus { get; set; } = ProductionEntityStatus.New;

    /// <summary>
    /// Gets or sets the quantity of the production entity.
    /// </summary>
    [JsonProperty(Order = 23)]
    public int Quantity { get; set; } = 1;

    #endregion

    #region (30) Production data

    /// <summary>
    /// Gets or sets the planned end date of the production entity.
    /// </summary>
    [JsonProperty(Order = 30)]
    public DateTimeOffset? StartDatePlanned { get; set; }

    /// <summary>
    /// Gets or sets the started at date of the production entity.
    /// </summary>
    [JsonProperty(Order = 31)]
    public DateTimeOffset? StartedAt { get; set; }

    /// <summary>Gets or sets the completed date planned</summary>
    [JsonProperty(Order = 32)]
    public DateTimeOffset? CompletionDatePlanned { get; set; }

    /// <summary>
    /// Gets or sets the completed at date of the production entity.
    /// </summary>
    [JsonProperty(Order = 33)]
    public DateTimeOffset? CompletedAt { get; set; }

    /// <summary>
    /// Gets or sets the planned delivery date of the production entity.
    /// </summary>
    [JsonProperty(Order = 34)]
    public DateTimeOffset? DeliveryDatePlanned { get; set; }

    #endregion

    #region (80) Additional data

    /// <summary>
    /// Gets or sets the notes of the production entity.
    /// </summary>
    [JsonProperty(Order = 80)]
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the additional data.
    /// </summary>
    [JsonProperty(Order = 81)]
    public Collection<AdditionalDataEntity>? AdditionalData { get; set; }
    
    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 90)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    #endregion

    /// <summary>
    /// Edge Thickness Front
    /// </summary>
    public decimal? EdgeThicknessFront { get; set; }

    /// <summary>
    /// Edge Thickness Back
    /// </summary>
    public decimal? EdgeThicknessBack { get; set; }

    /// <summary>
    /// Edge Thickness Left
    /// </summary>
    public decimal? EdgeThicknessLeft { get; set; }

    /// <summary>
    /// Edge Thickness Right
    /// </summary>
    public decimal? EdgeThicknessRight { get; set; }

    /// <summary>
    /// Surface Top
    /// </summary>
    public string? SurfaceTop { get; set; }

    /// <summary>
    /// Surface Bottom
    /// </summary>
    public string? SurfaceBottom { get; set; }

    /// <summary>
    /// CNC Program Name 3
    /// </summary>
    public string? CncProgramName3 { get; set; }

    /// <summary>
    /// LaminateTopGrain: optional
    /// </summary>
    public Grain? LaminateTopGrain { get; set; }

    /// <summary>
    /// LaminateBottomGrain: optional
    /// </summary>
    public Grain? LaminateBottomGrain { get; set; }

    /// <summary>
    /// Finish length
    /// </summary>
    public decimal? FinishLength { get; set; }

    /// <summary>
    /// Finish width
    /// </summary>
    public decimal? FinishWidth { get; set; }

    /// <summary>
    /// Cnc program name 1
    /// </summary>
    public string? CncProgramName1 { get; set; }

    /// <summary>
    /// Cnc program name 2
    /// </summary>
    public string? CncProgramName2 { get; set; }

    /// <summary>
    /// Label layout
    /// </summary>
    public string? LabelLayout { get; set; }

    /// <summary>
    /// Template
    /// </summary>
    public string? Template { get; set; }

    /// <summary>
    /// Laminate top
    /// </summary>
    public string? LaminateTop { get; set; }

    /// <summary>
    /// Laminate bottom
    /// </summary>
    public string? LaminateBottom { get; set; }

    /// <summary>
    /// Custom properties
    /// </summary>
    public IDictionary<string, string>? CustomProperties { get; set; }

    /// <summary>
    /// Grouping
    /// </summary>
    public string? Grouping { get; set; }

    /// <summary>
    /// 2. Cut size length
    /// </summary>
    public decimal? SecondCutLength { get; set; }

    /// <summary>
    /// 2. Cut size width
    /// </summary>
    public decimal? SecondCutWidth { get; set; }
}