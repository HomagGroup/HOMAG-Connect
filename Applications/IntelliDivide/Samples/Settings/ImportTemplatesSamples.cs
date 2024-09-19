using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Settings
{
    /// <summary />
    public class ImportTemplatesSamples
    {
        /// <summary />
        public static async Task GetCuttingTemplatesSample(IIntelliDivideClient intelliDivide)
        {
            var templates = await intelliDivide.GetImportTemplates(OptimizationType.Cutting).ToListAsync();

            Assert.IsNotNull(templates);
            Assert.IsTrue(templates.Any());
            Assert.IsFalse(templates.Any(m => m.OptimizationType != OptimizationType.Cutting));
            Assert.IsFalse(templates.Any(m => string.IsNullOrWhiteSpace(m.Name)));

            templates.Trace();
        }

        /// <summary />
        public static async Task GetNestingTemplatesSample(IIntelliDivideClient intelliDivide)
        {
            var templates = await intelliDivide.GetImportTemplates(OptimizationType.Nesting).ToListAsync();
            Assert.IsNotNull(templates);
            Assert.IsTrue(templates.Any());
            Assert.IsFalse(templates.Any(m => m.OptimizationType != OptimizationType.Nesting));
            Assert.IsFalse(templates.Any(m => string.IsNullOrWhiteSpace(m.Name)));

            templates.Trace();
        }
    }
}