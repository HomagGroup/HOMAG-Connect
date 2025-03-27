using System.Runtime.Serialization;

using HomagConnect.ProductionAssist.Contracts.Cutting.Enumerations;

namespace HomagConnect.ProductionAssist.Contracts.Cutting
{
    /// <summary>
    /// Cutting job
    /// </summary>
    public class CuttingJob
    {
        #region IExtensibleDataObject Members

        /// <intheritdoc />
        public ExtensionDataObject? ExtensionData { get; set; }

        #endregion

        #region Main data

        /// <summary>
        /// The number of boards in this cutting job
        /// </summary>
        public int Boards { get; set; }

        /// <summary>
        /// Cutting job id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Materials
        /// </summary>
        public string Materials { get; set; }

        /// <summary>
        /// Cutting job name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The number of parts in this cutting job
        /// </summary>
        public int Parts { get; set; }

        /// <summary>
        /// The number of offcuts in this cutting job
        /// </summary>
        public int Offcuts { get; set; }

        /// <summary>
        /// Production order id
        /// </summary>
        public CuttingState State { get; set; }

        #endregion Main data
    }
}