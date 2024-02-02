using HomagConnect.Base.Services;
using HomagConnect.IntelliDivide.Contracts;

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

            var url = $"api/intelliDivide/optimizations?skip={skip}&take={take}".ToLower();

            return await RequestEnumerable<Optimization>(url);
        }

        public async Task<OptimizationStatus> GetOptimizationStatusAsync(Guid optimizationId)
        {
            var url = $"api/intelliDivide/optimizations/{optimizationId}/state".ToLower();

            return await RequestObject<OptimizationStatus>(url);
        }
    }
}