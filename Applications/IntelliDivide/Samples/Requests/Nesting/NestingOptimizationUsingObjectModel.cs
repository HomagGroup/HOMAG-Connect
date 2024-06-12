using System.Collections.ObjectModel;
using System.Globalization;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Constants;
using HomagConnect.IntelliDivide.Contracts.Request;

namespace HomagConnect.IntelliDivide.Samples.Requests.Nesting
{
    /// <summary />
    public static class NestingOptimizationUsingObjectModel
    {
        /// <summary />
        public static async Task CreateNestingOptimizationByObjectModel(IIntelliDivideClient intelliDivide)
        {
            // Prepare the request
            var mprFiles = new List<ImportFile>();

            var machine = await intelliDivide.GetMachinesAsync(OptimizationType.Nesting).FirstAsync();
            var parameter = await intelliDivide.GetParametersAsync(machine.OptimizationType).FirstAsync();

            var request = await GetSampleNestingOptimizationByObjectModel(mprFiles);

            request.Name = "Nesting Sample-ImportOnly" + DateTime.Now.ToString("-yyyyMMdd-HHmm", CultureInfo.InvariantCulture);
            request.Machine = machine.Name;
            request.Parameters = parameter.Name;
            request.Action = OptimizationRequestAction.ImportOnly;

            request.Trace(nameof(request));

            // Send the request
            var response = await intelliDivide.RequestOptimizationAsync(request, mprFiles.ToArray());

            response.Trace(nameof(response));

            //// Retrieve the optimization
            var optimization = await intelliDivide.GetOptimizationAsync(response.OptimizationId);

            optimization.Trace(nameof(optimization));
        }

        /// <summary />
        public static async Task CreateNestingOptimizationByObjectModelAndOptimize(IIntelliDivideClient intelliDivide)
        {
            // Prepare the request
            var mprFiles = new List<ImportFile>();

            var machine = await intelliDivide.GetMachinesAsync(OptimizationType.Nesting).FirstAsync();
            var parameter = await intelliDivide.GetParametersAsync(machine.OptimizationType).FirstAsync();

            var request = await GetSampleNestingOptimizationByObjectModel(mprFiles);

            request.Name = "Nesting Sample-Optimize" + DateTime.Now.ToString("-yyyyMMdd-HHmm", CultureInfo.InvariantCulture);
            request.Machine = machine.Name;
            request.Parameters = parameter.Name;

            request.Action = OptimizationRequestAction.Optimize;

            request.Trace(nameof(request));

            // Send the request
            var response = await intelliDivide.RequestOptimizationAsync(request, mprFiles.ToArray());

            response.Trace(nameof(response));

            // Optional: Wait for the optimization to complete
            var optimization = await intelliDivide.WaitForCompletionAsync(response.OptimizationId, TimeSpan.FromMinutes(2));

            optimization.Trace(nameof(optimization));
        }

        /// <summary />
        public static async Task CreateNestingOptimizationByObjectModelAndOptimizeAndSend(IIntelliDivideClient intelliDivide)
        {
            // Prepare the request
            var mprFiles = new List<ImportFile>();

            var machine = await intelliDivide.GetMachinesAsync(OptimizationType.Nesting).FirstAsync(m => m.SupportsSending);
            var parameter = await intelliDivide.GetParametersAsync(machine.OptimizationType).FirstAsync();

            var request = await GetSampleNestingOptimizationByObjectModel(mprFiles);

            request.Name = "Nesting Sample-Send" + DateTime.Now.ToString("-yyyyMMdd-HHmm", CultureInfo.InvariantCulture);
            request.Machine = machine.Name;
            request.Parameters = parameter.Name;

            request.Action = OptimizationRequestAction.Send;

            request.Trace(nameof(request));

            // Send the request
            var response = await intelliDivide.RequestOptimizationAsync(request, mprFiles.ToArray());

            response.Trace(nameof(response));

            // Optional: Wait for the optimization to be transferred
            var optimization = await intelliDivide.WaitForOptimizationStatusAsync(response.OptimizationId, OptimizationStatus.Transferred, TimeSpan.FromMinutes(3));

            optimization.Trace(nameof(optimization));
        }

        /// <summary />
        public static async Task CreateNestingOptimizationByObjectModelOptimizeAndRetrieveResults(
            IIntelliDivideClient intelliDivide)
        {
            var mprFiles = new List<ImportFile>();

            var machine = await intelliDivide.GetMachinesAsync(OptimizationType.Nesting).FirstAsync();
            var parameter = await intelliDivide.GetParametersAsync(machine.OptimizationType).FirstAsync();

            var request = await GetSampleNestingOptimizationByObjectModel(mprFiles);

            request.Name = "Nesting Solution Details" + DateTime.Now.ToString("-yyyyMMdd-HHmm", CultureInfo.InvariantCulture);
            request.Machine = machine.Name;
            request.Parameters = parameter.Name;
            request.Action = OptimizationRequestAction.Optimize;

            request.Trace(nameof(request));
            
            var response = await intelliDivide.RequestOptimizationAsync(request, mprFiles.ToArray());

            response.Trace(nameof(response));
            
            var optimization = await intelliDivide.WaitForCompletionAsync(response.OptimizationId, TimeSpan.FromMinutes(2));
           
            if (optimization.Status != OptimizationStatus.Optimized)
            {
                throw new InvalidOperationException("Optimization did not reach the state optimized.");
            }
            
            optimization.Trace(nameof(optimization));
            
            var solutions = await intelliDivide.GetSolutionsAsync(optimization.Id).ToListAsync() ?? throw new InvalidOperationException("Solutions could not get retrieved.");
            solutions.Trace(nameof(solutions));
            
            var balancedSolutionDetails = await intelliDivide.GetSolutionDetailsAsync(optimization.Id, solutions.First(s => s.Name == SolutionName.BalancedSolution).Id);
            
            balancedSolutionDetails.Trace(nameof(balancedSolutionDetails));
        }

        private static async Task<OptimizationRequest> GetSampleNestingOptimizationByObjectModel(List<ImportFile> mprFiles)
        {
            var request = new OptimizationRequest();

            // Part A

            var mprA = new FileInfo(@"Requests\Nesting\PartA.mpr");
            
            request.Parts.Add(new OptimizationRequestPart
            {
                Description = mprA.Name,
                MprFileName = mprA.Name,
                MaterialCode = "P2_Gold Craft Oak_19",
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
            var mprB = new FileInfo(@"Requests\Nesting\PartB.mpr");
            
            request.Parts.Add(new OptimizationRequestPart
            {
                Description = mprB.Name,
                MprFileName = mprB.Name,
            
                MaterialCode = "P2_White_19",
                Grain = Grain.None,
                Quantity = 5
            });
            
            mprFiles.Add(await ImportFile.CreateAsync(mprB));

            return request;
        }
    }
}