using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Request;
using HomagConnect.IntelliDivide.Samples.Helper;

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
            var response = await intelliDivide.RequestOptimizationAsync(request, mprFiles.ToArray());

            response.Trace(nameof(response));

            // Retrieve the optimization
            var optimization = await intelliDivide.GetOptimizationAsync(response.OptimizationId);

            optimization.Trace(nameof(optimization));
        }
    }
}