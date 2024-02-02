using HomagConnect.Base.Services;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Result;

namespace HomagConnect.IntelliDivide.Client
{
    public partial class IntelliDivideClient : ServiceBase
    {
        public async Task<Optimization> GetOptimizationAsync(Guid optimizationId)
        {
            var url = $"api/intelliDivide/optimizations/{optimizationId}".ToLower();

            return await RequestObject<Optimization>(url);
        }

        public async Task<IEnumerable<Optimization>> GetOptimizationsAsync(uint skip = 0, uint take = 100)
        {
            if (take is > 500 or 0)
            {
                throw new ArgumentOutOfRangeException(nameof(take));
            }

            var url = $"api/intelliDivide/optimizations?skip={skip}&take={take}".ToLowerInvariant();

            return await RequestEnumerable<Optimization>(url);
        }

        public async Task<OptimizationStatus> GetOptimizationStatusAsync(Guid optimizationId)
        {
            var url = $"api/intelliDivide/optimizations/{optimizationId}/state".ToLowerInvariant();

            return await RequestObject<OptimizationStatus>(url);
        }

        public async Task<SolutionDetails> GetSolutionDetailsAsync(Guid optimizationId, Guid solutionId)
        {
            var url = $"/api/intelliDivide/optimizations/{optimizationId}/solutions/{solutionId}".ToLowerInvariant();

            var solution = await RequestObject<SolutionDetails>(url);

            return solution;
        }

        public async Task<IEnumerable<Solution>> GetSolutionsAsync(Guid optimizationId)
        {
            var url = $"/api/intelliDivide/optimizations/{optimizationId}/solutions".ToLowerInvariant();

            var solutions = await RequestEnumerable<Solution>(url);

            return solutions;
        }
    }
}