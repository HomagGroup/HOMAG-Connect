using System.Globalization;
using System.Runtime.CompilerServices;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Request;
using HomagConnect.IntelliDivide.Contracts.Result;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Requests.Cutting
{
    /// <summary>
    /// The class contains examples of cutting optimization requests that are common to requests created from the object model,
    /// an import template, or the project zip file.
    /// </summary>
    public static class CuttingOptimizationCommonSamples
    {
        /// <summary>
        /// The example shows how to request an optimization model with specific boards. If at least one board is added, the boards
        /// defined in MaterialManager are ignored.
        /// </summary>
        public static async Task CuttingRequest_SpecificBoards(IIntelliDivideClient intelliDivide)
        {
            var request = new OptimizationRequest
            {
                Name = "Sample_SpecificBoards" + DateTime.Now.ToString("_yyyyMMdd-HHmm", CultureInfo.InvariantCulture),
                Machine = "productionAssist Cutting",
                Parameters = "Default",
                Action = OptimizationRequestAction.Optimize
            };

            request.Parts.Add(new OptimizationRequestPart
            {
                Description = "Part A",
                MaterialCode = "MDF_19.0",
                Length = 800,
                Width = 600,
                Quantity = 1
            });

            request.Boards.Add(
                new OptimizationRequestBoard
                {
                    MaterialCode = "MDF_19.0",
                    BoardCode = "MDF_19.0_2800_2070",
                    Length = 2800,
                    Width = 2070,
                    Thickness = 19.0,
                    Costs = 10,
                    Grain = Grain.None,
                    Quantity = 70,
                });

            var response = await intelliDivide.RequestOptimization(request);
            if (response == null)
            {
                Assert.Fail("The request did not send a response.");
            }

            Optimization optimization = null;
            const int maxRetries = 5;
            int retryCount = 0;

            while (retryCount < maxRetries)
            {
                try
                {
                    optimization = await intelliDivide.WaitForOptimizationStatus(response.OptimizationId, OptimizationStatus.Optimized, CommonSampleSettings.TimeoutDuration);
                    if (optimization != null)
                    {
                        break;
                    }
                }
                catch
                {
                    retryCount++;
                    if (retryCount == maxRetries)
                    {
                        Assert.Fail($"The optimization with id {response.OptimizationId} could not be optimized after {maxRetries} attempts.");
                    }
                }
            }

            var solutions = await intelliDivide.GetSolutions(optimization!.Id);
            if (solutions == null || !solutions.Any())
            {
                Assert.Fail($"The optimization with id {optimization.Id} should have at least one solution available.");
            }

            var recommendedSolution = solutions.First();
            if (recommendedSolution == null)
            {
                Assert.Fail($"The solutions for the optimization with id {optimization.Id} should have at least one element.");
            }

            var targetDirectory = new DirectoryInfo(".");

            await intelliDivide.DownloadSolutionExport(recommendedSolution, SolutionExportType.Saw, targetDirectory);
        }

        /// <summary>
        /// The example shows how to send a specific solution to the machine after optimization.
        /// </summary>
        public static async Task CuttingRequest_SendSpecificSolution(IIntelliDivideClient intelliDivide)
        {
            // Prepare the request
            var request = await GetSampleCuttingOptimizationRequest(intelliDivide, OptimizationRequestAction.Optimize);

            request.Name = "Sample_SendSpecificSolution" + DateTime.Now.ToString("_yyyyMMdd-HHmm", CultureInfo.InvariantCulture);

            request.Trace(nameof(request));

            // Send the request
            var response = await intelliDivide.RequestOptimization(request);
            if (response == null)
            {
                Assert.Fail("The request did not send a response.");
            }

            response.Trace(nameof(response));

            // Wait for completion
            Optimization optimization = null;
            const int maxRetries = 5;
            int retryCount = 0;

            while (retryCount < maxRetries)
            {
                try
                {
                    optimization = await intelliDivide.WaitForCompletion(response.OptimizationId, CommonSampleSettings.TimeoutDuration);
                    if (optimization != null)
                    {
                        break;
                    }
                }
                catch
                {
                    retryCount++;
                    if (retryCount == maxRetries)
                    {
                        Assert.Fail($"The optimization with id {response.OptimizationId} could not be completed after {maxRetries} attempts.");
                    }
                }
            }

            optimization!.Trace(nameof(optimization));

            if (optimization!.Status != OptimizationStatus.Optimized)
            {
                throw new InvalidOperationException("Optimization did not reach the state optimized.");
            }

            var solutions = await intelliDivide.GetSolutions(optimization.Id);
            if (solutions == null || !solutions.Any())
            {
                Assert.Fail($"The optimization with id {optimization.Id} should have at least one solution available.");
            }

            var solutionToSend = solutions.First();
            if (solutionToSend == null)
            {
                Assert.Fail($"The solutions for the optimization with id {optimization.Id} should have at least one element.");
            }

            await intelliDivide.SendSolution(optimization.Id, solutionToSend.Id);
        }

        /// <summary>
        /// The example shows how to send a specific solution to the machine after optimization.
        /// </summary>
        public static async Task CuttingRequest_RetrieveResults(IIntelliDivideClient intelliDivide)
        {
            // Prepare the request
            var request = await GetSampleCuttingOptimizationRequest(intelliDivide, OptimizationRequestAction.Optimize);

            request.Name = "Sample_RetrieveResults" + DateTime.Now.ToString("_yyyyMMdd-HHmm", CultureInfo.InvariantCulture);

            request.Trace(nameof(request));

            // Send the request
            var response = await intelliDivide.RequestOptimization(request);
            if (response == null)
            {
                Assert.Fail("The request did not send a response.");
            }

            response.Trace(nameof(response));

            // Wait for completion
            Optimization optimization = null;
            const int maxRetries = 5;
            int retryCount = 0;

            while (retryCount < maxRetries)
            {
                try
                {
                    optimization = await intelliDivide.WaitForCompletion(response.OptimizationId, CommonSampleSettings.TimeoutDuration);
                    if (optimization != null)
                    {
                        break;
                    }
                }
                catch
                {
                    retryCount++;
                    if (retryCount == maxRetries)
                    {
                        Assert.Fail($"The optimization with id {response.OptimizationId} could not be completed after {maxRetries} attempts.");
                    }
                }
            }

            optimization.Trace(nameof(optimization));

            if (optimization.Status != OptimizationStatus.Optimized)
            {
                throw new InvalidOperationException("Optimization did not reach the state optimized.");
            }

            var solutions = await intelliDivide.GetSolutions(optimization.Id).ToListAsync() ?? throw new InvalidOperationException("Solutions could not get retrieved.");
            solutions.Trace(nameof(solutions));

            var balancedSolutionDetails = await intelliDivide.GetSolutionDetails(optimization.Id, solutions.First().Id);
            if(balancedSolutionDetails == null)
            {
                Assert.Fail($"The solutions for the optimization with id {optimization.Id} should have at least one element.");
            }

            balancedSolutionDetails.Trace(nameof(balancedSolutionDetails));

            await intelliDivide.DownloadSolutionExport(optimization.Id, balancedSolutionDetails.Id, SolutionExportType.Saw,
                new FileInfo("CreateCuttingOptimizationByObjectModelOptimizeAndRetrieveResults.saw"));
        }

        private static async Task<OptimizationRequest> GetSampleCuttingOptimizationRequest(IIntelliDivideClient intelliDivide, OptimizationRequestAction optimizationRequestAction,
            [CallerMemberName] string optimizationName = "")
        {
            var request = new OptimizationRequest();

            var machine = await intelliDivide.GetMachine("productionAssist Cutting");
            if (machine == null)
            {
                Assert.Fail("The machine is not available.");
            }

            var parameter = await intelliDivide.GetParameters(machine.OptimizationType).FirstOrDefaultAsync();
            if (parameter == null)
            {
                Assert.Fail("There is no optimizing parameter available.");
            }

            request.Name = optimizationName + DateTime.Now.ToString("s", CultureInfo.InvariantCulture);
            request.Machine = machine.Name;
            request.Parameters = parameter.Name;

            request.Parts.Add(new OptimizationRequestPart
            {
                Description = "Part A",
                MaterialCode = "P2_Gold_Craft_Oak_19.0",
                Length = 400,
                Width = 200,
                Grain = Grain.Lengthwise,
                Quantity = 1
            });

            request.Parts.Add(new OptimizationRequestPart
            {
                Description = "Part B",
                MaterialCode = "P2_White_19.0",
                Length = 300,
                Width = 300,
                Grain = Grain.None,
                Quantity = 5
            });

            request.Action = optimizationRequestAction;

            return request;
        }
    }
}