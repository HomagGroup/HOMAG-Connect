using HomagConnect.Base.Services;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Result;

namespace HomagConnect.IntelliDivide.Client
{
    /// <summary />
    public partial class IntelliDivideClient : ServiceBase
    {
        private const int _TakeLimit = 100;

        /// <summary>
        /// Gets the optimization having the specified optimization id.
        /// </summary>
        /// <param name="optimizationId">The id of of the optimization to get.</param>
        public async Task<Optimization> GetOptimizationAsync(Guid optimizationId)
        {
            var url = $"api/intelliDivide/optimizations/{optimizationId}".ToLower();

            return await RequestObject<Optimization>(url);
        }

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}" /> of optimizations available.
        /// </summary>
        /// <param name="take">Quantity of optimizations to return max.</param>
        /// <param name="skip">Quantity of optimizations to skip.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown, when more then 100 optimizations are requested.</exception>
        public async Task<IEnumerable<Optimization>> GetOptimizationsAsync(uint take, uint skip = 0)
        {
            if (take is > _TakeLimit or 0)
            {
                throw new ArgumentOutOfRangeException(nameof(take));
            }

            var url = $"api/intelliDivide/optimizations?take={take}&skip={skip}".ToLowerInvariant();

            return await RequestEnumerable<Optimization>(url);
        }

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}" /> of optimizations available.
        /// </summary>
        /// <param name="optimizationType">Request only optimizations having a specific <see cref="OptimizationType" /></param>
        /// <param name="take">Quantity of optimizations to return max.</param>
        /// <param name="skip">Quantity of optimizations to skip.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown, when more then 100 optimizations are requested.</exception>
        public async Task<IEnumerable<Optimization>> GetOptimizationsAsync(OptimizationType optimizationType, uint take, uint skip = 0)
        {
            if (take is > _TakeLimit or 0)
            {
                throw new ArgumentOutOfRangeException(nameof(take));
            }

            var url = $"api/intelliDivide/optimizations?optimizationType={optimizationType}&take={take}&skip={skip}".ToLowerInvariant();

            return await RequestEnumerable<Optimization>(url);
        }

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}" /> of optimizations available.
        /// </summary>
        /// <param name="optimizationType">Request only optimizations having a specific <see cref="OptimizationType" /></param>
        /// <param name="optimizationStatus">Request only optimizations having a specific <see cref="OptimizationStatus" /></param>
        /// <param name="take">Quantity of optimizations to return max.</param>
        /// <param name="skip">Quantity of optimizations to skip.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown, when more then 100 optimizations are requested.</exception>
        public async Task<IEnumerable<Optimization>> GetOptimizationsAsync(OptimizationType optimizationType, OptimizationStatus optimizationStatus, uint take, uint skip = 0)
        {
            if (take is > _TakeLimit or 0)
            {
                throw new ArgumentOutOfRangeException(nameof(take));
            }

            var url = $"api/intelliDivide/optimizations?optimizationType={optimizationType}&state={optimizationStatus}&take={take}&skip={skip}".ToLowerInvariant();

            return await RequestEnumerable<Optimization>(url);
        }

        /// <summary>
        /// Gets the <see cref="OptimizationStatus" /> of the optimization having the provided optimization id.
        /// </summary>
        /// <param name="optimizationId">The id of of the optimization.</param>
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

        /// <summary>
        /// Gets the <see cref="IEnumerable&lt;Solution&gt;" /> which have been calculated for an optimization request.
        /// </summary>
        /// <param name="optimizationId">The id of of the optimization.</param>
        /// <returns>Solutions if the optimization has been optimized successfully, otherwise an empty list.</returns>
        public async Task<IEnumerable<Solution>> GetSolutionsAsync(Guid optimizationId)
        {
            var url = $"/api/intelliDivide/optimizations/{optimizationId}/solutions".ToLowerInvariant();

            var solutions = await RequestEnumerable<Solution>(url);

            return solutions;
        }
    }
}