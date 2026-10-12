using System.ComponentModel.DataAnnotations;
using System.Globalization;

using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches.Enumerations;

using JsonSubTypes;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches;

/// <summary>
/// Represents a request to search for decors that match a given board or edgeband.
/// </summary>
[JsonConverter(typeof(JsonSubtypes), nameof(MaterialType))]
[JsonSubtypes.KnownSubType(typeof(BoardTypeDecorMatchSearchRequest), DecorMatchMaterialType.Board)]
[JsonSubtypes.KnownSubType(typeof(EdgebandTypeDecorMatchSearchRequest), DecorMatchMaterialType.Edgeband)]
public abstract class DecorMatchSearchRequest
{
    /// <summary>
    /// Gets whether this request matches against a board or an edgeband.
    /// </summary>
    /// <example>Board</example>
    [JsonProperty(Order = 0)]
    public abstract DecorMatchMaterialType MaterialType { get; }

    /// <summary>
    /// Gets or sets an optional free text search term to narrow down the results.
    /// </summary>
    /// <example>Oak</example>
    [JsonProperty(Order = 2)]
    public string? SearchTerm { get; set; }

    /// <summary>
    /// Gets or sets the preferred language used to resolve <see cref="DecorMatchCandidate.LocalizedName" />.
    /// </summary>
    /// <example>en-US</example>
    [JsonProperty(Order = 3)]
    public CultureInfo Culture { get; set; } = CultureInfo.CurrentUICulture;

    /// <summary>
    /// Gets or sets the number of matching decors to skip, for paging through results.
    /// </summary>
    /// <example>0</example>
    [JsonProperty(Order = 4)]
    [Range(0, int.MaxValue)]
    public int Skip { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of decors to return.
    /// </summary>
    /// <example>50</example>
    [JsonProperty(Order = 5)]
    [Range(1, 200)]
    public int Take { get; set; } = 50;

    /// <summary>
    /// Creates a request to search for decors that match the given board.
    /// </summary>
    /// <param name="material">The board used as the matching signal.</param>
    /// <param name="searchTerm">An optional free text search term.</param>
    /// <param name="culture">The preferred language. Defaults to <see cref="CultureInfo.CurrentUICulture" />.</param>
    /// <param name="skip">The number of matching decors to skip, for paging through results.</param>
    /// <param name="take">The maximum number of decors to return.</param>
    public static BoardTypeDecorMatchSearchRequest ForBoard(BoardType material, string? searchTerm = null, CultureInfo? culture = null, int skip = 0, int take = 50)
    {
        return new BoardTypeDecorMatchSearchRequest
        {
            Material = material,
            SearchTerm = searchTerm,
            Culture = culture ?? CultureInfo.CurrentUICulture,
            Skip = skip,
            Take = take
        };
    }

    /// <summary>
    /// Creates a request to search for decors that match the given edgeband.
    /// </summary>
    /// <param name="material">The edgeband used as the matching signal.</param>
    /// <param name="searchTerm">An optional free text search term.</param>
    /// <param name="culture">The preferred language. Defaults to <see cref="CultureInfo.CurrentUICulture" />.</param>
    /// <param name="skip">The number of matching decors to skip, for paging through results.</param>
    /// <param name="take">The maximum number of decors to return.</param>
    public static EdgebandTypeDecorMatchSearchRequest ForEdgeband(EdgebandType material, string? searchTerm = null, CultureInfo? culture = null, int skip = 0, int take = 50)
    {
        return new EdgebandTypeDecorMatchSearchRequest
        {
            Material = material,
            SearchTerm = searchTerm,
            Culture = culture ?? CultureInfo.CurrentUICulture,
            Skip = skip,
            Take = take
        };
    }
}