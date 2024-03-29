﻿using System.Runtime.CompilerServices;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Constants;
using HomagConnect.IntelliDivide.Contracts.Request;
using HomagConnect.IntelliDivide.Contracts.Result;
using HomagConnect.IntelliDivide.Samples.Helper;

namespace HomagConnect.IntelliDivide.Samples.Requests.Cutting
{
    /// <summary />
    public class CuttingOptimizationUsingObjectModel
    {
        /// <summary />
        public static async Task CreateCuttingOptimizationByObjectModel(IIntelliDivideClient intelliDivide)
        {
            // Prepare the request
            var request = await GetSampleCuttingOptimizationByObjectModel(intelliDivide, OptimizationRequestAction.ImportOnly);

            request.Trace(nameof(request));

            // Send the request
            var response = await intelliDivide.RequestOptimizationAsync(request);

            response.Trace(nameof(response));

            // Retrieve the optimization
            var optimization = await intelliDivide.GetOptimizationAsync(response.OptimizationId);

            optimization.Trace(nameof(optimization));
        }

        /// <summary />
        public static async Task CreateCuttingOptimizationByObjectModelAndOptimize(IIntelliDivideClient intelliDivide)
        {
            // Prepare the request
            var request = await GetSampleCuttingOptimizationByObjectModel(intelliDivide, OptimizationRequestAction.Optimize);

            request.Trace(nameof(request));

            // Send the request
            var response = await intelliDivide.RequestOptimizationAsync(request);

            response.Trace(nameof(response));

            // Wait for completion
            var optimization = await intelliDivide.WaitForCompletionAsync(response.OptimizationId, TimeSpan.FromMinutes(1));

            optimization.Trace(nameof(optimization));
        }

        /// <summary />
        public static async Task CreateCuttingOptimizationByObjectModelAndOptimizeAndSend(IIntelliDivideClient intelliDivide)
        {
            // Prepare the request
            var request = await GetSampleCuttingOptimizationByObjectModel(intelliDivide, OptimizationRequestAction.Send);

            request.Trace(nameof(request));

            // Send the request
            var response = await intelliDivide.RequestOptimizationAsync(request);

            response.Trace(nameof(response));

            // Wait for transferred
            var optimization = await intelliDivide.WaitForOptimizationStatusAsync(response.OptimizationId, OptimizationStatus.Transferred, TimeSpan.FromMinutes(1));

            optimization.Trace(nameof(optimization));
        }

        /// <summary />
        public static async Task CreateCuttingOptimizationByObjectModelOptimizeAndRetrieveResults(IIntelliDivideClient intelliDivide)
        {
            var request = await GetSampleCuttingOptimizationByObjectModel(intelliDivide, OptimizationRequestAction.Optimize);
            var response = await intelliDivide.RequestOptimizationAsync(request);

            var optimization = await intelliDivide.WaitForCompletionAsync(response.OptimizationId, TimeSpan.FromSeconds(120));

            if (optimization.Status != OptimizationStatus.Optimized)
            {
                throw new InvalidOperationException("Optimization did not reach the state optimized.");
            }
            else
            {
                optimization.Trace(nameof(optimization));

                var solutions = await intelliDivide.GetSolutionsAsync(optimization.Id).ToListAsync() ?? throw new InvalidOperationException("Solutions could not get retrieved.");
                solutions.Trace(nameof(solutions));

                var balancedSolutionDetails = await intelliDivide.GetSolutionDetailsAsync(optimization.Id, solutions.First(s => s.Name == SolutionName.BalancedSolution).Id);

                balancedSolutionDetails.Trace(nameof(balancedSolutionDetails));

                await intelliDivide.DownloadSolutionExport(optimization.Id, balancedSolutionDetails.Id, SolutionExportType.Saw,
                    new FileInfo("CreateCuttingOptimizationByObjectModelOptimizeAndRetrieveResults.saw"));
            }
        }

        /// <summary />
        public static async Task CreateCuttingOptimizationByObjectModelOptimizeAndArchive(IIntelliDivideClient intelliDivide)
        {
            var request = await GetSampleCuttingOptimizationByObjectModel(intelliDivide, OptimizationRequestAction.Optimize);
            var response = await intelliDivide.RequestOptimizationAsync(request);

            var optimization = await intelliDivide.WaitForCompletionAsync(response.OptimizationId, TimeSpan.FromSeconds(120));

            if (optimization.Status != OptimizationStatus.Optimized)
            {
                throw new InvalidOperationException("Optimization did not reach the state optimized.");
            }

            optimization.Trace(nameof(optimization));

            await intelliDivide.ArchiveOptimizationAsync(optimization.Id);
        }

        /// <summary />
        public static async Task CreateCuttingOptimizationByObjectModelAndDelete(IIntelliDivideClient intelliDivide)
        {
            var request = await GetSampleCuttingOptimizationByObjectModel(intelliDivide, OptimizationRequestAction.ImportOnly);
            var response = await intelliDivide.RequestOptimizationAsync(request);

            response.Trace(nameof(response));

            var optimization = await intelliDivide.GetOptimizationAsync(response.OptimizationId);

            optimization.Trace(nameof(optimization));

            await intelliDivide.DeleteOptimizationAsync(optimization.Id);
        }

        private static async Task<OptimizationRequest> GetSampleCuttingOptimizationByObjectModel(IIntelliDivideClient intelliDivide, OptimizationRequestAction optimizationRequestAction,
            [CallerMemberName] string optimizationName = "")
        {
            var request = new OptimizationRequest();

            var machine = await intelliDivide.GetMachineAsync("productionAssist Cutting");
            var parameter = await intelliDivide.GetParametersAsync(machine.OptimizationType).FirstAsync();

            request.Name = optimizationName + DateTime.Now.ToString("s");
            request.Machine = machine.Name;
            request.Parameters = parameter.Name;

            request.Parts = new List<OptimizationRequestPart>
            {
                new()
                {
                    Description = "Part A",
                    MaterialCode = "P2_Gold Craft Oak_19",
                    Length = 400,
                    Width = 200,
                    Grain = Grain.Lengthwise,
                    Quantity = 1
                },

                new()
                {
                    Description = "Part B",
                    MaterialCode = "P2_White_19",
                    Length = 300,
                    Width = 300,
                    Grain = Grain.None,
                    Quantity = 5
                }
            };

            request.Action = optimizationRequestAction;

            return request;
        }
    }
}