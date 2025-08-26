using System.ComponentModel.DataAnnotations;
using System.Globalization;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common.GrainMatchingTemplates;
using HomagConnect.IntelliDivide.Contracts.Request;

namespace HomagConnect.IntelliDivide.Samples.Requests.Cutting.ObjectModel
{
    /// <summary>
    /// Cutting request samples using the object model.
    /// </summary>
    /// <remarks>
    /// <see
    ///     href="https://github.com/HomagGroup/HOMAG-Connect/tree/main/Applications/IntelliDivide/Samples/Requests/Cutting/ObjectModel/Readme.md" />
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
                Name = "Sample_ObjectModel_GrainMatchingTemplate_ImportOnly" + DateTime.Now.ToString("_yyyyMMdd-HHmm", CultureInfo.InvariantCulture),
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
                        Positions =
                        [
                            new GrainMatchTemplatePosition
                            {
                                Column = 2,
                                Row = 1
                            }
                        ],
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
            var response = await intelliDivide.RequestOptimization(request);

            response.Trace(nameof(response));

            // Retrieve the optimization
            var optimization = await intelliDivide.GetOptimization(response.OptimizationId);

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
                Name = "Sample_ObjectModel_RequiredProperties_ImportOnly" + DateTime.Now.ToString("_yyyyMMdd-HHmm", CultureInfo.InvariantCulture),
                Machine = "productionAssist Cutting",
                Parameters = "Default",
                Action = OptimizationRequestAction.ImportOnly
            };

            request.Parts.Add(
                new OptimizationRequestPart
                {
                    Description = "Part A",
                    MaterialCode = "P2_White_19.0",
                    Length = 300,
                    Width = 300,
                    Quantity = 5
                });

            request.Parts.Add(new OptimizationRequestPart
            {
                Description = "Part B",
                MaterialCode = "P2_White_19.0",
                Length = 600,
                Width = 300,
                Quantity = 10
            });

            request.Trace(nameof(request));

            // Send the request
            var response = await intelliDivide.RequestOptimization(request);

            response.Trace(nameof(response));

            // Retrieve the optimization
            var optimization = await intelliDivide.GetOptimization(response.OptimizationId);

            optimization.Trace(nameof(optimization));
        }

        /// <summary>
        /// The example shows how to use the object model to create a cutting request with parts that have the required properties
        /// set and start optimization in a single request.
        /// </summary>
        public static async Task CuttingRequest_ObjectModel_RequiredProperties_ImportAndOptimize(IIntelliDivideClient intelliDivide)
        {
            // Prepare the request
            var request = new OptimizationRequest
            {
                Name = "Sample_ObjectModel_RequiredProperties_ImportAndOptimize" + DateTime.Now.ToString("_yyyyMMdd-HHmm", CultureInfo.InvariantCulture),
                Machine = "productionAssist Cutting",
                Parameters = "Default",
                Action = OptimizationRequestAction.Optimize
            };

            request.Parts.Add(
                new OptimizationRequestPart
                {
                    Description = "Part A",
                    MaterialCode = "P2_White_19.0",
                    Length = 300,
                    Width = 300,
                    Quantity = 5
                });

            request.Parts.Add(new OptimizationRequestPart
            {
                Description = "Part B",
                MaterialCode = "P2_White_19.0",
                Length = 600,
                Width = 300,
                Quantity = 10
            });

            request.Trace(nameof(request));

            // Send the request
            var response = await intelliDivide.RequestOptimization(request);

            response.Trace(nameof(response));

            // Wait for completion
            var optimization = await intelliDivide.WaitForCompletion(response.OptimizationId, CommonSampleSettings.TimeoutDuration);

            optimization.Trace(nameof(optimization));
        }

        /// <summary>
        /// The example shows how to use the object model to create a cutting request with parts that have the required properties
        /// set and start optimization in two steps.
        /// </summary>
        public static async Task CuttingRequest_ObjectModel_RequiredProperties_ImportValidateAndOptimize(IIntelliDivideClient intelliDivide)
        {
            // Prepare the request
            var request = new OptimizationRequest
            {
                Name = "Sample_ObjectModel_RequiredProperties_ImportValidateAndOptimize" + DateTime.Now.ToString("_yyyyMMdd-HHmm", CultureInfo.InvariantCulture),
                Machine = "productionAssist Cutting",
                Parameters = "Default",
                Action = OptimizationRequestAction.ImportOnly
            };

            request.Parts.Add(
                new OptimizationRequestPart
                {
                    Description = "Part A",
                    MaterialCode = "P2_White_19.0",
                    Length = 300,
                    Width = 300,
                    Quantity = 5
                });

            request.Parts.Add(new OptimizationRequestPart
            {
                Description = "Part B",
                MaterialCode = "P2_White_19.0",
                Length = 600,
                Width = 300,
                Quantity = 10
            });

            request.Trace(nameof(request));

            // Send the request
            var response = await intelliDivide.RequestOptimization(request);

            response.Trace(nameof(response));

            // Validate the request
            if (response.ValidationResults != null && response.ValidationResults.Any())
            {
                throw new ValidationException(response.ValidationResults[0].ErrorMessage);
            }

            //Start the optimization
            await intelliDivide.StartOptimization(response.OptimizationId);

            // Wait for completion
            var optimization = await intelliDivide.WaitForCompletion(response.OptimizationId, CommonSampleSettings.TimeoutDuration);

            optimization.Trace(nameof(optimization));
        }

        /// <summary>
        /// The example shows how to use the object model to create a cutting request with parts that have the required properties,
        /// start the optimization, and transfer the balanced solution in a single request.
        /// </summary>
        public static async Task CuttingRequest_ObjectModel_RequiredProperties_ImportOptimizeAndSend(IIntelliDivideClient intelliDivide)
        {
            // Prepare the request
            var request = new OptimizationRequest
            {
                Name = "Sample_ObjectModel_RequiredProperties_ImportOptimizeAndSend" + DateTime.Now.ToString("_yyyyMMdd-HHmm", CultureInfo.InvariantCulture),
                Machine = "productionAssist Cutting",
                Parameters = "Default",
                Action = OptimizationRequestAction.Send
            };

            request.Parts.Add(
                new OptimizationRequestPart
                {
                    Description = "Part A",
                    MaterialCode = "P2_White_19.0",
                    Length = 300,
                    Width = 300,
                    Quantity = 5
                });

            request.Parts.Add(new OptimizationRequestPart
            {
                Description = "Part B",
                MaterialCode = "P2_White_19.0",
                Length = 600,
                Width = 300,
                Quantity = 10
            });

            request.Trace(nameof(request));

            // Send the request
            var response = await intelliDivide.RequestOptimization(request);

            response.Trace(nameof(response));

            // Wait for completion
            var optimization = await intelliDivide.WaitForOptimizationStatus(response.OptimizationId, OptimizationStatus.Transferred, CommonSampleSettings.TimeoutDuration);

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
                Name = "Sample_ObjectModel_TypicalProperties_ImportOnly" + DateTime.Now.ToString("_yyyyMMdd-HHmm", CultureInfo.InvariantCulture),
                Machine = "productionAssist Cutting",
                Parameters = "Default",
                Action = OptimizationRequestAction.ImportOnly
            };

            request.Parts.Add(
                new()
                {
                    Description = "Part (typical properties)",
                    MaterialCode = "P2_Gold_Craft_Oak_19.0",
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
            var response = await intelliDivide.RequestOptimization(request);

            response.Trace(nameof(response));

            // Retrieve the optimization

            var optimization = await intelliDivide.GetOptimization(response.OptimizationId);

            optimization.Trace(nameof(optimization));
        }

        /// <summary>
        /// The sample shows how to create a cutting request with parts having all properties (incl. additional properties set).
        /// </summary>
        public static async Task CuttingRequest_ObjectModel_AdditionalProperties_Optimize(IIntelliDivideClient intelliDivide)
        {
            // Prepare the request
            var request = new OptimizationRequest
            {
                Name = "Sample_ObjectModel_AdditionalProperties_Optimize" + DateTime.Now.ToString("_yyyyMMdd-HHmm", CultureInfo.InvariantCulture),
                Machine = "productionAssist Cutting",
                Parameters = "Default",
                Action = OptimizationRequestAction.Optimize
            };

            request.Parts.Add(
                new OptimizationRequestPart
                {
                    Id = "productionRack 1004",
                    CustomerName = "HOMAG",
                    OrderName = "Kitchen 123",
                    OrderDate = DateTime.Today,
                    OrderItem = "1.1",

                    Notes = "This part shows how to set all properties.",

                    Description = "Side panel right",
                    MaterialCode = "P2_Gold_Craft_Oak_19.0",
                    Grain = Grain.Lengthwise,
                    Length = 400,
                    Width = 200,

                    Quantity = 1,
                    QuantityPlus = 5,

                    EdgeDiagram = "011:011:000:000",
                    EdgeFront = "PP_White_1.3_22_HM",
                    EdgeBack = "PP_White_1.3_22_HM",
                    EdgeLeft = "PP_White_1.3_22_HM",
                    EdgeRight = "PP_White_1.3_22_HM",

                    LaminateTop = "HPL_U961_2_0.8",
                    LaminateBottom = "HPL_U961_2_0.8",

                    FinishLength = 400,
                    FinishWidth = 200,

                    SecondCutLength = 399,
                    SecondCutWidth = 199,

                    CncProgramName1 = "SortingS1004",
                    CncProgramName2 = "SortingS1004_2",

                    LabelLayout = "Label#1",

                    Lot = "Lot #1",

                    AdditionalProperties = new Dictionary<string, object> { { "DeliveryRegion", "North" } }
                });

            request.Trace(nameof(request));

            // Send the request
            var response = await intelliDivide.RequestOptimization(request);

            // Wait for the optimization to complete
            var optimization = await intelliDivide.WaitForCompletion(response.OptimizationId, CommonSampleSettings.TimeoutDuration);

            // Get the solutions overview
            var solutions = await intelliDivide.GetSolutions(optimization.Id);

            // Get the solution details of the balanced solution
            var balancedSolution = await intelliDivide.GetSolutionDetails(optimization.Id, solutions.First().Id);

            balancedSolution.Trace(nameof(balancedSolution));
        }

        /// <summary>
        /// The sample shows how to create a cutting request using the object model with parts having stacking groups set.
        /// </summary>
        public static async Task CuttingRequest_ObjectModel_StackingGroups_ImportOnly(IIntelliDivideClient intelliDivide)
        {
            // Prepare the request
            var request = new OptimizationRequest
            {
                Name = "Sample_ObjectModel_StackingGroups_ImportOnly" + DateTime.Now.ToString("_yyyyMMdd-HHmm", CultureInfo.InvariantCulture),
                Machine = "productionAssist Cutting",
                Parameters = "HOMAG Connect stacking groups",
                Action = OptimizationRequestAction.ImportOnly
            };

            request.Parts.Add(
                new()
                {
                    Description = "Part A",
                    MaterialCode = "OAK_19.0",
                    Length = 800,
                    Width = 600,
                    Grain = Grain.Lengthwise,
                    Quantity = 3,
                    DestackingGroup = "A"
                });

            request.Parts.Add(
                new()
                {
                    Description = "Part B",
                    MaterialCode = "OAK_19.0",
                    Length = 800,
                    Width = 600,
                    Grain = Grain.Lengthwise,
                    Quantity = 2,
                    DestackingGroup = "B"
                });

            request.Parts.Add(
                new()
                {
                    Description = "Part C",
                    MaterialCode = "OAK_19.0",
                    Length = 800,
                    Width = 600,
                    Grain = Grain.Lengthwise,
                    Quantity = 3,
                    DestackingGroup = "A"
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
            var response = await intelliDivide.RequestOptimization(request);

            response.Trace(nameof(response));

            // Retrieve the optimization
            var optimization = await intelliDivide.GetOptimization(response.OptimizationId);

            optimization.Trace(nameof(optimization));
        }

        /// <summary>
        /// The sample shows how to create a cutting request using the object model with parts having stacking groups set.
        /// </summary>
        public static async Task CuttingRequest_ObjectModel_StackingGroups_Optimize(IIntelliDivideClient intelliDivide)
        {
            // Prepare the request
            var request = new OptimizationRequest
            {
                Name = "Sample_ObjectModel_StackingGroups_Optimize" + DateTime.Now.ToString("_yyyyMMdd-HHmm", CultureInfo.InvariantCulture),
                Machine = "productionAssist Cutting",
                Parameters = "HOMAG Connect stacking groups",
                Action = OptimizationRequestAction.Optimize
            };

            request.Parts.Add(
                new()
                {
                    Description = "Part A",
                    MaterialCode = "P2_White_19.0",
                    Length = 800,
                    Width = 600,
                    Grain = Grain.Lengthwise,
                    Quantity = 3,
                    DestackingGroup = "A"
                });

            request.Parts.Add(
                new()
                {
                    Description = "Part B",
                    MaterialCode = "OAK_19.0",
                    Length = 800,
                    Width = 600,
                    Grain = Grain.Lengthwise,
                    Quantity = 2,
                    DestackingGroup = "B"
                });

            request.Parts.Add(
                new()
                {
                    Description = "Part C",
                    MaterialCode = "P2_White_19.0",
                    Length = 800,
                    Width = 600,
                    Grain = Grain.Lengthwise,
                    Quantity = 3,
                    DestackingGroup = "A"
                });

            request.Parts.Add(
                new()
                {
                    Description = "Part D",
                    MaterialCode = "OAK_19.0",
                    Length = 800,
                    Width = 600,
                    Grain = Grain.Lengthwise,
                    Quantity = 1
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

            request.Boards.Add(
                new OptimizationRequestBoard
                {
                    MaterialCode = "P2_White_19.0",
                    BoardCode = "P2_White_19.0_2800_2070",
                    Length = 2800,
                    Width = 2070,
                    Thickness = 19.0,
                    Costs = 10,
                    Grain = Grain.None,
                    Quantity = 70,
                });

            request.Trace(nameof(request));

            // Send the request
            var response = await intelliDivide.RequestOptimization(request);

            response.Trace(nameof(response));

            // Wait for the optimization to complete
            var optimization = await intelliDivide.WaitForCompletion(response.OptimizationId, CommonSampleSettings.TimeoutDuration);

            optimization.Trace(nameof(optimization));

            // Get the solutions overview
            var solutions = await intelliDivide.GetSolutions(optimization.Id);

            // Get the solution details of the balanced solution
            var balancedSolution = await intelliDivide.GetSolutionDetails(optimization.Id, solutions.First().Id);

            balancedSolution.Trace(nameof(balancedSolution));
        }
    }
}