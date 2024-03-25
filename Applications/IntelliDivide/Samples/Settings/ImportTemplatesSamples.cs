using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Samples.Helper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Settings
{
    /// <summary />
    public class ImportTemplatesSamples
    {
        /// <summary />
        public static async Task GetCuttingTemplatesSample(IIntelliDivideClient intelliDivide)
        {
            var templates = await intelliDivide.GetImportTemplatesAsync(OptimizationType.Cutting).ToListAsync();

            Assert.IsNotNull(templates);
            Assert.IsTrue(templates.Any());
            Assert.IsFalse(templates.Any(m => m.OptimizationType != OptimizationType.Cutting));
            Assert.IsFalse(templates.Any(m => string.IsNullOrWhiteSpace(m.Name)));

            templates.Trace();
        }

        /// <summary />
        public static async Task GetNestingTemplatesSample(IIntelliDivideClient intelliDivide)
        {
            var templates = await intelliDivide.GetImportTemplatesAsync(OptimizationType.Nesting).ToListAsync();
            Assert.IsNotNull(templates);
            Assert.IsTrue(templates.Any());
            Assert.IsFalse(templates.Any(m => m.OptimizationType != OptimizationType.Nesting));
            Assert.IsFalse(templates.Any(m => string.IsNullOrWhiteSpace(m.Name)));

            templates.Trace();
        }
    }
}