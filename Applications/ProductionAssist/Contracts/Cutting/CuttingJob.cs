using System.Runtime.Serialization;

using HomagConnect.ProductionAssist.Contracts.Cutting.Enumerations;

namespace HomagConnect.ProductionAssist.Contracts.Cutting
{
    /// <summary>
    /// Cutting job of productionAssist with the number of boards, parts and offcuts it contains.
    /// </summary>
    /// <example>
    /// { "boards": 12, "id": "7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c", "materials": "P2_Gold_Craft_Oak_19.0", "name": "Kitchen cutting", "parts": 96, "offcuts": 4, "state": "Started" }
    /// </example>
    public class CuttingJob
    {
        #region IExtensibleDataObject Members

        /// <inheritdoc />
        public ExtensionDataObject? ExtensionData { get; set; }

        #endregion

        #region Main data

        /// <summary>
        /// Number of boards in the cutting job.
        /// </summary>
        public int Boards { get; set; }

        /// <summary>
        /// Unique identifier of the cutting job.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Materials used in the cutting job.
        /// </summary>
        /// <example>P2_Gold_Craft_Oak_19.0</example>
        public string Materials { get; set; }

        /// <summary>
        /// Name of the cutting job.
        /// </summary>
        /// <example>Kitchen cutting</example>
        public string Name { get; set; }

        /// <summary>
        /// Number of parts in the cutting job.
        /// </summary>
        public int Parts { get; set; }

        /// <summary>
        /// Number of offcuts in the cutting job.
        /// </summary>
        public int Offcuts { get; set; }

        /// <summary>
        /// Current production state of the cutting job.
        /// </summary>
        /// <example>Started</example>
        public CuttingState State { get; set; }

        #endregion Main data
    }
}