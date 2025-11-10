using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Settings
{
    /// <summary />
    public class GeneralSettingsSamples
    {
        /// <summary>
        /// The example shows how to get the properties available for an optimization part.
        /// </summary>
        public static async Task GetPartPropertiesSample(IIntelliDivideClient intelliDivide)
        {
            var properties = await intelliDivide.GetPartProperties().ToListAsync();
            if (properties == null || properties.Count <= 0)
            {
                Assert.Fail("No part properties could be found.");
            }

            properties.Trace();
        }
    }
}
