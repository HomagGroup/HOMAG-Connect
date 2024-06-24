using System.Collections.ObjectModel;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Request;

namespace HomagConnect.IntelliDivide.Samples.Requests.ObjectModel.Nesting
{
    /// <summary>
    /// Cutting request samples using the object model.
    /// </summary>
    /// <remarks>
    /// <see
    ///     href="https://github.com/HomagGroup/HOMAG-Connect/tree/main/Applications/IntelliDivide/Samples/Requests/ObjectModel/Nesting/Readme.md" />
    /// for further details.
    /// </remarks>
    public static class NestingRequestUsingObjectModelSamples
    {
        /// <summary>
        /// Gets the materials used in the samples.
        /// </summary>
        public static string[] SampleMaterialCodes = { "P2_White_19", "P2_Gold_Craft_Oak_19.0" };

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
                Name = "HOMAG Connect - ObjectModel_MprProgramVariables_ImportOnly",
                Machine = "productionAssist Nesting",
                Parameters = "Default",
                Action = OptimizationRequestAction.ImportOnly
            };

            var mpr = await ImportFile.CreateAsync(new FileInfo(@"Requests\ObjectModel\Nesting\Generic.mpr"));
            var mprReference = mpr.Name;

            // Part A

            request.Parts.Add(new OptimizationRequestPart
            {
                Description = "Part A",
                MprFileName = mprReference,
                MaterialCode = "P2_Gold Craft Oak_19",
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
                MaterialCode = "P2_Gold Craft Oak_19",
                Grain = Grain.Lengthwise,
                MprProgramVariables = new Collection<MprProgramVariable>
                {
                    new() { Name = "L", Value = "720.0" },
                    new() { Name = "B", Value = "380.0" }
                },
                Quantity = 3
            });

            mprFiles.Add(mpr);

            // Send the request
            var response = await intelliDivide.RequestOptimizationAsync(request, mprFiles);

            response.Trace(nameof(response));

            // Retrieve the optimization
            var optimization = await intelliDivide.GetOptimizationAsync(response.OptimizationId);

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
                Name = "HOMAG Connect - ObjectModel_RequiredProperties_ImportOnly",
                Machine = "productionAssist Nesting",
                Parameters = "Default",
                Action = OptimizationRequestAction.ImportOnly
            };

            // Part A

            var mprA = await ImportFile.CreateAsync(new FileInfo(@"Requests\ObjectModel\Nesting\PartA.mpr"));

            request.Parts.Add(new OptimizationRequestPart
            {
                Description = "Part A",
                MprFileName = mprA.Name,
                MaterialCode = "P2_Gold Craft Oak_19",
                Grain = Grain.Lengthwise,
                Quantity = 2
            });

            mprFiles.Add(mprA);

            // Send the request
            var response = await intelliDivide.RequestOptimizationAsync(request, mprFiles);

            response.Trace(nameof(response));

            // Retrieve the optimization
            var optimization = await intelliDivide.GetOptimizationAsync(response.OptimizationId);

            optimization.Trace(nameof(optimization));
        }
    }
}