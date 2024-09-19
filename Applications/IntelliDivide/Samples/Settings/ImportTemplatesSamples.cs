using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Settings
{
    /// <summary />
    public class ImportTemplatesSamples
    {
        /// <summary>
        /// The example shows how to get the defined cutting import templates.
        /// </summary>
        public static async Task GetCuttingTemplatesSample(IIntelliDivideClient intelliDivide)
        {
            var templates = await intelliDivide.GetImportTemplates(OptimizationType.Cutting).ToListAsync();

            templates.Trace();
        }

        /// <summary>
        /// The example shows how to get the defined nesting import templates.
        /// </summary>
        public static async Task GetNestingTemplatesSample(IIntelliDivideClient intelliDivide)
        {
            var templates = await intelliDivide.GetImportTemplates(OptimizationType.Nesting).ToListAsync();
            
            templates.Trace();
        }
    }
}