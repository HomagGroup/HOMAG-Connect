using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;

using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            if (boardTypes == null || !boardTypes.Any())
            {
                Assert.Inconclusive("No board types could be found.");
            }

            boardTypes.Trace();
        }

        /// <summary>
        /// The example shows how to get all edgeband types defined in materialManager.
        /// </summary>
        public static async Task Materials_GetEdgebandTypes(IIntelliDivideClient intelliDivide)
        {
            var edgebandTypes = await intelliDivide.GetEdgebandTypes(1000).ToListAsync();
            if (edgebandTypes == null || !edgebandTypes.Any())
            {
                Assert.Inconclusive("Nod edge band types could be found.");
            }

            edgebandTypes.Trace();
        }
    }
}