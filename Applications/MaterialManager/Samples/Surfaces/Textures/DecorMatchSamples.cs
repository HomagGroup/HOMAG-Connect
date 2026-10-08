using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Interfaces;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Interfaces;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches;

namespace HomagConnect.MaterialManager.Samples.Surfaces.Textures;

/// <summary>
/// Samples for searching decor matches for material types.
/// </summary>
public static class DecorMatchSamples
{
    /// <summary>
    /// Searches for decors matching a board type.
    /// </summary>
    public static async Task SearchBoardDecors(
        IMaterialManagerClientMaterialBoards materialManagerClient,
        BoardType boardType)
    {
        var result = await materialManagerClient.SearchBoardDecors(
            DecorMatchSearchRequest.ForBoard(boardType));

        result.Trace();
    }

    /// <summary>
    /// Searches for decors matching an edgeband type.
    /// </summary>
    public static async Task SearchEdgebandDecors(
        IMaterialManagerClientMaterialEdgebands materialManagerClient,
        EdgebandType edgebandType)
    {
        var result = await materialManagerClient.SearchBoardDecors(
            DecorMatchSearchRequest.ForEdgeband(edgebandType));

        result.Trace();
    }
}
