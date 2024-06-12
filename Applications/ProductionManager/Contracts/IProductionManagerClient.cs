using HomagConnect.ProductionManager.Contracts.Import;

namespace HomagConnect.ProductionManager.Contracts
{
    /// <summary />
    public interface IProductionManagerClient
    {
        /// <summary>
        /// Import an order using a structured zip file.
        /// </summary>
        /// <param name="importOrderRequest">
        /// Import request based on a structured <see cref="ImportOrderRequest" />.
        /// </param>
        /// <param name="projectFile">
        /// Structured zip file, whose format corresponds to the ImportSpecification (
        /// <seealso
        ///     href="https://dev.azure.com/homag-group/FOSSProjects/_git/homag-api-gateway-client?path=/Documentation/ImportSpecification.md" />
        /// format.
        /// </param>
        Task<ImportOrderResponse> ImportOrderAsync(ImportOrderRequest importOrderRequest, FileInfo projectFile);
    }
}