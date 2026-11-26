using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Interfaces;

using JsonSubTypes;

using Newtonsoft.Json;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Base class of all items of an order. The concrete item kind is identified by the <c>type</c> property.
/// </summary>
/// <example>
/// { "type": "Position", "id": "1", "notes": "Check measurements on site" }
/// </example>
[JsonConverter(typeof(JsonSubtypes), nameof(Type))]
[JsonSubtypes.KnownSubType(typeof(Group), Type.Group)]
[JsonSubtypes.KnownSubType(typeof(Position), Type.Position)]
[JsonSubtypes.KnownSubType(typeof(Component), Type.Component)]
[JsonSubtypes.KnownSubType(typeof(Part), Type.Part)]
[JsonSubtypes.KnownSubType(typeof(Resource), Type.Resource)]
[JsonSubtypes.KnownSubType(typeof(Price), Type.Price)]
[JsonSubtypes.KnownSubType(typeof(Configuration), Type.Configuration)]
[JsonSubtypes.KnownSubType(typeof(ConfigurationGroup), Type.ConfigurationGroup)]
[JsonSubtypes.KnownSubType(typeof(ConfigurationPosition), Type.ConfigurationPosition)]
[JsonSubtypes.KnownSubType(typeof(ErrorInfo), Type.ErrorInfo)]
[JsonSubtypes.KnownSubType(typeof(Board), Type.Board)]
[JsonSubtypes.KnownSubType(typeof(EdgeBand), Type.EdgeBand)]
[JsonSubtypes.KnownSubType(typeof(Offcut), Type.Offcut)]
[JsonSubtypes.KnownSubType(typeof(Reclaim), Type.Reclaim)]
public abstract class Base: ISupportsAdditionalData, ISupportsItems
{
    /// <summary>
    /// Kind of the item. Determines which concrete item type is used when the item is deserialized.
    /// </summary>
    /// <example>Position</example>
    /// <remarks>
    /// We need to allow the *setter* here, because otherwise this property is handled 
    /// with "JsonExtensionDataAttribute" and the type is added to this collection.
    /// </remarks>
    [JsonProperty(Order = 1)]
    public abstract Type Type { get; set; }

    /// <summary>
    /// Identifier of the item. Optional.
    /// </summary>
    /// <example>1</example>
    [Key]
    [JsonProperty(Order = 0)]
    public string? Id { get; set; }

    /// <summary>
    /// Notes about the item. Optional.
    /// </summary>
    /// <example>Check measurements on site</example>
    [JsonProperty(Order = 990)]
    [StringLength(1000)]
    public string? Notes { get; set; }

    /// <summary>
    /// Child items of the item. Optional.
    /// </summary>
    [JsonProperty(Order = 995)]
    public Collection<Base>? Items { get; set; }

    /// <summary>
    /// Additional data entries attached to the item. Optional.
    /// </summary>
    [JsonProperty(Order = 997)]
    public Collection<AdditionalDataEntity>? AdditionalData { get; set; }

    /// <summary>
    /// Additional properties configured in the application that are not part of the standard contract. Optional.
    /// </summary>
    /// <example>{ "externalSource": "ERP" }</example>
    [JsonExtensionData]
    [JsonProperty(Order = 999)]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Manufacturer of the item. Optional.
    /// </summary>
    /// <example>HOMAG</example>
    [JsonProperty(Order = 1000)]
    [StringLength(255)]
    public string? Manufacturer { get; set; }
}