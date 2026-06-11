using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Sales
{
    /// <summary>
    /// Defines the type of a module, which determines how it can be used in the configuration and how it is represented in the UI.
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum MDModuleType
    {
        /// <summary>
        /// Sub-module type, can be used as a child module in the configuration
        /// </summary>
        SubModule,
        /// <summary>
        /// Root module type, can be used as a root module in the configuration
        /// </summary>
        RootModule,
        /// <summary>
        /// Module type that is only used as a placeholder in the configuration.
        /// </summary>
        ConfigDummy,
        /// <summary>
        /// Module type that is only used as a drop container in the UI.
        /// </summary>
        DropContainer,
        /// <summary>
        /// Module type that is only used as an orchestrator for grouping modules in the UI, it has no representation in the configuration.
        /// </summary>
        GroupOrchestrator
    }
}
