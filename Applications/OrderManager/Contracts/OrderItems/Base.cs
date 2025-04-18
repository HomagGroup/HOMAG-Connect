﻿using System.Collections.ObjectModel;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Interfaces;

using JsonSubTypes;

using Newtonsoft.Json;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Production entity
/// </summary>
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
public abstract class Base: ISupportsAdditionalData
{
    /// <summary>
    /// Gets or sets the type of the item entity.
    /// </summary>
    /// <remarks>
    /// We need to allow the *setter* here, because otherwise this property is handled 
    /// with "JsonExtensionDataAttribute" and the type is added to this collection.
    /// </remarks>
    [JsonProperty(Order = 1)]
    public abstract Type Type { get; set; }

    /// <summary>
    /// Gets or sets the id
    /// </summary>
    [JsonProperty(Order = 0)]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the notes of the  entity.
    /// </summary>
    [JsonProperty(Order = 990)]
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the items.
    /// </summary>
    [JsonProperty(Order = 995)]
    public Collection<Base>? Items { get; set; }

    /// <summary>
    /// Gets or sets the additional data.
    /// </summary>
    [JsonProperty(Order = 997)]
    public Collection<AdditionalDataEntity>? AdditionalData { get; set; }

    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonExtensionData]
    [JsonProperty(Order = 999)]
    public IDictionary<string, object>? AdditionalProperties { get; set; }
}