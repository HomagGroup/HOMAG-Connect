using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;

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

            properties.Trace();
        }
    }
}
