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
        public int AvailableBoards { get; set; }

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
        public int ProducedParts { get; set; }

        /// <summary>
        /// Required boards
        /// </summary>
        public int RequiredBoards { get; set; }

        /// <summary>
        /// Required parts
        /// </summary>
        public int RequiredParts { get; set; }

        /// <summary>
        /// Production order id
        /// </summary>
        public CuttingState State { get; set; }

        /// <summary>
        /// Used boards
        /// </summary>
        public int UsedBoards { get; set; }
    }
}