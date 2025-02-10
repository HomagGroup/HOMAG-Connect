using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;

namespace HomagConnect.IntelliDivide.Samples.Materials
{
    /// <summary />
    public static class MaterialsSamples
    {
        /// <summary>
        /// The example shows how to get all board types defined in materialManager.
        /// </summary>
        public static async Task Materials_GetBoardTypes(IIntelliDivideClient intelliDivide)
        {
            var boardTypes = await intelliDivide.GetBoardTypes(1000).ToListAsync();

            boardTypes.Trace();
        }

        /// <summary>
        /// The example shows how to get all edgeband types defined in materialManager.
        /// </summary>
        public static async Task Materials_GetEdgebandTypes(IIntelliDivideClient intelliDivide)
        {
            var edgebandTypes = await intelliDivide.GetEdgebandTypes(1000).ToListAsync();

            edgebandTypes.Trace();
        }
    }
}