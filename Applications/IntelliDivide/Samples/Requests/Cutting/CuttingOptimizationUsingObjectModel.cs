using System.Runtime.CompilerServices;

using HomagConnect.IntelliDivide.Client;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Base;
using HomagConnect.IntelliDivide.Contracts.Request;
using HomagConnect.IntelliDivide.Samples.Helper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Requests.Cutting
{
    public class CuttingOptimizationUsingObjectModel
    {
        public static async Task CreateCuttingOptimizationByObjectModel(IntelliDivideClient intelliDivide)
        {
            var request = await GetSampleCuttingOptimizationByObjectModel(intelliDivide);

            request.Action = OptimizationRequestAction.ImportOnly;

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
            Assert.AreEqual(request.Parts.Sum(p => p.Quantity), optimization.PartsCount);

            Assert.AreEqual(request.Machine, optimization.Machine);
        }

        public static async Task OptimizeCuttingOptimizationByObjectModel(IntelliDivideClient intelliDivide)
        {
            var request = await GetSampleCuttingOptimizationByObjectModel(intelliDivide);

            request.Action = OptimizationRequestAction.Optimize;

            var response = await intelliDivide.RequestOptimizationAsync(request);

            Assert.IsNotNull(response.OptimizationId);
            Assert.AreEqual(OptimizationStatus.Started, response.OptimizationStatus);
            Assert.IsFalse(response.ValidationErrors.Any());

            var optimizationId =  response.OptimizationId;

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

        private static async Task<OptimizationRequest> GetSampleCuttingOptimizationByObjectModel(IntelliDivideClient intelliDivide, [CallerMemberName] string optimizationName = "")
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

            return request;
        }
    }
}