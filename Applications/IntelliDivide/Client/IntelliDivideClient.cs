using HomagConnect.Base.Services;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Request;

namespace HomagConnect.IntelliDivide.Client
{
    public class IntelliDivideClient : ServiceBase
    {
        #region Constructors

        public IntelliDivideClient(HttpClient client) : base(client) { }

        #endregion

        #region Settings (Machines, Paramters, Templates)

        public async Task<IEnumerable<OptimizationMachine>> GetMachinesAsync()
        {
            var cuttingMachines = await GetMachinesAsync(OptimizationType.Cutting);
            var nestingMachines = await GetMachinesAsync(OptimizationType.Nesting);

            return cuttingMachines.Union(nestingMachines).OrderBy(m => m.Name);
        }

        public async Task<OptimizationMachine> GetMachineAsync(string machineName)
        {
            var machines = await GetMachinesAsync(); 

            return machines.First(m => m.Name == machineName);
        }

        public async Task<IEnumerable<OptimizationMachine>> GetMachinesAsync(OptimizationType optimizationType)
        {
            var url = $"/api/intelliDivide/{optimizationType}/machines".ToLowerInvariant();
            return await RequestEnumerable<OptimizationMachine>(url);
        }

        public async Task<IEnumerable<OptimizationParameter>> GetParametersAsync(OptimizationType optimizationType)
        {
            var url = $"/api/intelliDivide/{optimizationType}/parameters".ToLowerInvariant();
            return await RequestEnumerable<OptimizationParameter>(url);
        }

        public async Task<IEnumerable<OptimizationImportTemplate>> GetImportTemplatesAsync(OptimizationType optimizationType, string fileExtension = "", string name = "")
        {
            var url = $"/api/intelliDivide/{optimizationType}/templates".ToLowerInvariant();
            var templates = await RequestEnumerable<OptimizationImportTemplate>(url);

            if (!string.IsNullOrEmpty(fileExtension))
            {
                templates = templates.Where(t => string.Equals(t.FileExtension.Trim(' ', '.'), fileExtension.Trim(' ', '.'), StringComparison.InvariantCultureIgnoreCase));
            }

            if (!string.IsNullOrEmpty(name))
            {
                templates = templates.Where(t => string.Equals(t.Name, name, StringComparison.InvariantCultureIgnoreCase));
            }

            return templates;
        }

        #endregion
    }
}