using System.Runtime.CompilerServices;

using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Constants;
using HomagConnect.IntelliDivide.Contracts.Request;
using HomagConnect.IntelliDivide.Contracts.Result;
using HomagConnect.IntelliDivide.Samples.Helper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Requests.Cutting
{
    /// <summary />
    public class CuttingOptimizationUsingObjectModel
    {
        /// <summary />
        public static async Task CreateCuttingOptimizationByObjectModel(IIntelliDivideClient intelliDivide)
        {
            var request = await GetSampleCuttingOptimizationByObjectModel(intelliDivide, OptimizationRequestAction.ImportOnly);

            var response = await intelliDivide.RequestOptimizationAsync(request);

            Assert.IsNotNull(response.OptimizationId);
            Assert.AreEqual(OptimizationStatus.New, response.OptimizationStatus);
            Assert.IsFalse(response.ValidationErrors.Any());

            response.Trace();

            var optimization = await intelliDivide.GetOptimizationAsync(response.OptimizationId);

            Assert.IsNotNull(optimization);
            Assert.AreEqual(OptimizationStatus.New, optimization.Status);

            optimization.Trace();

            Assert.AreEqual(request.Parameters, optimization.ParameterName);
            Assert.AreEqual(request.Name, optimization.Name);
            Assert.AreEqual(request.Parts.Sum(p => p.Quantity), optimization.QuantityOfParts);

            Assert.AreEqual(request.Machine, optimization.Machine);
        }

        /// <summary />
        public static async Task CreateCuttingOptimizationByObjectModelAndOptimize(IIntelliDivideClient intelliDivide)
        {
            var request = await GetSampleCuttingOptimizationByObjectModel(intelliDivide, OptimizationRequestAction.Optimize);

            request.Trace(nameof(request));

            var response = await intelliDivide.RequestOptimizationAsync(request);

            Assert.IsNotNull(response.OptimizationId);
            Assert.AreEqual(OptimizationStatus.Started, response.OptimizationStatus);
            Assert.IsFalse(response.ValidationErrors.Any());

            var optimizationId = response.OptimizationId;

            var timeout = DateTime.Now + TimeSpan.FromSeconds(60);

            while (DateTime.Now < timeout)
            {
                var optimizationStatus = await intelliDivide.GetOptimizationStatusAsync(optimizationId);

                Assert.AreNotEqual(OptimizationStatus.Faulted, optimizationStatus);

                if (optimizationStatus == OptimizationStatus.Optimized)
                {
                    var optimization = await intelliDivide.GetOptimizationAsync(response.OptimizationId);

                    Assert.IsNotNull(optimization);
                    Assert.AreEqual(OptimizationStatus.Optimized, optimization.Status);

                    optimization.Trace();
                    return;
                }

                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            Assert.Fail("Timeout");
        }

        /// <summary />
        public static async Task CreateCuttingOptimizationByObjectModelAndOptimizeAndSend(IIntelliDivideClient intelliDivide)
        {
            var request = await GetSampleCuttingOptimizationByObjectModel(intelliDivide, OptimizationRequestAction.Send);

            var response = await intelliDivide.RequestOptimizationAsync(request);

            Assert.IsNotNull(response.OptimizationId);
            Assert.AreEqual(OptimizationStatus.Started, response.OptimizationStatus);
            Assert.IsFalse(response.ValidationErrors.Any());

            var optimizationId = response.OptimizationId;

            var timeout = DateTime.Now + TimeSpan.FromSeconds(120);

            while (DateTime.Now < timeout)
            {
                var optimizationStatus = await intelliDivide.GetOptimizationStatusAsync(optimizationId);

                Assert.AreNotEqual(OptimizationStatus.Faulted, optimizationStatus);

                if (optimizationStatus == OptimizationStatus.Transferred)
                {
                    var optimization = await intelliDivide.GetOptimizationAsync(response.OptimizationId);

                    Assert.IsNotNull(optimization);
                    Assert.AreEqual(OptimizationStatus.Transferred, optimization.Status);

                    optimization.Trace();
                    return;
                }

                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            Assert.Fail("Timeout");
        }

        /// <summary />
        public static async Task CreateCuttingOptimizationByObjectModelOptimizeAndRetrieveResults(IIntelliDivideClient intelliDivide)
        {
            var request = await GetSampleCuttingOptimizationByObjectModel(intelliDivide, OptimizationRequestAction.Optimize);
            var response = await intelliDivide.RequestOptimizationAsync(request);

            var optimization = await intelliDivide.WaitForCompletion(response.OptimizationId, TimeSpan.FromSeconds(120));

            if (optimization.Status != OptimizationStatus.Optimized)
            {
                throw new InvalidOperationException("Optimization did not reach the state optimized.");
            }
            else
            {
                optimization.Trace(nameof(optimization));

                var solutions = (await intelliDivide.GetSolutionsAsync(optimization.Id)).ToArray() ?? throw new InvalidOperationException("Solutions could not get retrieved.");
                solutions.Trace(nameof(solutions));

                var balancedSolutionDetails = await intelliDivide.GetSolutionDetailsAsync(optimization.Id, solutions.First(s => s.Name == SolutionName.BalancedSolution).Id);

                balancedSolutionDetails.Trace(nameof(balancedSolutionDetails));

                await intelliDivide.DownloadSolutionExport(optimization.Id, balancedSolutionDetails.Id, SolutionExportType.Saw,
                    new FileInfo("CreateCuttingOptimizationByObjectModelOptimizeAndRetrieveResults.saw"));
            }
        }

        private static async Task<OptimizationRequest> GetSampleCuttingOptimizationByObjectModel(IIntelliDivideClient intelliDivide, OptimizationRequestAction optimizationRequestAction,
            [CallerMemberName] string optimizationName = "")
        {
            var request = new OptimizationRequest();

            var machine = await intelliDivide.GetMachineAsync("productionAssist Cutting");
            var parameter = (await intelliDivide.GetParametersAsync(machine.OptimizationType)).OrderBy(p => p.Name).First();

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