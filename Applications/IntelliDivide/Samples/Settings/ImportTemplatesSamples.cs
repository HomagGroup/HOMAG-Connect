﻿using HomagConnect.IntelliDivide.Client;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Samples.Helper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Settings
{
    public class ImportTemplatesSamples
    {
        public static async Task GetCuttingTemplatesSample(IntelliDivideClient intelliDivide)
        {
            var templates = (await intelliDivide.GetImportTemplatesAsync(OptimizationType.Cutting)).ToArray();

            Assert.IsNotNull(templates);
            Assert.IsTrue(templates.Any());
            Assert.IsFalse(templates.Any(m => m.OptimizationType != OptimizationType.Cutting));
            Assert.IsFalse(templates.Any(m => string.IsNullOrWhiteSpace(m.Name)));

            templates.Trace();
        }

        public static async Task GetNestingTemplatesSample(IntelliDivideClient intelliDivide)
        {
            var templates = (await intelliDivide.GetImportTemplatesAsync(OptimizationType.Nesting)).ToArray();

            Assert.IsNotNull(templates);
            Assert.IsTrue(templates.Any());
            Assert.IsFalse(templates.Any(m => m.OptimizationType != OptimizationType.Nesting));
            Assert.IsFalse(templates.Any(m => string.IsNullOrWhiteSpace(m.Name)));

            templates.Trace();
        }
    }
}