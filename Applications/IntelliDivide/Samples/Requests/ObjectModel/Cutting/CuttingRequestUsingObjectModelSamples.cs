using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common.GrainMatchingTemplates;
using HomagConnect.IntelliDivide.Contracts.Request;
using HomagConnect.IntelliDivide.Samples.Helper;

namespace HomagConnect.IntelliDivide.Samples.Requests.ObjectModel.Cutting
{
    /// <summary>
    /// Cutting request samples using the object model.
    /// </summary>
    /// <remarks>
    /// <see
    ///     href="https://github.com/HomagGroup/HOMAG-Connect/tree/main/Applications/IntelliDivide/Samples/Requests/ObjectModel/Cutting/Readme.md" />
    /// for further details.
    /// </remarks>
    public static class CuttingRequestUsingObjectModelSamples
    {
        /// <summary>
        /// The sample shows how to create a cutting request using the object model with a parts referencing a grain matching
        /// template.
        /// </summary>
        public static async Task CuttingRequest_ObjectModel_GrainMatchingTemplate_ImportOnly(IIntelliDivideClient intelliDivide)
        {
            // Prepare the request
            var request = new OptimizationRequest
            {
                Name = "HOMAG Connect - ObjectModel_GrainMatchingTemplate_ImportOnly",
                Machine = "productionAssist Cutting",
                Parameters = "Default",
                Action = OptimizationRequestAction.ImportOnly
            };

            request.Parts.Add(
                new()
                {
                    Description = "Part A",
                    MaterialCode = "OAK_19.0",
                    Length = 800,
                    Width = 600,
                    Template = "2 Parts (2 x 1):1.1:1:1",
                    Grain = Grain.Lengthwise,
                    Quantity = 1
                });

            request.Parts.Add(
                new()
                {
                    Description = "Part B",
                    MaterialCode = "OAK_19.0",
                    Length = 800,
                    Width = 600,
                    Grain = Grain.Lengthwise,
                    Quantity = 1,
                    Template = new GrainMatchTemplateReference
                    {
                        Template = "2 Parts (2 x 1)",
                        Positions = new[]
                        {
                            new GrainMatchTemplatePosition
                            {
                                Column = 2,
                                Row = 1
                            }
                        },
                        Trims = GrainMatchingTemplateOptionsTrims.AllSides,
                        Dividing = GrainMatchingTemplateOptionsDividing.SeparatePattern,
                        Grain = Grain.Lengthwise,
                        Instance = 1
                    }
                });

            request.Boards.Add(
                new OptimizationRequestBoard
                {
                    MaterialCode = "OAK_19.0",
                    BoardCode = "OAK_19.0_2800_2070",
                    Length = 2800,
                    Width = 2070,
                    Thickness = 19.0,
                    Costs = 10,
                    Grain = Grain.Lengthwise,
                    Quantity = 70,
                });

            request.Trace(nameof(request));

            // Send the request
            var response = await intelliDivide.RequestOptimizationAsync(request);

            response.Trace(nameof(response));

            // Retrieve the optimization
            var optimization = await intelliDivide.GetOptimizationAsync(response.OptimizationId);

            optimization.Trace(nameof(optimization));
        }

        /// <summary>
        /// The sample shows how to create a cutting request using the object model with a parts having the required properties set
        /// </summary>
        public static async Task CuttingRequest_ObjectModel_RequiredProperties_ImportOnly(IIntelliDivideClient intelliDivide)
        {
            // Prepare the request
            var request = new OptimizationRequest
            {
                Name = "HOMAG Connect - ObjectModel_RequiredProperties_ImportOnly",
                Machine = "productionAssist Cutting",
                Parameters = "Default",
                Action = OptimizationRequestAction.ImportOnly
            };

            request.Parts.Add(
                new()
                {
                    Description = "Part A",
                    MaterialCode = "P2_Weiss_19.0",
                    Length = 300,
                    Width = 300,
                    Quantity = 5
                });

            request.Parts.Add(new()
            {
                Description = "Part B",
                MaterialCode = "P2_Weiss_19.0",
                Length = 600,
                Width = 300,
                Quantity = 10
            });

            request.Trace(nameof(request));

            // Send the request
            var response = await intelliDivide.RequestOptimizationAsync(request);

            response.Trace(nameof(response));

            // Retrieve the optimization
            var optimization = await intelliDivide.GetOptimizationAsync(response.OptimizationId);

            optimization.Trace(nameof(optimization));
        }

        /// <summary>
        /// The sample shows how to create a cutting request using the object model with a parts having the typical properties set
        /// </summary>
        public static async Task CuttingRequest_ObjectModel_TypicalProperties_ImportOnly(IIntelliDivideClient intelliDivide)
        {
            // Prepare the request
            var request = new OptimizationRequest
            {
                Name = "HOMAG Connect - ObjectModel_TypicalProperties_ImportOnly",
                Machine = "productionAssist Cutting",
                Parameters = "Default",
                Action = OptimizationRequestAction.ImportOnly
            };

            request.Parts.Add(
                new()
                {
                    Description = "Part (typical properties)",
                    MaterialCode = "P2_Gold Craft Oak_19.0",
                    Length = 400,
                    Width = 200,
                    Quantity = 1,

                    Grain = Grain.Lengthwise,

                    CustomerName = "HOMAG",
                    OrderName = "Kitchen 123",

                    EdgeDiagram = "011:011:000:000",
                    EdgeFront = "PP_White_1.3_22_HM",
                    EdgeBack = "PP_White_1.3_22_HM",
                    EdgeLeft = "PP_White_1.3_22_HM",
                    EdgeRight = "PP_White_1.3_22_HM",

                    CncProgramName1 = "SortingS1004",
                    CncProgramName2 = "SortingS1004_2"
                });

            request.Trace(nameof(request));

            // Send the request
            var response = await intelliDivide.RequestOptimizationAsync(request);

            response.Trace(nameof(response));

            // Retrieve the optimization

            var optimization = await intelliDivide.GetOptimizationAsync(response.OptimizationId);

            optimization.Trace(nameof(optimization));
        }
    }
}