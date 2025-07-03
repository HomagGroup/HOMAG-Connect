using HomagConnect.MaterialManager.Contracts.Processing.Interfaces;
using HomagConnect.MaterialManager.Samples.Helper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.MaterialManager.Samples.Read.ProcessParameters.Cnc
{
    /// <summary>
    /// Sample methods to read CNC process parameter data using the materialManager CNC client.
    /// </summary>
    public static class MaterialManagerReadCncParameters
    {
        /// <summary>
        /// Gets and traces all milling parameter parameter groups.
        /// </summary>
        public static async Task GetMillingParameterGroups(IMaterialManagerClientProcessingCnc cncClient)
        {
            var parameterGroups = await cncClient.GetMillingParameterGroups();
            Assert.IsNotNull(parameterGroups);
            parameterGroups.Trace();
        }

        /// <summary>
        /// Gets and traces all milling parameter material groups.
        /// </summary>
        public static async Task GetMillingParameterMaterialGroups(IMaterialManagerClientProcessingCnc cncClient)
        {
            var materialGroups = await cncClient.GetMillingParameterMaterialGroups();
            Assert.IsNotNull(materialGroups);
            materialGroups.Trace();
        }

        /// <summary>
        /// Gets and traces all milling parameter tool groups.
        /// </summary>
        public static async Task GetMillingParameterToolGroups(IMaterialManagerClientProcessingCnc cncClient)
        {
            var toolGroups = await cncClient.GetMillingParameterToolGroups();
            Assert.IsNotNull(toolGroups);
            toolGroups.Trace();
        }
    }
}