using HomagConnect.ProductionAssist.Contracts.Cutting.Enumerations;

namespace HomagConnect.ProductionAssist.Contracts.Cutting
{
    public class CuttingJob
    {
        public string AvailableBoards { get; set; }

        public string Boards { get; set; }

        public Guid Id { get; set; }

        public string Materials { get; set; }

        public string Name { get; set; }

        public string Parts { get; set; }

        public CuttingState State { get; set; }
    }
}