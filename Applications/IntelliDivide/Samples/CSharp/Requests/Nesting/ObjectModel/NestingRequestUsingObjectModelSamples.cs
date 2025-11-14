using System.Collections.ObjectModel;
using System.Globalization;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Request;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Requests.Nesting.ObjectModel
{
    /// <summary>
    /// Cutting request samples using the object model.
    /// </summary>
    /// <remarks>
    /// <see
    ///     href="https://github.com/HomagGroup/HOMAG-Connect/tree/main/Applications/IntelliDivide/Samples/Requests/Nesting/ObjectModel/Readme.md" />
    /// for further details.
    /// </remarks>
    public static class NestingRequestUsingObjectModelSamples
    {
        /// <summary>
        /// The sample shows how to create a cutting request using the object model with a parts referencing a grain matching
        /// template.
        /// </summary>
        public static async Task NestingRequest_ObjectModel_MprProgramVariables_ImportOnly(IIntelliDivideClient intelliDivide)
        {
            var mprFiles = new List<ImportFile>();

            // Prepare the request
            var request = new OptimizationRequest
            {
                Name = "Sample_ObjectModel_MprProgramVariables_ImportOnly" + DateTime.Now.ToString("_yyyyMMdd-HHmm", CultureInfo.InvariantCulture),
                Machine = "productionAssist Nesting",
                Parameters = (await intelliDivide.GetParameters(OptimizationType.Nesting).FirstAsync()).Name,
                Action = OptimizationRequestAction.ImportOnly
            };

            var mpr = await ImportFile.CreateAsync(new FileInfo(@"Data\Nesting\Generic.mpr"));
            var mprReference = mpr.Name;

            // Part A

            request.Parts.Add(new OptimizationRequestPart
            {
                Description = "Part A",
                MprFileName = mprReference,
                MaterialCode = "P2_Gold_Craft_Oak_19.0",
                Grain = Grain.Lengthwise,
                MprProgramVariables = new Collection<MprProgramVariable>
                {
                    new() { Name = "L", Value = "980.0" },
                    new() { Name = "B", Value = "450.0" }
                },
                Quantity = 2
            });

            // Part B
            request.Parts.Add(new OptimizationRequestPart
            {
                Description = "Part B",
                MprFileName = mprReference,
                MaterialCode = "P2_Gold_Craft_Oak_19.0",
                Grain = Grain.Lengthwise,
                MprProgramVariables = new Collection<MprProgramVariable>
                {
                    new() { Name = "L", Value = "720.0" },
                    new() { Name = "B", Value = "380.0" }
                },
                Quantity = 3
            });

            mprFiles.Add(mpr);

            request.Trace(nameof(request));

            // Send the request
            var response = await intelliDivide.RequestOptimization(request, mprFiles);
            if (response == null)
            {
                Assert.Fail("The request did not send a response.");
            }

            response.Trace(nameof(response));

            // Retrieve the optimization
            var optimization = await intelliDivide.GetOptimization(response.OptimizationId);
            if (optimization == null)
            {
                Assert.Fail($"The optimization with id {response.OptimizationId} could not be found.");
            }

            optimization.Trace(nameof(optimization));
        }

        /// <summary>
        /// The sample shows how to create a cutting request using the object model with a parts referencing a grain matching
        /// template.
        /// </summary>
        public static async Task NestingRequest_ObjectModel_RequiredProperties_ImportOnly(IIntelliDivideClient intelliDivide)
        {
            var mprFiles = new List<ImportFile>();

            // Prepare the request
            var request = new OptimizationRequest
            {
                Name = "Sample_ObjectModel_RequiredProperties_ImportOnly" + DateTime.Now.ToString("_yyyyMMdd-HHmm", CultureInfo.InvariantCulture),
                Machine = "productionAssist Nesting",
                Parameters = (await intelliDivide.GetParameters(OptimizationType.Nesting).FirstAsync()).Name,
                Action = OptimizationRequestAction.ImportOnly
            };

            // Part A

            var mprA = await ImportFile.CreateAsync(new FileInfo(@"Data\Nesting\PartA.mpr"));

            request.Parts.Add(new OptimizationRequestPart
            {
                Description = "Part A",
                MprFileName = mprA.Name,
                MaterialCode = "P2_Gold_Craft_Oak_19.0",
                Grain = Grain.Lengthwise,
                Quantity = 2
            });

            mprFiles.Add(mprA);

            request.Trace(nameof(request));

            // Send the request
            var response = await intelliDivide.RequestOptimization(request, mprFiles);
            if (response == null)
            {
                Assert.Fail("The request did not send a response.");
            }

            response.Trace(nameof(response));

            // Retrieve the optimization
            var optimization = await intelliDivide.GetOptimization(response.OptimizationId);
            if (optimization == null)
            {
                Assert.Fail($"The optimization with id {response.OptimizationId} could not be found.");
            }

            optimization.Trace(nameof(optimization));
        }

        /// <summary />
        public static async Task NestingRequest_ObjectModel_RequiredProperties_Optimize(IIntelliDivideClient intelliDivide)
        {
            // Prepare the request
            var mprFiles = new List<ImportFile>();

            var machine = await intelliDivide.GetMachines(OptimizationType.Nesting).FirstOrDefaultAsync();
            if (machine == null)
            {
                Assert.Fail("There is no Nesting machine available.");
            }

            var parameter = await intelliDivide.GetParameters(machine.OptimizationType).FirstOrDefaultAsync();
            if (parameter == null)
            {
                Assert.Fail("There is no optimizing parameter available.");
            }

            var request = await GetSampleNestingOptimizationByObjectModel(mprFiles);

            request.Name = "Nesting Sample-Optimize" + DateTime.Now.ToString("-yyyyMMdd-HHmm", CultureInfo.InvariantCulture);
            request.Machine = machine.Name;
            request.Parameters = parameter.Name;

            request.Action = OptimizationRequestAction.Optimize;

            request.Trace(nameof(request));

            // Send the request
            var response = await intelliDivide.RequestOptimization(request, mprFiles.ToArray());
            if (response == null)
            {
                Assert.Fail("The request did not send a response.");
            }

            response.Trace(nameof(response));

            // Optional: Wait for the optimization to complete
            var optimization = await intelliDivide.WaitForCompletion(response.OptimizationId, CommonSampleSettings.TimeoutDuration);
            if (optimization == null)
            {
                Assert.Fail($"The optimization with id {response.OptimizationId} could not be optimized.");
            }

            optimization.Trace(nameof(optimization));
        }

        internal static async Task<OptimizationRequest> GetSampleNestingOptimizationByObjectModel(List<ImportFile> mprFiles)
        {
            var request = new OptimizationRequest();

            // Part A

            var mprA = new FileInfo(@"Data\Nesting\PartA.mpr");

            request.Parts.Add(new OptimizationRequestPart
            {
                Description = mprA.Name,
                MprFileName = mprA.Name,
                MaterialCode = "P2_Gold_Craft_Oak_19.0",
                Grain = Grain.Lengthwise,
                MprProgramVariables = new Collection<MprProgramVariable>
                {
                    new() { Name = "L", Value = "980" },
                    new() { Name = "B", Value = "450" }
                },
                Quantity = 2
            });

            mprFiles.Add(await ImportFile.CreateAsync(mprA));

            // Part B
            var mprB = new FileInfo(@"Data\Nesting\PartB.mpr");

            request.Parts.Add(new OptimizationRequestPart
            {
                Description = mprB.Name,
                MprFileName = mprB.Name,

                MaterialCode = "P2_White_19.0",
                Grain = Grain.None,
                Quantity = 5
            });

            mprFiles.Add(await ImportFile.CreateAsync(mprB));

            return request;
        }
    }
}