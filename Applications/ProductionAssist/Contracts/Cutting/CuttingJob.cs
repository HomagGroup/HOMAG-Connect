using HomagConnect.ProductionAssist.Contracts.Cutting.Enumerations;

namespace HomagConnect.ProductionAssist.Contracts.Cutting
{
    /// <summary>
    /// Cutting job
    /// </summary>
    public class CuttingJob
    {
        /// <summary>
        /// Available boards
        /// </summary>
        public string AvailableBoards { get; set; }

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
        /// Produced parts
        /// </summary>
        public string ProducedParts { get; set; }

        /// <summary>
        /// Required boards
        /// </summary>
        public string RequiredBoards { get; set; }

        /// <summary>
        /// Required parts
        /// </summary>
        public string RequiredParts { get; set; }

        /// <summary>
        /// Production order id
        /// </summary>
        public CuttingState State { get; set; }

        /// <summary>
        /// Used boards
        /// </summary>
        public string UsedBoards { get; set; }
    }
}