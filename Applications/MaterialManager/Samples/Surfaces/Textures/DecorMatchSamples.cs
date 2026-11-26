using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches;

namespace HomagConnect.MaterialManager.Samples.Surfaces.Textures;

/// <summary>
/// Samples for searching decor matches for material types.
/// </summary>
public static class DecorMatchSamples
{
    /// <summary>
    /// Searches for decors matching a board type with details.
    /// </summary>
    public static async Task SearchBoardDecors(
        IMaterialManagerClientDecorCatalog materialManagerClient,
        BoardTypeDetails boardType)
    {
        var result = await materialManagerClient.SearchBoardDetailsDecors(
            DecorMatchSearchRequest.ForBoardDetails(boardType));

        result.Trace();
    }

    /// <summary>
    /// Searches for decors matching an edgeband type with details.
    /// </summary>
    public static async Task SearchEdgebandDecors(
        IMaterialManagerClientDecorCatalog materialManagerClient,
        EdgebandTypeDetails edgebandType)
    {
        var result = await materialManagerClient.SearchEdgebandDetailsDecors(
            DecorMatchSearchRequest.ForEdgebandDetails(edgebandType));

        result.Trace();
    }
}
