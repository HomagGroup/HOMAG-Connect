#if NET10_0_OR_GREATER

using System.Collections.Generic;
using System.Threading;
using HomagConnect.Base.Client;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Interfaces;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Interfaces;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures;

namespace HomagConnect.MaterialManager.Contracts.Extension
{
    /// <summary>
    /// Provides asynchronous paging extension methods for MaterialManager clients.
    /// </summary>
    public static class MaterialManagerClientExtensions
    {
        private const int DefaultTakeSkipPageSize = 1000;
        private const int DefaultContinuationTokenPageSize = 100;

        /// <summary>
        /// Streams all board types by requesting all take/skip pages.
        /// </summary>
        /// <param name="materialManagerClientMaterialBoards">The MaterialManager board client.</param>
        /// <param name="cancellationToken">A token that can be used to cancel paging and enumeration.</param>
        /// <returns>An asynchronous sequence containing all board types.</returns>
        public static IAsyncEnumerable<BoardType> GetBoardTypes(
            this IMaterialManagerClientMaterialBoards materialManagerClientMaterialBoards,
            CancellationToken cancellationToken = default)
        {
            return AsyncPaging.GetAllByTakeSkip<BoardType>(
                materialManagerClientMaterialBoards.GetBoardTypes,
                DefaultTakeSkipPageSize,
                cancellationToken);
        }

        /// <summary>
        /// Streams all edgeband types by requesting all take/skip pages.
        /// </summary>
        /// <param name="materialManagerClientMaterialEdgebands">The MaterialManager edgeband client.</param>
        /// <param name="cancellationToken">A token that can be used to cancel paging and enumeration.</param>
        /// <returns>An asynchronous sequence containing all edgeband types.</returns>
        public static IAsyncEnumerable<EdgebandType> GetEdgebandTypes(
            this IMaterialManagerClientMaterialEdgebands materialManagerClientMaterialEdgebands,
            CancellationToken cancellationToken = default)
        {
            return AsyncPaging.GetAllByTakeSkip<EdgebandType>(
                materialManagerClientMaterialEdgebands.GetEdgebandTypes,
                DefaultTakeSkipPageSize,
                cancellationToken);
        }

        /// <summary>
        /// Streams all textures by requesting all continuation-token pages.
        /// </summary>
        /// <param name="materialManagerClientTextures">The MaterialManager texture client.</param>
        /// <param name="cancellationToken">A token that can be used to cancel paging and enumeration.</param>
        /// <returns>An asynchronous sequence containing all textures.</returns>
        public static IAsyncEnumerable<Texture> GetTextures(
            this IMaterialManagerClientTextures materialManagerClientTextures,
            CancellationToken cancellationToken = default)
        {
            return AsyncPaging.GetAllByContinuationToken<Texture>(
                async continuationToken =>
                {
                    var result = await materialManagerClientTextures.GetTextures(filter: null, pageSize: DefaultContinuationTokenPageSize, continuationToken: continuationToken);
                    return new AsyncPaging.ContinuationTokenPage<Texture>(result.Textures, result.ContinuationToken);
                },
                cancellationToken);
        }
    }
}

#endif